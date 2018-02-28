// <copyright file="AccountService.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MediaShop.BusinessLogic.Properties;
using MediaShop.Common;
using MediaShop.Common.Dto.Messaging;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Dto.User.Validators;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Exceptions.User;
using MediaShop.Common.Helpers;

namespace MediaShop.BusinessLogic.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using FluentValidation;

    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Class with user service business logic.
    /// </summary>
    /// <seealso cref="IAccountService" />
    public class AccountService : IAccountService
    {
        private readonly IAccountFactoryRepository _factoryRepository;
        private readonly IEmailService _emailService;
        private readonly IValidator<RegisterUserDto> _validator;
        private readonly IAccountTokenFactoryValidator _tokenValidator;

        /// <summary>
        /// account service
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AccountService(IAccountFactoryRepository factoryRepository, IEmailService emailService, IValidator<RegisterUserDto> validator, IAccountTokenFactoryValidator tokenValidator)
        {
            this._factoryRepository = factoryRepository;
            this._emailService = emailService;
            this._validator = validator;
            _tokenValidator = tokenValidator;
        }

        /// <summary>
        /// Find user for login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<AccountDbModel> FindUserAsync(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(Resources.NullOrEmptyValueString, nameof(userName));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(Resources.NullOrEmptyValue, nameof(password));
            }

            var user = await _factoryRepository.Accounts.GetByLoginAsync(userName);

            if (user == null || !user.Password.Equals(password) || user.IsDeleted || user.IsBanned)
            {
                return null;
            }

            if (!user.IsConfirmed)
            {
                throw new ConfirmedUserException(Resources.ConfirmationError);
            }

            return user;
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userModel">The user to register.</param>
        /// <returns><c>Account </c> if succeeded, <c>null</c> otherwise.</returns>
        /// <exception cref="ExistingLoginException">Throws when user with such login already exists</exception>
        public Account Register(RegisterUserDto userModel)
        {
            var result = _validator.Validate(userModel);

            if (!result.IsValid)
            {
                throw new ExistingLoginException(result.Errors.Select(m => m.ErrorMessage));
            }

            var findUser = _factoryRepository.Accounts.GetByEmail(userModel.Email);

            //User registers for the first time
            if (findUser == null)
            {
                var modelDbModel = Mapper.Map<AccountDbModel>(userModel);
                modelDbModel.Password = userModel.Password.GetHash();
                modelDbModel.AccountConfirmationToken = TokenHelper.NewToken();
                var account = this._factoryRepository.Accounts.Add(modelDbModel);
                account = account ?? throw new AddAccountException();
                var confirmationModel = Mapper.Map<AccountConfirmationDto>(modelDbModel);
                confirmationModel.Origin = userModel.Origin;
                _emailService.SendConfirmation(confirmationModel);

                return Mapper.Map<Account>(account);
            }

            //User registers after deleting
            findUser.IsConfirmed = false;
            findUser.Password = userModel.Password.GetHash();
            findUser.AccountConfirmationToken = TokenHelper.NewToken();
            var confirmedUser = this._factoryRepository.Accounts.UpdateAsync(findUser) ?? throw new UpdateAccountException();
            var confirmationModelUser = Mapper.Map<AccountConfirmationDto>(confirmedUser);
            confirmationModelUser.Origin = userModel.Origin;
            _emailService.SendConfirmation(confirmationModelUser);

            return Mapper.Map<Account>(confirmedUser);
        }

        public async Task<Account> RegisterAsync(RegisterUserDto userModel)
        {
            var result = _validator.Validate(userModel);

            if (!result.IsValid)
            {
                throw new ExistingLoginException(result.Errors.Select(m => m.ErrorMessage));
            }

            var findUser = await _factoryRepository.Accounts.GetByEmailAsync(userModel.Email).ConfigureAwait(false);

            //User registers for the first time
            if (findUser == null)
            {
                var modelDbModel = Mapper.Map<AccountDbModel>(userModel);
                modelDbModel.Password = userModel.Password.GetHash();
                modelDbModel.AccountConfirmationToken = TokenHelper.NewToken();
                var account = await this._factoryRepository.Accounts.AddAsync(modelDbModel).ConfigureAwait(false);
                account = account ?? throw new AddAccountException();
                var confirmationModel = Mapper.Map<AccountConfirmationDto>(modelDbModel);
                confirmationModel.Origin = userModel.Origin;
                await _emailService.SendConfirmationAsync(confirmationModel).ConfigureAwait(false);

                return Mapper.Map<Account>(account);
            }

            //User registers after deleting
            findUser.IsConfirmed = false;
            findUser.Login = userModel.Login;
            findUser.Password = userModel.Password.GetHash();
            findUser.AccountConfirmationToken = TokenHelper.NewToken();
            var confirmedUser = await this._factoryRepository.Accounts.UpdateAsync(findUser).ConfigureAwait(false) ?? throw new UpdateAccountException();
            var confirmationModelUser = Mapper.Map<AccountConfirmationDto>(confirmedUser);
            confirmationModelUser.Origin = userModel.Origin;
            await _emailService.SendConfirmationAsync(confirmationModelUser).ConfigureAwait(false);

            return Mapper.Map<Account>(confirmedUser);
        }

        /// <summary>
        /// Confirm user registration
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="id">id user</param>
        /// <returns><c>account</c> if succeeded</returns>
        public Account ConfirmRegistration(AccountConfirmationDto model)
        {
            var result = _tokenValidator.AccountConfirmation.Validate(model);

            if (!result.IsValid)
            {
                throw new ModelValidateException(result.Errors.Select(m => m.ErrorMessage));
            }

            var user = this._factoryRepository.Accounts.GetByEmail(model.Email);

            if (user.IsConfirmed)
            {
                throw new ConfirmedUserException(Resources.ConfirmationError);
            }

            //User registered fo the first time
            if (!user.IsDeleted)
            {
                var profile = this._factoryRepository.Profiles.Add(new ProfileDbModel());
                profile = profile ?? throw new AddProfileException();

                var settings = this._factoryRepository.Settings.Add(new SettingsDbModel());
                settings = settings ?? throw new AddSettingsException();

                user.IsConfirmed = true;
                user.ProfileId = profile.Id;
                user.Profile = profile;
                user.SettingsId = settings.Id;
                user.Settings = settings;
                user.AccountConfirmationToken = TokenHelper.NewToken();
                var confirmedUser = this._factoryRepository.Accounts.Update(user) ??
                                    throw new UpdateAccountException();

                return Mapper.Map<Account>(confirmedUser);
            }

            //User registers after deleting
            user.IsDeleted = false;
            user.IsConfirmed = true;
            var account = this._factoryRepository.Accounts.Update(user);
            return Mapper.Map<Account>(account);
        }

        public async Task<Account> ConfirmRegistrationAsync(AccountConfirmationDto model)
        {
            var result = _tokenValidator.AccountConfirmation.Validate(model);

            if (!result.IsValid)
            {
                throw new ModelValidateException(result.Errors.Select(m => m.ErrorMessage));
            }

            var user = this._factoryRepository.Accounts.GetByEmail(model.Email);

            if (user.IsConfirmed)
            {
                throw new ConfirmedUserException(Resources.ConfirmationError);
            }

            //User registered fo the first time
            if (!user.IsDeleted)
            {
                var profile = await this._factoryRepository.Profiles.AddAsync(new ProfileDbModel())
                    .ConfigureAwait(false);
                profile = profile ?? throw new AddProfileException();

                var settings = await this._factoryRepository.Settings.AddAsync(new SettingsDbModel())
                    .ConfigureAwait(false);
                settings = settings ?? throw new AddSettingsException();

                user.IsConfirmed = true;
                user.ProfileId = profile.Id;
                user.Profile = profile;
                user.SettingsId = settings.Id;
                user.Settings = settings;
                user.AccountConfirmationToken = TokenHelper.NewToken();

                var confirmedUser = await this._factoryRepository.Accounts.UpdateAsync(user).ConfigureAwait(false);
                confirmedUser = confirmedUser ?? throw new UpdateAccountException();

                return Mapper.Map<Account>(confirmedUser);
            }

            //User registers after deleting
            user.IsDeleted = false;
            user.IsConfirmed = true;
            var account = await this._factoryRepository.Accounts.UpdateAsync(user).ConfigureAwait(false);
            return Mapper.Map<Account>(account);
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="data">Login data</param>
        /// <returns><c>Authorised user</c></returns>
        public Account Login(LoginDto data)
        {
            if (data == null || string.IsNullOrWhiteSpace(data.Login) || string.IsNullOrWhiteSpace(data.Password))
            {
                throw new ArgumentNullException(Resources.NullOrEmptyValue, nameof(data));
            }

            var user = _factoryRepository.Accounts.GetByLogin(data.Login) ?? throw new NotFoundUserException();

            if (user.Password != data.Password)
            {
                throw new IncorrectPasswordException();
            }

            var statistic = new StatisticDbModel() { AccountId = user.Id };
            var result = this._factoryRepository.Statistics.Add(statistic);
            result = result ?? throw new AddStatisticException();

            return Mapper.Map<Account>(result.AccountDbModel);
        }

        public async Task<Account> LoginAsync(LoginDto data)
        {
            var user = _factoryRepository.Accounts.GetByLogin(data.Login) ?? throw new NotFoundUserException();

            if (user.Password != data.Password)
            {
                throw new IncorrectPasswordException();
            }

            var statistic = new StatisticDbModel() { AccountId = user.Id };
            var result = await this._factoryRepository.Statistics.AddAsync(statistic);
            result = result ?? throw new AddStatisticException();

            return Mapper.Map<Account>(result.AccountDbModel);
        }

        public Account Logout(long id)
        {
            var statistic = this._factoryRepository.Statistics.Find(s => s.AccountId == id && s.DateLogOut == null).FirstOrDefault();
            if (statistic == null)
            {
                throw new AddStatisticException();
            }

            statistic.DateLogOut = DateTime.Now;
            var result = this._factoryRepository.Statistics.Update(statistic) ?? throw new AddStatisticException();

            return Mapper.Map<Account>(result.AccountDbModel);
        }

        /// <summary>
        /// Init procedure password recovery
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotFoundUserException"></exception>
        /// <param name="email">Account Email</param>
        public void InitRecoveryPassword(ForgotPasswordDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                throw new ArgumentNullException(nameof(model.Email), Resources.NullOrEmptyValueString);
            }

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                throw new ArgumentNullException(nameof(model.Origin), Resources.NullOrEmptyValueString);
            }

            var user = this._factoryRepository.Accounts.GetByEmail(model.Email);

            if (user == null)
            {
                throw new NotFoundUserException();
            }

            var resoreDtoModel = Mapper.Map<AccountPwdRestoreDto>(user);
            resoreDtoModel.Origin = model.Origin;

            _emailService.SendRestorePwdLink(resoreDtoModel);
        }

        /// <summary>
        /// Init procedure password recovery
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotFoundUserException"></exception>
        /// <param name="email">Account Email</param>
        public async Task InitRecoveryPasswordAsync(ForgotPasswordDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                throw new ArgumentNullException(nameof(model.Email), Resources.NullOrEmptyValueString);
            }

            if (string.IsNullOrWhiteSpace(model.Email))
            {
                throw new ArgumentNullException(nameof(model.Origin), Resources.NullOrEmptyValueString);
            }

            var user = await this._factoryRepository.Accounts.GetByEmailAsync(model.Email).ConfigureAwait(false);

            if (user == null)
            {
                throw new NotFoundUserException();
            }

            var resoreDtoModel = Mapper.Map<AccountPwdRestoreDto>(user);
            resoreDtoModel.Origin = model.Origin;
            _emailService.SendRestorePwdLinkAsync(resoreDtoModel).ConfigureAwait(false);
        }

        /// <summary>
        /// Reset user password  for recovery
        /// </summary>
        /// <param name="email">user email</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotFoundUserException"></exception>
        /// <exception cref="ConfirmationTokenException"></exception>
        /// <returns>account</returns>
        public Account RecoveryPassword(ResetPasswordDto model)
        {
            var result = _tokenValidator.AccountPwdRestore.Validate(model);

            if (!result.IsValid)
            {
                throw new ModelValidateException(result.Errors.Select(m => m.ErrorMessage));
            }

            var user = this._factoryRepository.Accounts.GetByEmail(model.Email);

            user.Password = model.Password.GetHash();
            user.AccountConfirmationToken = TokenHelper.NewToken();
            var restoredUser = this._factoryRepository.Accounts.Update(user) ?? throw new UpdateAccountException();

            return Mapper.Map<Account>(restoredUser);
        }

        /// <summary>
        /// Reset user password  for recovery
        /// </summary>
        /// <param name="email">user email</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotFoundUserException"></exception>
        /// <exception cref="ConfirmationTokenException"></exception>
        /// <returns>account</returns>
        public async Task<Account> RecoveryPasswordAsync(ResetPasswordDto model)
        {
            var result = _tokenValidator.AccountPwdRestore.Validate(model);

            if (!result.IsValid)
            {
                throw new ModelValidateException(result.Errors.Select(m => m.ErrorMessage));
            }

            var user = await this._factoryRepository.Accounts.GetByEmailAsync(model.Email).ConfigureAwait(false);

            user.Password = model.Password.GetHash();
            user.AccountConfirmationToken = TokenHelper.NewToken();
            var restoredUser = await this._factoryRepository.Accounts.UpdateAsync(user).ConfigureAwait(false) ?? throw new UpdateAccountException();

            return Mapper.Map<Account>(restoredUser);
        }

        /// <summary>
        /// GetAllUsers
        /// </summary>
        /// <returns>IEnumerable<PermissionDto></returns>
        public IEnumerable<UserDto> GetAllUsers()
        {
            return Mapper.Map<IEnumerable<UserDto>>(_factoryRepository.Accounts.GetAllUsers());
        }       
    }
}

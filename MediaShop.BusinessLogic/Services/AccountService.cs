// <copyright file="AccountService.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System;
using MediaShop.BusinessLogic.Properties;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Dto.User.Validators;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Exceptions.CartExseptions;

namespace MediaShop.BusinessLogic.Services
{
    using System.Linq;
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

        /// <summary>
        /// account service
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AccountService(IAccountFactoryRepository factoryRepository, IEmailService emailService, IValidator<RegisterUserDto> validator)
        {
            this._factoryRepository = factoryRepository;
            this._emailService = emailService;
            this._validator = validator;
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

            var modelDbModel = Mapper.Map<AccountDbModel>(userModel);
            var account = this._factoryRepository.Accounts.Add(modelDbModel) ?? throw new AddAccountException();

            if (!_emailService.SendConfirmation(modelDbModel.Email, modelDbModel.Id))
            {
                throw new CanNotSendEmailException();
            }

            return Mapper.Map<Account>(account);
        }

        /// <summary>
        /// Confirm user registration
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="id">id user</param>
        /// <returns><c>account</c> if succeeded</returns>
        public Account ConfirmRegistration(string email, long id)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(Resources.NullOrEmptyValueString);
            }

            if (id < 1)
            {
                throw new ArgumentException(Resources.InvalidIdValue);
            }

            var user = this._factoryRepository.Accounts.Get(id);

            if (user == null || !user.Email.Equals(email))
            {
                throw new NotFoundUserException();
            }

            if (user.IsConfirmed)
            {
                throw new ConfirmedUserException();
            }

            var profile = this._factoryRepository.Profiles.Add(new ProfileDbModel()) ?? throw new AddProfileException();
            var settings = this._factoryRepository.Settings.Add(new SettingsDbModel()) ?? throw new AddSettingsException();

            user.IsConfirmed = true;
            user.ProfileId = profile.Id;
            user.Profile = profile;
            user.SettingsId = settings.Id;
            user.Settings = settings;
            var confirmedUser = this._factoryRepository.Accounts.Update(user) ?? throw new UpdateAccountException();

            return Mapper.Map<Account>(confirmedUser);
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="data">Login data</param>
        /// <returns><c>Authorised user</c></returns>
        public Account Login(LoginDto data)
        {
            var user = _factoryRepository.Accounts.GetByLogin(data.Login) ?? throw new NotFoundUserException();

            if (user.Password != data.Password)
            {
                throw new IncorrectPasswordException();
            }

            var statistic = new StatisticDbModel() { AccountId = user.Id };
            var result = this._factoryRepository.Statistics.Add(statistic) ?? throw new AddStatisticException();          

            return Mapper.Map<Account>(result.AccountDbModel);
        }

        public Account Logout(long id)
        {
            throw new NotImplementedException();
        }

        public Account RecoveryPassword(string email)
        {
            throw new NotImplementedException();
        }        

        public Account SetRemoveFlagIsBanned(Account accountBLmodel, bool flag)
        {
            var existingAccount = this._factoryRepository.Accounts.GetByLogin(accountBLmodel.Login) ??
                                  throw new NotFoundUserException();

            existingAccount.IsBanned = flag;

            var updatingAccount = this._factoryRepository.Accounts.Update(existingAccount);
            var updatingAccountBl = Mapper.Map<Account>(updatingAccount);

            return updatingAccountBl;
        }
    }
}

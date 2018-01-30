// <copyright file="AccountService.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System;
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
        private readonly IAccountRepository _storeAccounts;
        private readonly IProfileRepository _storeProfile;
        private readonly ISettingsRepository _storeSettings;
        private readonly IPermissionRepository _storePermission;
        private readonly IStatisticRepository _storeStatistic;
        private readonly IEmailService _emailService;
        private readonly IValidator<RegisterUserDto> _validator;

        /// <summary>
        /// account service
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AccountService(IAccountRepository repository, IProfileRepository repositoryProfile, ISettingsRepository repositorySettings, IPermissionRepository repositoryPermission, IStatisticRepository repositoryStatistic, IEmailService emailService, IValidator<RegisterUserDto> validator)
        {
            this._storeAccounts = repository;
            this._storePermission = repositoryPermission;
            this._storeProfile = repositoryProfile;
            this._storeSettings = repositorySettings;
            this._storeStatistic = repositoryStatistic;
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
            var account = this._storeAccounts.Add(modelDbModel) ?? throw new AddAccountException();

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
            var user = this._storeAccounts.Get(id);

            if (user == null || user.Email != email)
            {
                throw new NotFoundUserException();
            }

            if (user.IsConfirmed)
            {
                throw new ConfirmedUserException();
            }

            var profile = this._storeProfile.Add(new ProfileDbModel()) ?? throw new AddProfileException();
            var settings = this._storeSettings.Add(new SettingsDbModel()) ?? throw new AddSettingsException();

            user.IsConfirmed = true;
            user.ProfileId = profile.Id;
            user.Profile = profile;
            user.SettingsId = settings.Id;
            user.Settings = settings;
            var confirmedUser = this._storeAccounts.Update(user) ?? throw new UpdateAccountException();

            return Mapper.Map<Account>(confirmedUser);
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="data">Login data</param>
        /// <returns><c>Authorised user</c></returns>
        public Account Login(LoginDto data)
        {
            var user = _storeAccounts.GetByLogin(data.Login) ?? throw new NotFoundUserException();

            if (user.Password != data.Password)
            {
                throw new IncorrectPasswordException();
            }

            var statistic = new StatisticDbModel() { AccountId = user.Id };
            var result = this._storeStatistic.Add(statistic) ?? throw new AddStatisticException();          

            return Mapper.Map<Account>(result.AccountDbModel);
        }

        /// <summary>
        /// Removes the role from the user's permission list.
        /// </summary>
        /// <param name="id">The identifier of the user.</param>
        /// <param name="role">The role to remove.</param>
        /// <returns><c>true</c> if succeeded, <c>false</c> otherwise.</returns>
        public bool RemoveRole(RoleUserBl roleUserBl)
        {
            var existingAccount = this._storeAccounts.GetByLogin(roleUserBl.Login);
            if (existingAccount == null)
            {
                throw new NotFoundUserException();
            }

            var existingRoles = this._storePermission.GetByAccount(existingAccount);
            var existingRole = existingRoles.SingleOrDefault(p => p.Role == (Role)roleUserBl.Role);
            if (existingRole != null)
            {
                this._storePermission.Delete(existingRole);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds the role to the user's permission list.
        /// </summary>
        /// <param name="role">The role to add</param>
        /// <returns><c>Permission</c> if role added
        /// <c>null</c> otherwise</returns>
        public Permission AddRole(RoleUserBl role)
        {
            var account = this._storeAccounts.GetByLogin(role.Login);

            if (account == null)
            {
                throw new NotFoundUserException();
            }

            var existingPermission = account.Permissions.SingleOrDefault(x => (int)x.Role == role.Role);

            // User allready has this Role
            if (existingPermission != null)
            {
                return null;
            }

            var permission = Mapper.Map<PermissionDbModel>(role);
            permission.AccountDbModel = account;
            var addedPermission = _storePermission.Add(permission);

            return Mapper.Map<Permission>(addedPermission);
        }

        public Account SetRemoveFlagIsBanned(Account accountBLmodel, bool flag)
        {
            var existingAccount = this._storeAccounts.GetByLogin(accountBLmodel.Login);

            if (existingAccount == null)
            {
                throw new NotFoundUserException();
            }

            existingAccount.IsBanned = flag;

            var updatingAccount = this._storeAccounts.Update(existingAccount);
            var updatingAccountBl = Mapper.Map<Account>(updatingAccount);

            return updatingAccountBl;
        }
    }
}

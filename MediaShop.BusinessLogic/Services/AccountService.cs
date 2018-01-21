// <copyright file="AccountService.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Dto.User.Validators;
using MediaShop.Common.Exceptions;

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
        private readonly IPermissionRepository _storePermission;
        private readonly IEmailService _emailService;
        private readonly AbstractValidator<RegisterUserDto> _validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AccountService(IAccountRepository repository, IPermissionRepository repositoryPermission, IEmailService emailService, AbstractValidator<RegisterUserDto> validator)
        {
            this._storeAccounts = repository;
            this._storePermission = repositoryPermission;
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
            // 1. validate todo move to injector
            //var validator = new ExistingUserValidator(_storeAccounts);
            var result = _validator.Validate(userModel);

            if (!result.IsValid)
            {
                throw new ExistingLoginException(result.Errors.Select(m => m.ErrorMessage));
            }

            // 2. create account
            var modelDbModel = Mapper.Map<AccountDbModel>(userModel);
            this._storeAccounts.Add(modelDbModel);

            // 3. send email confirmation
            // email service -> sendConfirmation (email, id)
            if (!_emailService.SendConfirmation(modelDbModel.Email, modelDbModel.Id))
            {
                throw new CanNotSendEmailException();
            }

            // 4. return account
            return Mapper.Map<Account>(modelDbModel);
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

            user.IsConfirmed = true;
            user.Profile = new ProfileDbModel();
            user.Settings = new SettingsDbModel();

            var confirmedUser = this._storeAccounts.Update(user);

            return Mapper.Map<Account>(confirmedUser);
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="data">Login data</param>
        /// <returns><c>Authorised user</c></returns>
        public AuthorizedUser Login(LoginDto data)
        {
            var user = _storeAccounts.GetByLogin(data.Login);

            if (user.Password != data.Password)
            {
                throw new IncorrectPasswordException();
            }

            //TODO generate token-string write to DB  - id,token
            var authorizedUser = Mapper.Map<AuthorizedUser>(user);
            authorizedUser.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            return authorizedUser;
        }

        /// <summary>
        /// Adds the role to the user's permission list.
        /// </summary>
        /// <param name="role">The role to add</param>
        /// <returns><c>Permission</c> if role added
        /// <c>null</c> otherwise</returns>
        public PermissionDomain AddRole(RoleUserBl role)
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

            return Mapper.Map<PermissionDomain>(addedPermission);
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

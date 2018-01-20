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
    using MediaShop.Common.Dto;
    using MediaShop.Common.Helpers;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Class with user service business logic.
    /// </summary>
    /// <seealso cref="IAccountService" />
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _store;
        private readonly IPermissionRepository _storePermission;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AccountService(IAccountRepository repository, IPermissionRepository repositoryPermission)
        {
            this._store = repository;
            this._storePermission = repositoryPermission;
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userModel">The user to register.</param>
        /// <returns><c>Account </c> if succeeded, <c>null</c> otherwise.</returns>
        /// <exception cref="ExistingLoginException">Throws when user with such login already exists</exception>
        public Account Register(Account userModel)
        {
            var existingAccount = this._store.GetByLogin(userModel.Login);
            if (existingAccount != null)
            {
                throw new ExistingLoginException(userModel.Login);
            }

            var account = Mapper.Map<AccountDbModel>(userModel);

            var createdAccount = this._store.Add(account);

            return Mapper.Map<Account>(createdAccount);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public Account Register(RegisterUserDto userModel)
        {
            // 1. validate todo move to injector
            var validator = new ExistingUserVaildator(_store);
            var result = validator.Validate(userModel);
            if (!result.IsValid)
            {
                throw new ExistingLoginException(result.Errors.SelectMany(m => m.ErrorMessage).ToString());
            }

            // 2. create account
            var modelDbModel = Mapper.Map<AccountDbModel>(userModel);
            this._store.Add(modelDbModel);

            // 3. send email confirmation
            // email service -> sendConfirmation (email, id)

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
            var existingAccount = this._store.GetByLogin(roleUserBl.Login);
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
        public PermissionDomain AddRole(RoleUserBl role)
        {
            var account = this._store.GetByLogin(role.Login);

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

            var permission = Mapper.Map<Permission>(role);
            permission.AccountDbModel = account;
            var addedPermission = _storePermission.Add(permission);

            return Mapper.Map<PermissionDomain>(addedPermission);
        }

        public Account SetRemoveFlagIsBanned(Account accountBLmodel, bool flag)
        {
            var existingAccount = this._store.GetByLogin(accountBLmodel.Login);

            if (existingAccount == null)
            {
                throw new NotFoundUserException();
            }

            existingAccount.IsBanned = flag;

            var updatingAccount = this._store.Update(existingAccount);
            var updatingAccountBl = Mapper.Map<Account>(updatingAccount);

            return updatingAccountBl;
        }
    }
}

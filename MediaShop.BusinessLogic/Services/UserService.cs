// <copyright file="UserService.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System;
using MediaShop.Common.Dto.User;
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
    /// <seealso cref="IUserService" />
    public class UserService : IUserService
    {
        private readonly IAccountRepository _store;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UserService(IAccountRepository repository)
        {
            this._store = repository;
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userModel">The user to register.</param>
        /// <returns><c>Account </c> if succeeded, <c>null</c> otherwise.</returns>
        /// <exception cref="ExistingLoginException">Throws when user with such login already exists</exception>
        public AccountDomain Register(AccountDomain userModel)
        {
            var existingAccount = this._store.GetByLogin(userModel.Login);
            if (existingAccount != null)
            {
                throw new ExistingLoginException(userModel.Login);
            }

            var account = Mapper.Map<Account>(userModel);

            var createdAccount = this._store.Add(account);

            return Mapper.Map<AccountDomain>(createdAccount);
        }

        /// <summary>
        /// Removes the role from the user's permission list.
        /// </summary>
        /// <param name="id">The identifier of the user.</param>
        /// <param name="role">The role to remove.</param>
        /// <returns><c>true</c> if succeeded, <c>false</c> otherwise.</returns>
        public bool RemoveRole(AccountDomain accountBLmodel, Role role)
        {
           /* var user = this._store.Find(account => account.Id == id).FirstOrDefault();

            return user?.Permissions.Remove(accountRole => accountRole == role) > 0;*/
            return true;
        }

        public AccountDomain SetRemoveFlagIsBanned(AccountDomain accountBLmodel, bool flag)
        {
            var existingAccount = this._store.GetByLogin(accountBLmodel.Login);

            if (existingAccount == null)
            {
                throw new ExistingLoginException(accountBLmodel.Login);
            }

            existingAccount.IsBanned = flag;

            var updatingAccount = this._store.Update(existingAccount);
            var updatingAccountBl = Mapper.Map<AccountDomain>(updatingAccount);

            return updatingAccountBl;
        }
    }
}

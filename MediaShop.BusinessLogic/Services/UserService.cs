// <copyright file="UserService.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

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
        private readonly IRepository<Account> store;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UserService(IRepository<Account> repository)
        {
            this.store = repository;
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userModel">The user to register.</param>
        /// <returns><c>Account </c> if succeeded, <c>null</c> otherwise.</returns>
        /// <exception cref="MediaShop.Common.Models.User.ExistingLoginException">Throws when user with such login already exists</exception>
        public Account Register(UserDto userModel)
        {
            if (this.store.Find(x => x.Login == userModel.Login)?.Single() != null)
            {
                throw new ExistingLoginException(userModel.Login);
            }

            var account = Mapper.Map<Account>(userModel);
            account.Permissions.Add(userModel.UserRole);

            var createdAccount = this.store.Add(account);

            return createdAccount;
        }

        /// <summary>
        /// Removes the role from the user's permission list.
        /// </summary>
        /// <param name="id">The identifier of the user.</param>
        /// <param name="role">The role to remove.</param>
        /// <returns><c>true</c> if succeeded, <c>false</c> otherwise.</returns>
        public bool RemoveRole(ulong id, Role role)
        {
            var user = this.store.Find(account => account.Id == id).FirstOrDefault();

            return user?.Permissions.Remove(accountRole => accountRole == role) > 0;
        }
    }
}
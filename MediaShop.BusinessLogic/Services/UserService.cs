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
    /// <seealso cref="MediaShop.Common.Interfaces.Services.IUserService" />
    public class UserService : IUserService
    {
        private readonly IRespository<Account> store;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UserService(IRespository<Account> repository)
        {
            this.store = repository;
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userModel">The user to register.</param>
        /// <returns><c>true</c> if succeeded, <c>false</c> otherwise.</returns>
        /// <exception cref="MediaShop.Common.Models.User.ExistingLoginException">Throws when user with such login already exists</exception>
        public bool Register(UserDto userModel)
        {
            if (this.store.Find(x => x.Login == userModel.Login).FirstOrDefault() != null)
            {
                throw new ExistingLoginException(userModel.Login);
            }

            var account = Mapper.Map<Account>(userModel);
            account.Permissions.Add(userModel.UserRole);

            var createdAccount = this.store.Add(account);

            if (createdAccount == null || createdAccount.Id == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Removes the role from the user's permission list.
        /// </summary>
        /// <param name="id">The identifier of the user.</param>
        /// <param name="role">The role to remove.</param>
        /// <returns><c>true</c> if succeeded, <c>false</c> otherwise.</returns>
        public bool RemoveRole(int id, Role role)
        {
            var user = this.store.Find(account => account.Id == id).FirstOrDefault();

            return user?.Permissions.Remove(accountRole => accountRole == role) > 0;
        }
    }
}
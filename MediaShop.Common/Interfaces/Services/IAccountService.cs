// <copyright file="IAccountService.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System;
using MediaShop.Common.Dto.User;

namespace MediaShop.Common.Interfaces.Services
{
    using MediaShop.Common.Dto;
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Interface IAccountService
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userModel">The user to register.</param>
        /// <returns><c>true</c> if succeeded, <c>false</c> otherwise.</returns>
        Account Register(RegisterUserDto userModel);

        /// <summary>
        /// Confirm user registration
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="id">id user</param>
        /// <returns><c>account</c> if succeeded</returns>
        Account ConfirmRegistration(string email, long id);

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="data">Login data</param>
        /// <returns><c>Authorised user</c></returns>
        Account Login(LoginDto data);

        /// <summary>
        /// Removes the role from the user's permission list.
        /// </summary>
        /// <param name="id">The identifier of the user.</param>
        /// <param name="role">The role to remove.</param>
        /// <returns><c>true</c> if succeeded, <c>false</c> otherwise.</returns>
        bool RemoveRole(RoleUserBl roleUserBl);

        /// <summary>
        /// Adds the role to the user's permission list.
        /// </summary>
        /// <param name="role">The role to add</param>
        /// <returns><c>Permission</c> if role added
        /// <c>null</c> otherwise</returns>
        Permission AddRole(RoleUserBl role);

        Account SetRemoveFlagIsBanned(Account accountBLmodel, bool flag);
    }
}

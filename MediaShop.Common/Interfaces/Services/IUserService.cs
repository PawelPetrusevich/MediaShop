// <copyright file="IUserService.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System;
using MediaShop.Common.Dto.User;

namespace MediaShop.Common.Interfaces.Services
{
    using MediaShop.Common.Dto;
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Interface IUserService
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userModel">The user to register.</param>
        /// <returns><c>true</c> if succeeded, <c>false</c> otherwise.</returns>
        Account Register(UserDto userModel);

        /// <summary>
        /// Removes the role from the user's permission list.
        /// </summary>
        /// <param name="id">The identifier of the user.</param>
        /// <param name="role">The role to remove.</param>
        /// <returns><c>true</c> if succeeded, <c>false</c> otherwise.</returns>
        bool RemoveRole(long id, Role role);
    }
}

// <copyright file="IAccountService.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Exceptions.User;

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
        /// <param name="token">Confirmation token</param>
        /// <returns><c>account</c> if succeeded</returns>
        Account ConfirmRegistration(string email, string confirmationToken);

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="data">Login data</param>
        /// <returns><c>Login account</c></returns>
        Account Login(LoginDto data);

        /// <summary>
        /// Logout user
        /// </summary>
        /// <param name="id">id user</param>
        /// <returns>account</returns>
        Account Logout(long id);

        /// <summary>
        /// Reset user password  for recovery
        /// </summary>
        /// <param name="email">user email</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotFoundUserException"></exception>
        /// <exception cref="ConfirmationTokenException"></exception>
        /// <returns>account</returns>
        Account RecoveryPassword(ResetPasswordDto model);

        /// <summary>
        /// Init procedure password recovery
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotFoundUserException"></exception>
        /// <param name="email">Account Email</param>
        void InitRecoveryPassword(string email);

        /// <summary>
        /// Set permission
        /// </summary>
        /// <param name="permissionDto">Permissions data</param>
        /// <returns>account</returns>
        Account SetPermission(PermissionDto permission);

        /// <summary>
        /// Remove permission
        /// </summary>
        /// <param name="permissionDto">Permissions data</param>
        /// <returns>account</returns>
        Account RemovePermission(PermissionDto permission);

        /// <summary>
        /// Set or remove flag banned
        /// </summary>
        /// <param name="accountBLmodel"></param>
        /// <param name="flag"></param>
        /// <returns>account</returns>
        Account SetRemoveFlagIsBanned(Account accountBLmodel, bool flag);
    }
}

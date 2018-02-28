// <copyright file="IAccountService.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using MediaShop.Common.Dto.Messaging;
using MediaShop.Common.Dto.User;
using MediaShop.Common.Exceptions;
using MediaShop.Common.Exceptions.User;

namespace MediaShop.Common.Interfaces.Services
{
    using System.Collections.Generic;

    using MediaShop.Common.Dto;
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Interface IAccountService
    /// </summary>
    public interface IAccountService
    {
        Task<AccountDbModel> FindUserAsync(string userName, string password);

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userModel">The user to register.</param>
        /// <returns><c>true</c> if succeeded, <c>false</c> otherwise.</returns>
        Account Register(RegisterUserDto userModel);

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userModel">The user to register.</param>
        /// <returns><c>true</c> if succeeded, <c>false</c> otherwise.</returns>
        Task<Account> RegisterAsync(RegisterUserDto userModel);

        /// <summary>
        /// Confirm user registration
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="token">Confirmation token</param>
        /// <returns><c>account</c> if succeeded</returns>
        Account ConfirmRegistration(AccountConfirmationDto model);

        /// <summary>
        /// Confirm user registration
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="id">id user</param>
        /// <returns><c>account</c> if succeeded</returns>
        Task<Account> ConfirmRegistrationAsync(AccountConfirmationDto model);

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="data">Login data</param>
        /// <returns><c>Login account</c></returns>
        Account Login(LoginDto data);

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="data">Login data</param>
        /// <returns><c>Login account</c></returns>
        Task<Account> LoginAsync(LoginDto data);

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
        /// Reset user password  for recovery
        /// </summary>
        /// <param name="email">user email</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotFoundUserException"></exception>
        /// <exception cref="ConfirmationTokenException"></exception>
        /// <returns>account</returns>
        Task<Account> RecoveryPasswordAsync(ResetPasswordDto model);

        /// <summary>
        /// Init procedure password recovery
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotFoundUserException"></exception>
        /// <param name="email">Account Email</param>
        void InitRecoveryPassword(ForgotPasswordDto model);

        /// <summary>
        /// Init procedure password recovery
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NotFoundUserException"></exception>
        /// <param name="email">Account Email</param>
        Task InitRecoveryPasswordAsync(ForgotPasswordDto model);

        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns>IEnumerable<PermissionDto></returns>
        IEnumerable<UserDto> GetAllUsers();
    }
}

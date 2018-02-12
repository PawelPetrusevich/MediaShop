// <copyright file="IAccountService.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
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
        /// <param name="id">id user</param>
        /// <returns><c>account</c> if succeeded</returns>
        Account ConfirmRegistration(string email, long id);

        /// <summary>
        /// Confirm user registration
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="id">id user</param>
        /// <returns><c>account</c> if succeeded</returns>
        Task<Account> ConfirmRegistrationAsync(string email, long id);

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
        /// <returns>account</returns>
        Account RecoveryPassword(string email);      
    }
}

// <copyright file="UserRepository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.Common.Interfaces.Repositories
{
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Interface IAccountRepository
    /// </summary>
    /// <seealso cref="IRepository{Account}" />
    public interface IAccountRepository : IRepository<Account>
    {
        /// <summary>
        /// Gets the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns>Entity</returns>
        Account GetByLogin(string login);
    }
}

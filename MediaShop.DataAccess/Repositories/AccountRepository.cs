// <copyright file="AccountRepository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.DataAccess.Repositories
{
    using System.Data.Entity;
    using System.Linq;

    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Class AccountRepository.
    /// </summary>
    /// <seealso cref="Repositories.Repository{Account}" />
    /// <seealso cref="IAccountRepository" />
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AccountRepository(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Gets the specified login.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns>Entity</returns>
        public Account GetByLogin(string login)
        {
            return this.Dbset.Single(account => account.Login == login);
        }
    }
}
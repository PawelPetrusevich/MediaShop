// <copyright file="AccountProfileRepository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.DataAccess.Repositories
{
    using System.Data.Entity;

    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Class AccountProfileRepository.
    /// </summary>
    /// <seealso cref="MediaShop.DataAccess.Repositories.Repository{AccountProfile}" />
    /// <seealso cref="MediaShop.Common.Interfaces.Repositories.IAccountProfileRepository" />
    public class AccountProfileRepository : Repository<AccountProfile>, IAccountProfileRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountProfileRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AccountProfileRepository(DbContext context)
            : base(context)
        {
        }
    }
}

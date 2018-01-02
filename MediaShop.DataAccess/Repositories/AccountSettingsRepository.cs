// <copyright file="AccountSettingsRepository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.DataAccess.Repositories
{
    using System.Data.Entity;

    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Class AccountSettingsRepository.
    /// </summary>
    /// <seealso cref="MediaShop.DataAccess.Repositories.Repository{AccountSettings}" />
    /// <seealso cref="IAccountSettingsRepository" />
    public class AccountSettingsRepository : Repository<AccountSettings>, IAccountSettingsRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountSettingsRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AccountSettingsRepository(DbContext context)
            : base(context)
        {
        }
    }
}
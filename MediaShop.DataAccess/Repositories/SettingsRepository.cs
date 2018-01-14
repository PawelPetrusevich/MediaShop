// <copyright file="SettingsRepository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

using MediaShop.DataAccess.Repositories.Base;

namespace MediaShop.DataAccess.Repositories
{
    using System.Data.Entity;

    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Class SettingsRepository.
    /// </summary>
    /// <seealso cref="Repository{T}" />
    /// <seealso cref="ISettingsRepository" />
    public class SettingsRepository : Repository<Settings>, ISettingsRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SettingsRepository(DbContext context)
            : base(context)
        {
        }
    }
}
// <copyright file="ProfileRepository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.DataAccess.Repositories
{
    using System.Data.Entity;

    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Class ProfileRepository.
    /// </summary>
    /// <seealso cref="MediaShop.DataAccess.Repositories.Repository{AccountProfile}" />
    /// <seealso cref="MediaShop.Common.Interfaces.Repositories.IProfileRepository" />
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ProfileRepository(DbContext context)
            : base(context)
        {
        }
    }
}

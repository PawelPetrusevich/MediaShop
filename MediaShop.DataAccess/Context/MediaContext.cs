// <copyright file="MediaContext.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

namespace MediaShop.DataAccess.Context
{
    using MediaShop.DataAccess.Configurations;
    using System.Data.Entity;
    using MediaShop.Common.Models;

    /// <summary>
    /// Class MediaContext.
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class MediaContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaContext"/> class.
        /// </summary>
        public MediaContext()
            : base("MediaShopConnection")
        {
        }

        public IDbSet<ContentCart> ContentCart { get; set; }

        /// <summary>
        /// Method configuration tables
        /// </summary>
        /// <param name="modelBuilder">modelBuilder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ContentCartConfiguration());

            // your config for Entity here placed in configuartion folder
        }
    }
}

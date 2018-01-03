// <copyright file="MediaContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MediaShop.DataAccess.Context
{
    using System.Data.Entity;
    using MediaShop.Common.Models.Content;

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

        /// <summary>
        /// Gets or sets the Products.
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the ProductTypes.
        /// </summary>
        public DbSet<ProductType> ProductTypes { get; set; }

        /// <summary>
        /// Overrided method OnModelCreating.
        /// </summary>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ProductConfiguration());
        }
    }
}
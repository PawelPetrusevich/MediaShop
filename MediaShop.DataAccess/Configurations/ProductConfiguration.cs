// <copyright file="ProductConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MediaShop.DataAccess.Context
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.ModelConfiguration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MediaShop.Common.Models.Content;

    /// <summary>
    /// class ProductConfiguration.
    /// </summary>
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductConfiguration"/> class.
        /// </summary>
        public ProductConfiguration()
        {
            this.HasRequired<ProductType>(p => p.ProductType)
                .WithMany(p => p.Products)
                .HasForeignKey<int>(s => s.ProductTypeId);
            this.HasKey(p => p.Id);
            this.Property(p => p.Description)
                .IsOptional()
                .IsVariableLength()
                .IsUnicode(true);
            this.Property(p => p.IsFavorite)
                .IsRequired();
            this.Property(p => p.IsPremium)
                .IsRequired();
            this.Property(p => p.ProductName)
                .IsRequired()
                .HasMaxLength(50);
            this.Property(p => p.ProductPrice)
                .IsRequired()
                .HasPrecision(15, 2);
        }
    }
}

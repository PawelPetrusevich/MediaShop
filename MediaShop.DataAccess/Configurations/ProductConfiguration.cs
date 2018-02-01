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
            this.HasKey(k => k.Id);
            this.HasRequired(s => s.OriginalProduct).WithRequiredDependent(x => x.Product);
            this.HasRequired(s => s.CompressedProduct).WithRequiredDependent(x => x.Product);
            this.HasRequired(s => s.ProtectedProduct).WithRequiredDependent(x => x.Product);
        }
    }
}

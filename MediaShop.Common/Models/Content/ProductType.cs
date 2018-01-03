// <copyright file="ProductType.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MediaShop.Common.Models.Content
{
    using System.Collections.Generic;

    /// <summary>
    /// Class ProductType
    /// </summary>
    public class ProductType
    {
        /// <summary>
        /// Gets or sets the TypeId.
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// Gets or sets the TypeName.
        /// </summary>
        public int TypeName { get; set; }

        /// <summary>
        /// Gets or sets collection of Products.
        /// </summary>
        public ICollection<Product> Products { get; set; }
    }
}

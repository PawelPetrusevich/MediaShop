// <copyright file="Product.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediaShop.Common.Models.User;

namespace MediaShop.Common.Models.Content
{
    /// <summary>
    /// Class Product
    /// </summary>
    public class Product : Entity
    {
        /// <summary>
        /// Gets or sets the ProductName.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ProductPrice.
        /// </summary>
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the bollean IsPremium.
        /// </summary>
        public bool IsPremium { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the login.
        /// </summary>
        public bool IsFavorite { get; set; }

        /// <summary>
        /// Gets or sets the ProductType.
        /// </summary>
        public ProductType ProductType { get; set; }
    }
}

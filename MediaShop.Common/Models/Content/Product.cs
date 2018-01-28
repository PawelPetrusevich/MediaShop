// <copyright file="Product.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediaShop.Common.Enums;

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

        /// <summary>
        /// Gets or sets file original product
        /// </summary>
        public OriginalProduct OriginalProduct { get; set; }

        /// <summary>
        /// Gets or sets file id of original product
        /// </summary>
        public long OriginalProductId { get; set; }

        /// <summary>
        /// Gets or sets file of protected product
        /// </summary>
        public ProtectedProduct ProtectedProduct { get; set; }

        /// <summary>
        /// Gets or sets file id of protected product
        /// </summary>
        public long ProtectedProductId { get; set; }

        /// <summary>
        /// Gets or sets file of compressed product 
        /// </summary>
        public CompressedProduct CompressedProduct { get; set; }

        /// <summary>
        /// Gets or sets file id of compressed product 
        /// </summary>
        public long CompressedProductId { get; set; }
    }
}

// <copyright file="ProductDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MediaShop.Common.Dto
{
    /// <summary>
    /// class ProductDto
    /// </summary>
    public class ProductDto
    {
        /// <summary>
        /// Gets or sets the ProductName.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the ProductPrice.
        /// </summary>
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// Gets or sets the ProductId.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }
    }
}

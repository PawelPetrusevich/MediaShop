// <copyright file="IProductService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using MediaShop.Common.Dto.Product;

namespace MediaShop.Common.Interfaces.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Net;
    using MediaShop.Common.Dto;
    using MediaShop.Common.Models.Content;

    /// <summary>
    /// Interface IProductService.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get method.
        /// </summary>
        /// <param name="id">product Id</param>
        /// <returns>result</returns>
        ProductDto GetProduct(long id);

        /// <summary>
        /// Upload products.
        /// </summary>
        /// <param name="model">product model</param>
        /// <returns>result</returns>
        ProductDto UploadProducts(UploadProductModel model);

        /// <summary>
        /// DElete method.
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>result</returns>
        ProductDto DeleteProduct(long id);

        /// <summary>
        /// Find method.
        /// </summary>
        /// <param name="conditionsList">filter</param>
        /// <returns>product</returns>
        IEnumerable<ProductDto> Find(List<ProductSearchModel> conditionsList);
    }
}
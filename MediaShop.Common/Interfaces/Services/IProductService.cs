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
        /// Get method.
        /// </summary>
        /// <param name="id">product Id</param>
        /// <returns>result</returns>
        ProductContentDTO GetOriginalProduct(long id);

        /// <summary>
        /// Get method.
        /// </summary>
        /// <param name="id">product Id</param>
        /// <returns>result</returns>
        ProductContentDTO GetProtectedProduct(long id);

        /// <summary>
        /// Get method.
        /// </summary>
        /// <param name="id">product Id</param>
        /// <returns>result</returns>
        ProductContentDTO GetCompressedProduct(long id);

        /// <summary>
        /// Upload products.
        /// </summary>
        /// <param name="model">product model</param>
        /// <returns>result</returns>
        ProductDto UploadProducts(UploadModel model);

        /// <summary>
        /// Update method.
        /// </summary>
        /// <param name="model">product model</param>
        /// <returns>result</returns>
        ProductDto Update(Product model);

        /// <summary>
        /// DElete method.
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>result</returns>
        ProductDto DeleteProduct(long id);

        /// <summary>
        /// Find method.
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>product</returns>
        //IEnumerable<Product> Find(Expression<Func<Product, bool>> filter);
        IEnumerable<ProductDto> Find(List<ProductSearchModel> conditionsList);
    }
}
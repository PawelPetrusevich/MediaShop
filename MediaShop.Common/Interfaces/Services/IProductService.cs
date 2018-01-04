// <copyright file="IProductService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
        Product Get(int id);

        /// <summary>
        /// Add method.
        /// </summary>
        /// <param name="model">product model</param>
        /// <returns>result</returns>
        ProductDto Add(Product model);

        /// <summary>
        /// Update method.
        /// </summary>
        /// <param name="model">product model</param>
        /// <returns>result</returns>
        ProductDto Update(Product model);

        /// <summary>
        /// Update method.
        /// </summary>
        /// <param name="model">product model</param>
        /// <returns>result</returns>
        ProductDto Delete(Product model);

        /// <summary>
        /// DElete method.
        /// </summary>
        /// <param name="id">product id</param>
        /// <returns>result</returns>
        ProductDto Delete(int id);

        /// <summary>
        /// Find method.
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>product</returns>
        IEnumerable<Product> Find(Expression<Func<Product, bool>> filter);

        /// <summary>
        /// IEnumerable Products.
        /// </summary>
        /// <returns>products</returns>
        IEnumerable<Product> Products();

        /// <summary>
        /// Add method.
        /// </summary>
        /// <param name="products">products</param>
        /// <returns>product</returns>
        IEnumerable<ProductDto> Add(IEnumerable<Product> products);

        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="products">products</param>
        /// <returns>product</returns>
        IEnumerable<ProductDto> Delete(IEnumerable<Product> products);
    }
}
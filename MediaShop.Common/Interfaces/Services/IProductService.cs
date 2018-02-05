﻿// <copyright file="IProductService.cs" company="PlaceholderCompany">
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
        ProductDto GetById(long id);

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
        ProductDto SoftDeleteById(long id);

        /// <summary>
        /// Find method.
        /// </summary>
        /// <param name="conditionsList">filter</param>
        /// <returns>product</returns>
        IEnumerable<ProductDto> Find(List<ProductSearchModel> conditionsList);

        /// <summary>
        /// Get list purshased products
        /// </summary>
        /// <param name="userId">users id</param>
        /// <returns>return DTO whith product name and original product byte array</returns>
        IEnumerable<CompressedProductDTO> GetListPurshasedProducts(long userId);

        /// <summary>
        /// Get original purshased product
        /// </summary>
        /// <param name="userId">users id</param>
        OriginalProductDTO GetOriginalPurshasedProduct(long userId, long productId);
    }
}
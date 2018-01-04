// <copyright file="IProductRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MediaShop.Common.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MediaShop.Common.Models.Content;

    /// <summary>
    /// Class IProductRepository.
    /// </summary>
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Query products.
        /// </summary>
        /// <returns>products</returns>
        IQueryable<Product> Products();
    }
}

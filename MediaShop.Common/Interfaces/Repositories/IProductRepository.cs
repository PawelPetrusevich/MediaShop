using System.Linq;

namespace MediaShop.Common.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
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

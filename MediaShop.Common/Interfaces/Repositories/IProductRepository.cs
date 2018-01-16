using System.Linq;

namespace MediaShop.Common.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MediaShop.Common.Models.Content;

    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// get original product from data base
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>product</returns>
        Product GetOriginalProduct(long id);

        /// <summary>
        /// get protected product from data base
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>product</returns>
        Product GetProtectedProduct(long id);

        /// <summary>
        /// get compressed product from data base
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>product</returns>
        Product GetCompressedProduct(long id);
    }
}

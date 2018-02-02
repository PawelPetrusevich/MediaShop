using System.Linq;
using MediaShop.Common.Dto.Product;

namespace MediaShop.Common.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MediaShop.Common.Models.Content;

    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// Soft delete
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Product SoftDelete(long id);
    }
}

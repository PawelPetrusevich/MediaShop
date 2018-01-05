namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MediaShop.Common.Models.CartModels;
    using MediaShop.Common.Interfaces.Repositories;

    /// <summary>
    /// Class ProductRepository
    /// </summary>
    public class ProductRepository : IProductRepository<Product>
    {
        public Product Add(Product model)
        {
            throw new NotImplementedException();
        }

        public Product Delete(Product model)
        {
            throw new NotImplementedException();
        }

        public Product Delete(ulong id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> Find(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Product Get(ulong id)
        {
            throw new NotImplementedException();
        }

        public Product Update(Product model)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MediaShop.Common.Models.Content;

namespace MediaShop.Common.Interfaces.Services
{
    public interface IProductService
    {
        Product Get(int id);

        Product Add(Product model);

        Product Update(Product model);

        Product Delete(Product model);

        Product Delete(int id);

        IEnumerable<Product> Find(Expression<Func<Product, bool>> filter);

        IEnumerable<Product> Products();

        List<Product> AddProductsList(IEnumerable<Product> products);

        List<Product> DeleteProductsList(IEnumerable<Product> products);
    }
}
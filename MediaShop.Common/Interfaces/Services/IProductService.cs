using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using MediaShop.Common.Dto;
using MediaShop.Common.Models.Content;

namespace MediaShop.Common.Interfaces.Services
{
    public interface IProductService
    {
        Product Get(int id);

        ProductDto Add(Product model);

        ProductDto Update(Product model);

        ProductDto Delete(Product model);

        ProductDto Delete(int id);

        IEnumerable<Product> Find(Expression<Func<Product, bool>> filter);

        IEnumerable<Product> Products();

        IEnumerable<ProductDto> Add(IEnumerable<Product> products);

        IEnumerable<ProductDto> Delete(IEnumerable<Product> products);
    }
}
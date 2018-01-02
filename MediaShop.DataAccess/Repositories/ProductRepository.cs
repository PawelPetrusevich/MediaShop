namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using MediaShop.Common.Dto;
    using MediaShop.Common.Interfaces.Repositories;

    /// <summary>
    /// Class ProductRepository
    /// </summary>
    public class ProductRepository : IProductRepository<ProductDto>
    {
        public ProductDto Add(ProductDto model)
        {
            throw new NotImplementedException();
        }

        public ProductDto Delete(ProductDto model)
        {
            throw new NotImplementedException();
        }

        public ProductDto Delete(ulong id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public IEnumerable<ProductDto> Find(Expression<Func<ProductDto, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public ProductDto Get(ulong id)
        {
            throw new NotImplementedException();
        }

        public ProductDto Update(ProductDto model)
        {
            throw new NotImplementedException();
        }
    }
}

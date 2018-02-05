using System.Data.Entity;
using MediaShop.Common.Dto.Product;
using MediaShop.DataAccess.Repositories.Base;

namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models.Content;
    using MediaShop.DataAccess.Context;

    /// <summary>
    /// Class ProductRepository.
    /// </summary>
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DbContext context)
            : base(context)
        {
        }

        public Product SoftDelete(long id)
        {
            using (Context)
            {
                var model = DbSet.SingleOrDefault(entity => entity.Id == id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    Context.SaveChanges();
                    return model;
                }
            }

            return default(Product);
        }

        public List<Product> GetListOnSale()
        {
            using (Context)
            {
                return DbSet.Where(entity => entity.IsDeleted == false).ToList();
            }

            return default(List<Product>);
        }
    }
}

using System.Data.Entity;
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

        public override Product Delete(long id)
        {
            var model = DbSet
                .Include(entity => entity.CompressedProduct)
                .Include(entity => entity.ProtectedProduct)
                .Include(entity => entity.OriginalProduct)
                .SingleOrDefault(entity => entity.Id == id);

            if (model != null)
            {
                using (Context)
                {
                    DbSet.Remove(model);
                    Context.SaveChanges();
                    return model;
                }
            }

            return default(Product);
        }
    }
}

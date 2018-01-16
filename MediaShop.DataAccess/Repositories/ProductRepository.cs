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

        /// <summary>
        /// get original product
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>db entry</returns>
        public Product GetOriginalProduct(long id)
        {
            return DbSet.Include(entity => entity.OriginalProduct).SingleOrDefault(entity => entity.Id == id);
        }

        /// <summary>
        /// get protected product
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>db entry</returns>
        public Product GetProtectedProduct(long id)
        {
            return DbSet.Include(entity => entity.ProtectedProduct).SingleOrDefault(entity => entity.Id == id);
        }

        /// <summary>
        /// get compressed product
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>db entry</returns>
        public Product GetCompressedProduct(long id)
        {
            return DbSet.Include(entity => entity.CompressedProduct).SingleOrDefault(entity => entity.Id == id);
        }
    }
}

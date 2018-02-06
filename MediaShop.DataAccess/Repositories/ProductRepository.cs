﻿using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
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
            var model = DbSet.SingleOrDefault(entity => entity.Id == id);

            if (model != null)
            {
                using (Context)
                {
                   model.IsDeleted = true;
                    Context.SaveChanges();
                    return model;
                }
            }

            return default(Product);
        }

        public virtual async Task<Product> AddAsync(Product model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            using (Context)
            {
                var result = DbSet.Add(model);
                await Context.SaveChangesAsync().ConfigureAwait(false);
                return result;
            }
        }

        public virtual async Task<Product> DeleteAsync(Product model)
        {
            if (DbSet.Contains(model))
            {
                using (Context)
                {
                    var result = DbSet.Remove(model);
                    await Context.SaveChangesAsync().ConfigureAwait(false);
                    return model;
                }
            }

            return default(Product);
        }

        public virtual async Task<Product> DeleteAsync(long id)
        {
            var model = DbSet.SingleOrDefault(x => x.Id == id);
            if (model == null)
            {
                using (Context)
                {
                    DbSet.Remove(model);
                    await Context.SaveChangesAsync().ConfigureAwait(false);
                    return model;
                }
            }

            return default(Product);
        }

        public virtual async Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException();
            }

            var result = await DbSet.Where(filter).ToListAsync().ConfigureAwait(false);
            return result;
        }

        public virtual async Task<Product> GetAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await DbSet.SingleOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }

        public async Task<Product> UpdateAsync(Product model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            using (Context)
            {
                Product entity = DbSet.SingleOrDefault(x => x.Id == model.Id);
                entity = Mapper.Map(model, entity);
                Context.Entry(entity).State = EntityState.Modified;
                await Context.SaveChangesAsync().ConfigureAwait(false);
                return entity;
            }
        }
    }
}

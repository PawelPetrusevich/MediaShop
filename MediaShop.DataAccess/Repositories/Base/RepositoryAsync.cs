using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models;

namespace MediaShop.DataAccess.Repositories.Base
{
    public class RepositoryAsync<T> : IRepositoryAsync<T>
        where T : Entity
    {
        protected readonly DbContext Context;

        protected readonly DbSet<T> DbSet;
        private bool _disposed;

        public RepositoryAsync(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        ~RepositoryAsync()
        {
            Dispose(false);
        }

        public virtual async Task<T> AddAsync(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            using (Context)
            {
                var result = DbSet.Add(model);
                await Context.SaveChangesAsync();
                return result;
            }
        }

        public virtual async Task<T> DeleteAsync(T model)
        {
            if (DbSet.Contains(model))
            {
                using (Context)
                {
                    var result = DbSet.Remove(model);
                    await Context.SaveChangesAsync();
                    return model;
                }
            }

            return default(T);
        }

        public virtual async Task<T> DeleteAsync(long id)
        {
            var model = DbSet.SingleOrDefault(x => x.Id == id);
            if (model == null)
            {
                using (Context)
                {
                    DbSet.Remove(model);
                    await Context.SaveChangesAsync();
                    return model;
                }
            }

            return default(T);
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException();
            }

            var result = await DbSet.Where(filter).ToListAsync();
            return result;
        }

        public virtual async Task<T> GetAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await DbSet.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> UpdateAsync(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            using (Context)
            {
                T entity = DbSet.SingleOrDefault(x => x.Id == model.Id);
                entity = Mapper.Map(model, entity);
                Context.Entry(entity).State = EntityState.Modified;
                await Context.SaveChangesAsync();
                return entity;
            }
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="flag"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool flag)
        {
            if (_disposed)
            {
                return;
            }

            Context.Dispose();
            _disposed = true;

            if (flag)
            {
                GC.SuppressFinalize(this);
            }
        }
    }
}

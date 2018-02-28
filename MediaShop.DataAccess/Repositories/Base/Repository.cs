// <copyright file="Repository.cs" company="MediaShop">
// Copyright (c) MediaShop. All rights reserved.
// </copyright>

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models;
    using MediaShop.DataAccess.Properties;

namespace MediaShop.DataAccess.Repositories.Base
{
    /// <summary>
    /// Class Repository.
    /// </summary>
    /// <typeparam name="T">Entity</typeparam>
    /// <seealso cref="System.IDisposable" />
    /// <seealso cref="MediaShop.Common.Interfaces.Repositories.IRepository{T}" />
    public class Repository<T> : IRepository<T>, IRepositoryAsync<T>
        where T : Entity
    {
        /// <summary>
        /// The context
        /// </summary>
        protected readonly DbContext Context;

        /// <summary>
        /// The db set
        /// </summary>
        protected readonly DbSet<T> DbSet;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<T>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        ~Repository()
        {
            Dispose(false);
        }

        /// <summary>
        /// Method Get
        /// gets user by ID
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>db entry</returns>
        public virtual T Get(long id)
        {
            if (id <= 0)
        {
                throw new ArgumentException(Resources.InvalidIdValue);
            }

            return DbSet.SingleOrDefault(entity => entity.Id == id);
        }

        /// Method Get
        /// gets user by ID
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>db entry</returns>
        public virtual async Task<T> GetAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Resources.InvalidIdValue);
            }

            var result = await DbSet.SingleOrDefaultAsync(entity => entity.Id == id).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// add model to repository
        /// </summary>
        /// <param name="model">model user</param>
        /// <returns>db entry</returns>
        /// <exception cref="ArgumentNullException">if model = null</exception>
        public virtual T Add(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var result = DbSet.Add(model);
            Context.SaveChanges();
            return result;
        }

        /// <summary>
        /// add model to repository
        /// </summary>
        /// <param name="model">model user</param>
        /// <returns>db entry</returns>
        /// <exception cref="ArgumentNullException">if model = null</exception>
        public virtual async Task<T> AddAsync(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var result = DbSet.Add(model);
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// interface method update for type TModel
        /// </summary>
        /// <param name="model">Model to update</param>
        /// <returns>Updated model</returns>
        public virtual T Update(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            T entity = Get(model.Id);

            entity = Mapper.Map(model, entity);
            Context.Entry(entity).State = EntityState.Modified;

            Context.SaveChanges();
            return entity;
        }

        /// <summary>
        /// interface method update for type TModel
        /// </summary>
        /// <param name="model">Model to update</param>
        /// <returns>Updated model</returns>
        public virtual async Task<T> UpdateAsync(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            T entity = Get(model.Id);

            entity = Mapper.Map(model, entity);
            Context.Entry(entity).State = EntityState.Modified;

            await Context.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        /// <summary>
        /// interface method delete for type TModel by model
        /// </summary>
        /// <param name="model">Model to delete</param>
        /// <returns>Instance of deleted model</returns>
        public virtual T Delete(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (DbSet.Contains(model))
            {
                DbSet.Remove(model);
                Context.SaveChanges();
                return model;
            }

            return default(T);
        }

        /// <summary>
        /// interface method delete for type TModel by model
        /// </summary>
        /// <param name="model">Model to delete</param>
        /// <returns>Instance of deleted model</returns>
        public virtual async Task<T> DeleteAsync(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (DbSet.Contains(model))
            {
                DbSet.Remove(model);
                await Context.SaveChangesAsync().ConfigureAwait(false);
                return model;
            }

            return default(T);
        }

        /// <summary>
        /// interface method delete for type TModel by id
        /// </summary>
        /// <param name="id">Id of deleting model</param>
        /// <returns>Deleted model</returns>
        public virtual T Delete(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Resources.InvalidIdValue);
            }

            var model = DbSet.SingleOrDefault(entity => entity.Id == id);

            if (model != null)
            {
                DbSet.Remove(model);
                Context.SaveChanges();
                return model;
            }

            return default(T);
        }

        /// <summary>
        /// interface method delete for type TModel by id
        /// </summary>
        /// <param name="id">Id of deleting model</param>
        /// <returns>Deleted model</returns>
        public virtual async Task<T> DeleteAsync(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Resources.InvalidIdValue);
            }

            var model = await DbSet.SingleOrDefaultAsync(entity => entity.Id == id);

            if (model != null)
            {
                DbSet.Remove(model);
                await Context.SaveChangesAsync().ConfigureAwait(false);
                return model;
            }

            return default(T);
        }

        /// <summary>
        /// Find by id
        /// </summary>
        /// <param name="filter">Filter criteria</param>
        /// <returns>Suitable entities</returns>
        /// <exception cref="ArgumentNullException">filter</exception>
        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return DbSet.Where(filter).ToList();
        }

        /// <summary>
        /// Find by id
        /// </summary>
        /// <param name="filter">Filter criteria</param>
        /// <returns>Suitable entities</returns>
        /// <exception cref="ArgumentNullException">filter</exception>
        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var result = await DbSet.Where(filter).ToListAsync().ConfigureAwait(false);

            return result;
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

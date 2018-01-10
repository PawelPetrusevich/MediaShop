﻿namespace MediaShop.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Models;
    using MediaShop.DataAccess.Context;

    /// <summary>
    /// Class for work with repository
    /// </summary>
    public class CartRepository : ICartRepository<ContentCart>, IDisposable
    {
        protected readonly DbContext DbContext;

        protected readonly DbSet<ContentCart> DbSet;

        private bool disposed;

        public CartRepository(DbContext context)
        {
            this.DbContext = context;
            this.DbSet = this.DbContext.Set<ContentCart>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="CartRepository"/> class.
        /// </summary>
        ~CartRepository()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Method for add object type ContentCartDto
        /// </summary>
        /// <param name="model">updating object</param>
        /// <returns>rezalt operation</returns>
        public ContentCart Add(ContentCart model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            var result = this.DbSet.Add(model);
            this.DbContext.SaveChanges();
            return result;
        }

        /// <summary>
        /// Method for update object type ContentCartDto
        /// </summary>
        /// <param name="model">updating object</param>
        /// <returns>rezalt operation</returns>
        public ContentCart Update(ContentCart model)
        {
            this.DbContext.Entry(model).State = EntityState.Modified;
            this.DbContext.SaveChanges();
            return model;
        }

        /// <summary>
        /// Method for delete object type ContentCartDto
        /// </summary>
        /// <param name="model">object for delete</param>
        /// <returns>rezalt operation</returns>
        public ContentCart Delete(ContentCart model)
        {
            var result = this.DbSet.Remove(model);
            this.DbContext.SaveChanges();
            return result;
        }

        /// <summary>
        /// Method for delete object type ContentCartDto
        /// </summary>
        /// <param name="id">id for delete</param>
        /// <returns>rezalt operation</returns>
        public ContentCart Delete(long id)
        {
            var contentCart = this.DbSet.Where(x => x.Id == id
                && x.StateContent == Common.Enums.CartEnums.StateCartContent.InCart).SingleOrDefault();
            var result = this.DbSet.Remove(contentCart);
            this.DbContext.SaveChanges();
            return result;
        }

        /// <summary>
        /// Method for find collection of object type ContentCartDto
        /// by predicate
        /// </summary>
        /// <param name="filter">predicate</param>
        /// <returns>collection objects</returns>
        public IEnumerable<ContentCart> Find(Expression<Func<ContentCart, bool>> filter)
        {
            var result = this.DbSet.Where(filter);
            return result;
        }

        /// <summary>
        /// Method for getting object type ContentCartDto
        /// by identificator
        /// </summary>
        /// <param name="id">identificator</param>
        /// <returns>rezalt operation</returns>
        public ContentCart Get(long id)
        {
            return this.DbSet.Where(x => x.Id == id).SingleOrDefault();
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        /// <param name="flag">flag from where the method was called releases resources</param>
        protected virtual void Dispose(bool flag)
        {
            if (!this.disposed)
            {
                this.DbContext.Dispose();
                this.disposed = true;
                if (flag)
                {
                    GC.SuppressFinalize(this); // tell Garbage collector not to call the finalizer for the current object
                }
            }
        }
    }
}
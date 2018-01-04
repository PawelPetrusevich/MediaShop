// <copyright file="ProductRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
    public class ProductRepository : IProductRepository, IDisposable
    {
        private MediaContext productContext;
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        public ProductRepository()
        {
            this.productContext = new MediaContext();
        }

        /// <summary>
        /// Add method.
        /// </summary>
        /// <param name="model">Product model</param>
        /// <returns>result</returns>
        public Product Add(Product model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            var result = this.productContext.Products.Add(model);
            this.productContext.SaveChanges();
            return result;
        }

        /// <summary>
        /// Delete method.
        /// </summary>
        /// <param name="model">Product model</param>
        /// <returns>result</returns>
        public Product Delete(Product model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            var result = this.productContext.Products.Remove(model);
            this.productContext.SaveChanges();
            return result;
        }

        /// <summary>
        /// Delete by Id method.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>result</returns>
        public Product Delete(int id)
        {
            var model = this.productContext.Products.Single(x => x.Id == id);
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            var result = this.productContext.Products.Remove(model);
            return result;
        }

        /// <summary>
        /// Find product.
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>result</returns>
        public IEnumerable<Product> Find(Expression<Func<Product, bool>> filter)
        {
            var result = this.productContext.Products.Where(filter);
            return result;
        }

        /// <summary>
        /// Get method.
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Id</returns>
        public Product Get(int id)
        {
            return this.productContext.Products.Single(x => x.Id == id);
        }

        /// <summary>
        /// Query products.
        /// </summary>
        /// <returns>result</returns>
        public IQueryable<Product> Products()
        {
            var result = this.productContext.Products.AsQueryable();
            return result;
        }

        /// <summary>
        /// Update method.
        /// </summary>
        /// <param name="model">Product model</param>
        /// <returns>model</returns>
        public Product Update(Product model)
        {
            this.productContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
            this.productContext.SaveChanges();

            return model;
        }

        /// <summary>
        /// Dispose pattern realisation.
        /// </summary>
        /// <param name="disposing">flag disposing</param>
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.productContext.Dispose();
                }
            }

            this.disposed = true;
        }

        /// <summary>
        /// Dispose method.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

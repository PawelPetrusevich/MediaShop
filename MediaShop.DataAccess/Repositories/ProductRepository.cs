using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.Content;
using MediaShop.DataAccess.Context;

namespace MediaShop.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private MediaContext productContext;
        private bool disposed = false;

        public ProductRepository()
        {
            this.productContext = new MediaContext();
        }

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

        public IEnumerable<Product> Find(Expression<Func<Product, bool>> filter)
        {
            var result = this.productContext.Products.Where(filter);
            return result;
        }

        public Product Get(int id)
        {
            return this.productContext.Products.Single(x => x.Id == id);
        }

        public IQueryable<Product> Products()
        {
            var result = this.productContext.Products.AsQueryable();
            return result;
        }

        public Product Update(Product model)
        {
            this.productContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
            this.productContext.SaveChanges();

            return model;
        }

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

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

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
    public class ProductRepository : IProductRepository
    {
        public Product Add(Product model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            using (var productContext = new MediaContext())
            {
                var result = productContext.Products.Add(model);
                productContext.SaveChanges();
                return result;
            }
        }

        public Product Delete(Product model)
        {
            if (model == null)
            {
                throw new ArgumentNullException();
            }

            using (var productContext = new MediaContext())
            {
                var result = productContext.Products.Remove(model);
                productContext.SaveChanges();
                return result;
            }
        }

        public Product Delete(int id)
        {
            using (var productContext = new MediaContext())
            {
                var model = productContext.Products.Single(x => x.Id == id);
                if (model == null)
                {
                    throw new ArgumentNullException();
                }

                var result = productContext.Products.Remove(model);
                return result;
            }
        }

        public IEnumerable<Product> Find(Expression<Func<Product, bool>> filter)
        {
            using (var productContext = new MediaContext())
            {
                var result = productContext.Products.Where(filter);
                return result;
            }
        }

        public Product Get(int id)
        {
            using (var productContext = new MediaContext())
            {
                return productContext.Products.Single(x => x.Id == id);
            }
        }

        public IQueryable<Product> Products()
        {
            using (var productContext = new MediaContext())
            {
                var result = productContext.Products.AsQueryable();
                return result;
            }
        }

        public Product Update(Product model)
        {
            using (var productContext = new MediaContext())
            {
                productContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                productContext.SaveChanges();
            }

            return model;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.Content;

namespace MediaShop.BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;

        public ProductService(IProductRepository repository)
        {
            this.repository = repository;
        }

        public Product Add(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }

            this.repository.Add(product);

            return product;
        }

        public Product Delete(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }

            this.repository.Delete(product);

            return product;
        }

        public Product Delete(int id)
        {
            return this.repository.Delete(id);
        }

        public IEnumerable<Product> Find(Expression<Func<Product, bool>> filter)
        {
            return this.repository.Find(filter);
        }

        public Product Get(int id)
        {
            return this.repository.Get(id);
        }

        public Product Update(Product product)
        {
            if (product == null)
            {
                throw new NullReferenceException();
            }

            this.repository.Update(product);

            return product;
        }

        public IEnumerable<Product> Products()
        {
            return this.repository.Products();
        }
    }
}

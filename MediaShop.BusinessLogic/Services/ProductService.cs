using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
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

        /// <summary>
        /// метод добовления продукта
        /// </summary>
        /// <param name="product">принимае экземпляр product</param>
        /// <returns>возрощаем product</returns>
        public Product Add(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }

            this.repository.Add(product);

            return product;
        }

        /// <summary>
        /// метод удаления продукта
        /// </summary>
        /// <param name="product">передаем product</param>
        /// <returns>возвращаем product</returns>
        public Product Delete(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }

            this.repository.Delete(product);

            return product;
        }

        /// <summary>
        /// метод удаления продукта по id
        /// </summary>
        /// <param name="id">передаем id product</param>
        /// <returns>возрощаем product</returns>
        public Product Delete(int id)
        {
            return this.repository.Delete(id);
        }

        /// <summary>
        /// поиск согласно уловию
        /// </summary>
        /// <param name="filter">принимаем условие</param>
        /// <returns>возрощаем список product</returns>
        public IEnumerable<Product> Find(Expression<Func<Product, bool>> filter)
        {
            return this.repository.Find(filter);
        }

        /// <summary>
        /// найти продукт по id
        /// </summary>
        /// <param name="id">передаем id</param>
        /// <returns>возращаем product</returns>
        public Product Get(int id)
        {
            return this.repository.Get(id);
        }

        /// <summary>
        /// обновление записи
        /// </summary>
        /// <param name="product">передаем product</param>
        /// <returns>return product</returns>
        public Product Update(Product product)
        {
            if (product == null)
            {
                throw new NullReferenceException();
            }

            this.repository.Update(product);

            return product;
        }

        /// <summary>
        /// получаем список продуктов
        /// </summary>
        /// <returns>список product</returns>
        public IEnumerable<Product> Products()
        {
            return this.repository.Products();
        }

        /// <summary>
        /// добовляем список продуктов в таблицу
        /// </summary>
        /// <param name="products">передаем список продуктов</param>
        /// <returns>возрощаем статус код</returns>
        public HttpStatusCode AddProductsList(IEnumerable<Product> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var product in products)
            {
                this.repository.Add(product);
            }

            return HttpStatusCode.OK;
        }

        /// <summary>
        /// удаляем список продуктов
        /// </summary>
        /// <param name="products">передаем  список продуктов</param>
        /// <returns>возрощаем статус код</returns>
        public HttpStatusCode DeleteProductsList(IEnumerable<Product> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var product in products)
            {
                this.repository.Delete(product);
            }

            return HttpStatusCode.OK;
        }
    }
}

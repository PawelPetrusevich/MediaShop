// <copyright file="ProductService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace MediaShop.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Net;
    using AutoMapper;
    using MediaShop.Common.Dto;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models.Content;

    /// <summary>
    /// class ProductService.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="repository">repository</param>
        public ProductService(IProductRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// метод добовления продукта
        /// </summary>
        /// <param name="product">принимае экземпляр product</param>
        /// <returns>возрощаем product</returns>
        public ProductDto Add(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }

            var result = this.repository.Add(product);

            return result is null ? throw new InvalidOperationException() : Mapper.Map<Product, ProductDto>(result);
        }

        /// <summary>
        /// метод удаления продукта
        /// </summary>
        /// <param name="product">передаем product</param>
        /// <returns>возвращаем product</returns>
        public ProductDto Delete(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }

            var result = this.repository.Delete(product);

            return result is null ? throw new InvalidOperationException() : Mapper.Map<Product, ProductDto>(result);
        }

        /// <summary>
        /// метод удаления продукта по id
        /// </summary>
        /// <param name="id">передаем id product</param>
        /// <returns>возрощаем product</returns>
        public ProductDto Delete(long id)
        {
            var result = this.repository.Delete(id);

            return result is null ? throw new InvalidOperationException() : Mapper.Map<Product, ProductDto>(result);
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
        public Product Get(long id)
        {
            return this.repository.Get(id);
        }

        /// <summary>
        /// обновление записи
        /// </summary>
        /// <param name="product">передаем product</param>
        /// <returns>return product</returns>
        public ProductDto Update(Product product)
        {
            if (product == null)
            {
                throw new NullReferenceException();
            }

            var result = this.repository.Update(product);

            return result is null ? throw new InvalidOperationException() : Mapper.Map<Product, ProductDto>(result);
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
        public IEnumerable<ProductDto> Add(IEnumerable<Product> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException();
            }

            ICollection<ProductDto> resultsCollection = new List<ProductDto>();
            foreach (var product in products)
            {
                var result = this.repository.Add(product);

                if (result != null)
                {
                    resultsCollection.Add(Mapper.Map<Product, ProductDto>(result));
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            return resultsCollection;
        }

        /// <summary>
        /// удаляем список продуктов
        /// </summary>
        /// <param name="products">передаем  список продуктов</param>
        /// <returns>возрощаем статус код</returns>
        public IEnumerable<ProductDto> Delete(IEnumerable<Product> products)
        {
            if (products == null)
            {
                throw new ArgumentNullException();
            }

            ICollection<ProductDto> resultsCollection = new List<ProductDto>();
            foreach (var product in products)
            {
                var result = this.repository.Delete(product);

                if (result != null)
                {
                    resultsCollection.Add(Mapper.Map<Product, ProductDto>(result));
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            return resultsCollection;
        }
    }
}

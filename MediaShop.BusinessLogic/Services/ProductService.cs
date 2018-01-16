// <copyright file="ProductService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.Linq;
using System.Text;
using MediaShop.BusinessLogic.ExtensionMethods;
using MediaShop.Common.Dto.Product;
using MediaShop.Common.Enums;

namespace MediaShop.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq.Expressions;
    using AutoMapper;
    using MediaShop.BusinessLogic.Properties;
    using MediaShop.Common.Dto;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models.Content;

    /// <summary>
    /// class ProductService.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="repository">repository</param>
        public ProductService(IProductRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Загрузка файла на сервер
        /// </summary>
        /// <param name="uploadModels">Модель формы загрузки</param>
        /// <returns>Возрощаем модель для отоброжения</returns>
        public ProductDto UploadProducts(UploadModel uploadModels)
        {
            var data = Mapper.Map<Product>(uploadModels);
            var uploadProductInByte = Convert.FromBase64String(uploadModels.UploadProduct);
            data.ProductType = uploadProductInByte.GetMimeFromFile();

            if (string.IsNullOrEmpty(uploadModels.UploadProduct))
            {
                throw new ArgumentNullException(Resources.NullOrEmptyContent);
            }
            else
            {
                switch (data.ProductType)
                {
                    case ProductType.Image:
                        data.OriginalProduct.Content = uploadProductInByte;
                        data.CompressedProduct.Content = uploadProductInByte.GetCompressedImage();
                        data.ProtectedProduct.Content = uploadProductInByte.GetProtectedImage();
                        break;
                    case ProductType.unknow:
                        throw new ArgumentException(Resources.UnknowProductType);
                }
            }

            var result = this._repository.Add(data);

            return result is null ? throw new InvalidOperationException() : Mapper.Map<Product, ProductDto>(result);
        }

        /// <summary>
        /// метод удаления продукта
        /// </summary>
        /// <param name="id">id of product</param>
        /// <returns>ProductDto</returns>
        public ProductDto DeleteProduct(long id)
        {
            if (id <= 0)
            {
                throw new InvalidOperationException(Resources.DeleteWithNullId);
            }

            var result = this._repository.Delete(id);

            return result is null ? throw new InvalidOperationException() : Mapper.Map<Product, ProductDto>(result);
        }

        /// <summary>
        /// поиск согласно уловию - еще не написан
        /// </summary>
        /// <param name="filter">принимаем условие</param>
        /// <returns>возрощаем список product</returns>
        public IEnumerable<Product> Find(Expression<Func<Product, bool>> filter)
        {
            //еще не написан
            return this._repository.Find(filter);
        }

        /// <summary>
        /// Получить оригинальный контент по id
        /// </summary>
        /// <param name="id">передаем id</param>
        /// <returns>возращаем ProductContentDTO</returns>
        public ProductContentDTO GetOriginalProduct(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Resources.GetWithNullId);
            }

            var result = this._repository.GetOriginalProduct(id);

            if (result is null)
            {
                throw new InvalidOperationException();
            }

            var productContent = Mapper.Map<Product, ProductContentDTO>(result);
            productContent.Content = Convert.ToBase64String(result.OriginalProduct.Content);

            return productContent;
        }

        /// <summary>
        /// Получить защищенный контент по id
        /// </summary>
        /// <param name="id">передаем id</param>
        /// <returns>возращаем ProductContentDTO</returns>
        public ProductContentDTO GetProtectedProduct(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Resources.GetWithNullId);
            }

            var result = this._repository.GetProtectedProduct(id);

            if (result is null)
            {
                throw new InvalidOperationException();
            }

            var productContent = Mapper.Map<Product, ProductContentDTO>(result);
            productContent.Content = Convert.ToBase64String(result.ProtectedProduct.Content);

            return productContent;
        }

        /// <summary>
        /// Получить уменьшенную копию контента по id
        /// </summary>
        /// <param name="id">передаем id</param>
        /// <returns>возращаем ProductContentDTO</returns>
        public ProductContentDTO GetCompressedProduct(long id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(Resources.GetWithNullId);
            }

            var result = this._repository.GetCompressedProduct(id);

            if (result is null)
            {
                throw new InvalidOperationException();
            }

            var productContent = Mapper.Map<Product, ProductContentDTO>(result);
            productContent.Content = Convert.ToBase64String(result.CompressedProduct.Content);

            return productContent;
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

            var result = this._repository.Update(product);

            return result is null ? throw new InvalidOperationException() : Mapper.Map<Product, ProductDto>(result);
        }
    }
}

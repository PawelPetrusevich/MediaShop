﻿// <copyright file="ProductService.cs" company="PlaceholderCompany">
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
        /// <returns>Возвращаем модель для отображения</returns>
        public ProductDto UploadProducts(UploadProductModel uploadModels)
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
                data.OriginalProduct = new OriginalProduct();
                data.CompressedProduct = new CompressedProduct();
                data.ProtectedProduct = new ProtectedProduct();

                switch (data.ProductType)
                {
                    case ProductType.Image:
                        data.OriginalProduct.Content = uploadProductInByte;
                        data.CompressedProduct.Content = uploadProductInByte.GetCompressedImage();
                        data.ProtectedProduct.Content = uploadProductInByte.GetProtectedImage();
                        break;
                    case ProductType.Music:
                        data.OriginalProduct.Content = uploadProductInByte;
                        data.CompressedProduct.Content = null;
                        data.ProtectedProduct.Content = uploadProductInByte.GetProtectedMusic();
                        break;
                    case ProductType.unknow:
                        throw new ArgumentException(Resources.UnknowProductType);
                }
            }

            var result = this._repository.Add(data);

            return result is null ? throw new InvalidOperationException(Resources.UploadProductError) : Mapper.Map<ProductDto>(result);
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

            return result is null ? throw new InvalidOperationException(Resources.DeleteProductError) : Mapper.Map<ProductDto>(result);
        }

        /// <summary>
        /// поиск согласно уловию 
        /// </summary>
        /// <param name="conditionsList">принимаем условие</param>
        /// <returns>возрощаем список product</returns>
        public IEnumerable<ProductDto> Find(List<ProductSearchModel> conditionsList)
        {
            var operations = new List<Expression>();
            var parameterExpr = Expression.Parameter(typeof(Product));

            foreach (var condition in conditionsList)
            {
                var parameter = Expression.Property(parameterExpr, condition.LeftValue);

                var propType = typeof(Product).GetProperty(condition.LeftValue).PropertyType;
                var castValue = Convert.ChangeType(condition.RightValue, propType);
                ConstantExpression constant = Expression.Constant(castValue);
                switch (condition.Operand)
                {
                    case "=":
                        operations.Add(Expression.Equal(parameter, constant));
                        break;
                    case ">":
                        operations.Add(Expression.GreaterThan(parameter, constant));
                        break;
                    case "<":
                        operations.Add(Expression.LessThan(parameter, constant));
                        break;
                    case "Contains":
                        {
                            var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                            operations.Add(Expression.Call(parameter, method, constant));
                            break;
                        }
                }
            }

            var resultFilter = operations.Aggregate(Expression.And);

            var lambda = Expression.Lambda<Func<Product, bool>>(resultFilter, parameterExpr);

            return Mapper.Map<List<ProductDto>>(this._repository.Find(lambda));
        }

        /// <summary>
        /// Получение информации по ID
        /// </summary>
        /// <param name="id">id of product</param>
        /// <returns>ProductDto</returns>
        public ProductDto GetProduct(long id)
        {
            if (id <= 0)
            {
                throw new InvalidOperationException(Resources.GetWithNullId);
            }

            var result = this._repository.Get(id);

            return result is null ? throw new InvalidOperationException(Resources.GetProductError) : Mapper.Map<ProductDto>(result);
        }

        /// <summary>
        /// Download service
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>return Download product dto</returns>
        public DownloadProductDto DownloadProduct(long id)
        {
            if (id <= 0)
            {
                throw new InvalidOperationException(Resources.GetWithNullId);
            }

            var result = this._repository.Get(id);

            return result is null
                ? throw new InvalidOperationException(Resources.GetProductError)
                : Mapper.Map<DownloadProductDto>(result);
        }
    }
}

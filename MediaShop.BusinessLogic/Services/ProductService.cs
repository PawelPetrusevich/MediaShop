// <copyright file="ProductService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using MediaShop.BusinessLogic.ExtensionMethods;
using MediaShop.Common.Dto.Product;
using MediaShop.Common.Enums;
using MediaShop.Common.Models;

namespace MediaShop.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using AutoMapper;
    using MediaShop.BusinessLogic.Properties;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models.Content;

    /// <summary>
    /// class ProductService.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICartRepository _cartRepository;
        private readonly IAccountRepository _accountRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="repository">repository</param>
        public ProductService(IProductRepository repository, ICartRepository cartRepository, IAccountRepository accountRepository)
        {
            this._repository = repository;
            this._cartRepository = cartRepository;
            this._accountRepository = accountRepository;
        }

        /// <summary>
        /// Загрузка файла на сервер
        /// </summary>
        /// <param name="uploadModels">Модель формы загрузки</param>
        /// <returns>Возвращаем модель для отображения</returns>
        public ProductDto UploadProducts(UploadProductModel uploadModels, long creatorId)
        {
            var data = Mapper.Map<Product>(uploadModels);
            var uploadProductInByte = Convert.FromBase64String(uploadModels.UploadProduct);
            data.ProductType = uploadProductInByte.GetMimeFromByteArray();
            if (this._accountRepository.Get(creatorId) == null)
            {
                throw new ArgumentException(Resources.UserNotFound);
            }

            data.CreatorId = creatorId;

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
                        data.CompressedProduct.Content = Convert.FromBase64String(Resources.CompressedAudio);
                        data.ProtectedProduct.Content = uploadProductInByte.GetProtectedMusic();
                        break;
                    case ProductType.Video:
                        data.OriginalProduct.Content = uploadProductInByte;
                        data.ProtectedProduct.Content = uploadProductInByte.GetProtectedVideoAsync(HttpContext.Current);
                        data.CompressedProduct.Content = uploadProductInByte.GetCompresedVideoFrameAsync(HttpContext.Current);
                        break;
                    case ProductType.unknow:
                        throw new ArgumentException(Resources.UnknowProductType);
                }
            }

            var result = this._repository.Add(data);

            return result is null ? throw new InvalidOperationException(Resources.UploadProductError) : Mapper.Map<ProductDto>(result);
        }

        /// <summary>
        /// Async service for upload product
        /// </summary>
        /// <param name="uploadModels">UploadProductModel</param>
        /// <returns>Task ProductDTO</returns>
        public async Task<ProductDto> UploadProductsAsync(UploadProductModel uploadModels, long creatorId)
        {
            var data = Mapper.Map<Product>(uploadModels);
            var uploadProductInByte = Convert.FromBase64String(uploadModels.UploadProduct);
            data.ProductType = uploadProductInByte.GetMimeFromByteArray();

            if (this._accountRepository.Get(creatorId) == null)
            {
                throw new ArgumentException(Resources.UserNotFound);
            }

            data.CreatorId = creatorId;

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
                        data.CompressedProduct.Content = Convert.FromBase64String(Resources.CompressedAudio);
                        data.ProtectedProduct.Content = uploadProductInByte.GetProtectedMusic();
                        break;
                    case ProductType.Video:
                        data.OriginalProduct.Content = uploadProductInByte;
                        data.ProtectedProduct.Content = uploadProductInByte.GetProtectedVideoAsync(HttpContext.Current);
                        data.CompressedProduct.Content = uploadProductInByte.GetCompresedVideoFrameAsync(HttpContext.Current);
                        break;
                    case ProductType.unknow:
                        throw new ArgumentException(Resources.UnknowProductType);
                }
            }

            var result = await this._repository.AddAsync(data);

            return result is null ? throw new InvalidOperationException(Resources.UploadProductError) : Mapper.Map<ProductDto>(result);
        }

        /// <summary>
        /// метод удаления продукта
        /// </summary>
        /// <param name="id">id of product</param>
        /// <returns>ProductDto</returns>
        public ProductDto SoftDeleteById(long id, long creatorId)
        {
            if (id <= 0)
            {
                throw new InvalidOperationException(Resources.DeleteWithNullId);
            }

            if (this._accountRepository.Get(creatorId) == null)
            {
                throw new ArgumentException(Resources.UserNotFound);
            }

            var currentProduct = _repository.Get(id);

            if (currentProduct is null)
            {
                throw new InvalidOperationException(Resources.GetProductError);
            }

            if (creatorId != currentProduct.CreatorId)
            {
                throw new InvalidOperationException(Resources.NoRootForDelete);
            }

            var result = _repository.SoftDelete(id);

            return result is null ? throw new InvalidOperationException(Resources.DeleteProductError) : Mapper.Map<ProductDto>(result);
        }

        /// <summary>
        /// метод удаления продукта
        /// </summary>
        /// <param name="id">id of product</param>
        /// <param name="creatorId">id создателя продукта</param>
        /// <returns>ProductDto</returns>
        public async Task<ProductDto> SoftDeleteByIdAsync(long id, long creatorId)
        {
            if (id <= 0)
            {
                throw new InvalidOperationException(Resources.DeleteWithNullId);
            }

            if (this._accountRepository.Get(creatorId) == null)
            {
                throw new ArgumentException(Resources.UserNotFound);
            }

            var currentProduct = this._repository.Get(id);
            if (currentProduct is null)
            {
                throw new InvalidOperationException(Resources.GetProductError);
            }

            if (creatorId != currentProduct.CreatorId)
            {
                throw new InvalidOperationException(Resources.NoRootForDelete);
            }

            var result = await this._repository.SoftDeleteAsync(id);

            return result is null ? throw new InvalidOperationException(Resources.DeleteProductError) : Mapper.Map<ProductDto>(result);
        }

        /// <summary>
        /// поиск согласно уловию 
        /// </summary>
        /// <param name="conditionsList">принимаем условие</param>
        /// <returns>возрощаем список product</returns>
        public IEnumerable<CompressedProductDTO> Find(List<ProductSearchModel> conditionsList)
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

            return Mapper.Map<List<CompressedProductDTO>>(this._repository.Find(lambda));
        }

        /// <summary>
        /// Async Find Methods
        /// </summary>
        /// <param name="conditionsList">принимаем условие</param>
        /// <returns>возрощаем список product</returns>
        public async Task<IEnumerable<CompressedProductDTO>> FindAsync(List<ProductSearchModel> conditionsList)
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

            var result = await this._repository.FindAsync(lambda);

            return result is null ? throw new ArgumentException() : Mapper.Map<List<CompressedProductDTO>>(result);
        }

        /// <summary>
        /// Get list purshased products
        /// </summary>
        /// <param name="userId">users id</param>
        /// <returns>Список куленого контента</returns>
        public IEnumerable<CompressedProductDTO> GetListPurshasedProducts(long userId)
        {
            if (userId <= 0)
            {
                throw new InvalidOperationException(Resources.LessThanOrEqualToZeroValue);
            }

            var operations = new List<Expression>();
            var parameterExpr = Expression.Parameter(typeof(ContentCart));

            // CreatorID = UserID
            var parameter = Expression.Property(parameterExpr, "CreatorID");
            ConstantExpression constant = Expression.Constant(userId);
            operations.Add(Expression.Equal(parameter, constant));

            // StateContent = inPaid
            parameter = Expression.Property(parameterExpr, "StateContent");
            constant = Expression.Constant(CartEnums.StateCartContent.InPaid);
            operations.Add(Expression.Equal(parameter, constant));

            var resultFilter = operations.Aggregate(Expression.And);
            var lambda = Expression.Lambda<Func<ContentCart, bool>>(resultFilter, parameterExpr);

            return Mapper.Map<List<CompressedProductDTO>>(this._cartRepository.Find(lambda));
        }

        public async Task<IEnumerable<CompressedProductDTO>> GetListPurshasedProductsAsync(long userId)
        {
            if (userId <= 0)
            {
                throw new InvalidOperationException(Resources.LessThanOrEqualToZeroValue);
            }

            var operations = new List<Expression>();
            var parameterExpr = Expression.Parameter(typeof(ContentCart));

            // CreatorID = UserID
            var parameter = Expression.Property(parameterExpr, "CreatorID");
            ConstantExpression constant = Expression.Constant(userId);
            operations.Add(Expression.Equal(parameter, constant));

            // StateContent = inPaid
            parameter = Expression.Property(parameterExpr, "StateContent");
            constant = Expression.Constant(CartEnums.StateCartContent.InPaid);
            operations.Add(Expression.Equal(parameter, constant));

            var resultFilter = operations.Aggregate(Expression.And);
            var lambda = Expression.Lambda<Func<ContentCart, bool>>(resultFilter, parameterExpr);

            return Mapper.Map<List<CompressedProductDTO>>(await Task.Run(() => this._cartRepository.Find(lambda)));
        }

        /// <summary>
        /// Get list products on sale
        /// </summary>
        /// <returns>Список продуктов для продажи</returns>
        public IEnumerable<CompressedProductDTO> GetListOnSale()
        {
            return Mapper.Map<List<CompressedProductDTO>>(this._repository.GetListOnSale());
        }

        /// <summary>
        /// Get list products on sale
        /// </summary>
        /// <returns>A <see cref="Task"/> список продуктов для продажи</returns>
        public async Task<IEnumerable<CompressedProductDTO>> GetListOnSaleAsync()
        {
            return Mapper.Map<List<CompressedProductDTO>>(await this._repository.GetListOnSaleAsync());
        }

        /// <summary>
        /// Get original purshased product
        /// </summary>
        /// <param name="userId">users id</param>
        public OriginalProductDTO GetOriginalPurshasedProduct(long userId, long productId)
        {
            if (userId <= 0)
            {
                throw new InvalidOperationException(Resources.LessThanOrEqualToZeroValue);
            }

            var operations = new List<Expression>();
            var parameterExpr = Expression.Parameter(typeof(ContentCart));

            // CreatorID in content cart = UserID
            var parameter = Expression.Property(parameterExpr, "CreatorID");
            ConstantExpression constant = Expression.Constant(userId);
            operations.Add(Expression.Equal(parameter, constant));

            // ProductId = productId
            parameter = Expression.Property(parameterExpr, "ProductId");
            constant = Expression.Constant(productId);
            operations.Add(Expression.Equal(parameter, constant));

            // StateContent = inPaid
            parameter = Expression.Property(parameterExpr, "StateContent");
            constant = Expression.Constant(CartEnums.StateCartContent.InPaid);
            operations.Add(Expression.Equal(parameter, constant));

            var resultFilter = operations.Aggregate(Expression.And);
            var lambda = Expression.Lambda<Func<ContentCart, bool>>(resultFilter, parameterExpr);
            var result = this._cartRepository.Find(lambda).FirstOrDefault();

            if (result == null)
            {
                throw new ArgumentNullException();
            }

            return Mapper.Map<OriginalProductDTO>(result);
        }

        /// <summary>
        /// Get original purshased product Async methods
        /// </summary>
        /// <param name="userId">users id</param>
        public async Task<OriginalProductDTO> GetOriginalPurshasedProductAsync(long userId, long productId)
        {
            if (userId <= 0)
            {
                throw new InvalidOperationException(Resources.LessThanOrEqualToZeroValue);
            }

            var operations = new List<Expression>();
            var parameterExpr = Expression.Parameter(typeof(ContentCart));

            // CreatorID = UserID
            var parameter = Expression.Property(parameterExpr, "CreatorID");
            ConstantExpression constant = Expression.Constant(userId);
            operations.Add(Expression.Equal(parameter, constant));

            // ProductId = productId
            parameter = Expression.Property(parameterExpr, "ProductId");
            constant = Expression.Constant(productId);
            operations.Add(Expression.Equal(parameter, constant));

            // StateContent = inPaid
            parameter = Expression.Property(parameterExpr, "StateContent");
            constant = Expression.Constant(CartEnums.StateCartContent.InPaid);
            operations.Add(Expression.Equal(parameter, constant));

            var resultFilter = operations.Aggregate(Expression.And);
            var lambda = Expression.Lambda<Func<ContentCart, bool>>(resultFilter, parameterExpr);
            var result = await Task.Run(() => this._cartRepository.Find(lambda).FirstOrDefault());

            if (result == null)
            {
                throw new ArgumentNullException();
            }

            return Mapper.Map<OriginalProductDTO>(result);
        }

        /// <summary>
        /// Получение информации по ID
        /// </summary>
        /// <param name="id">id of product</param>
        /// <returns>ProductDto</returns>
        public ProductInfoDto GetById(long id)
        {
            if (id <= 0)
            {
                throw new InvalidOperationException(Resources.GetWithNullId);
            }

            var result = this._repository.Get(id);

            return result is null ? throw new InvalidOperationException(Resources.GetProductError) : Mapper.Map<ProductInfoDto>(result);
        }

        /// <summary>
        /// Get list purshased products
        /// </summary>
        /// <param name="userId">users id</param>
        public async Task<IEnumerable<CompressedProductDTO>> GetUploadProductListAsync(long userId)
        {
            if (userId <= 0)
            {
                throw new InvalidOperationException(Resources.LessThanOrEqualToZeroValue);
            }

            var result = await this._repository.FindAsync(x => (x.CreatorId == userId) && (x.IsDeleted == false));

            return Mapper.Map<List<CompressedProductDTO>>(result);
        }
    }
}

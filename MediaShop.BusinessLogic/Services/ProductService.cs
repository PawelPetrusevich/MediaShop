// <copyright file="ProductService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
        private readonly IProductRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="repository">repository</param>
        public ProductService(IProductRepository repository)
        {
            this.repository = repository;
        }

        public List<ProductDto> UploadProducts(IEnumerable<Product> data)
        {
            var returnProducts = new List<ProductDto>();

            // Вытащить данные из заголовка файла
            // 1.проверка валидации или null
            // 2.Создание защищеной
            // 3. создание уменьшеной
            // 4. Добавление

            foreach (var product in data)
            {
                try
                {
                    product.ProtectedProduct =
                        new ProtectedProduct()
                        {
                            File = GetProtectedImage(product.OriginalProduct.File),
                            Product = product,
                            ProductId = product.Id
                        };

                    product.CompressedProduct =
                        new CompressedProduct()
                        {
                            File = GetCompressedImage(product.OriginalProduct.File),
                            Product = product,
                            ProductId = product.Id
                        };
                }
                catch (Exception e)
                {
                    throw;
                }

                var result = repository.Add(product);
                if (!ReferenceEquals(result, null))
                {
                    returnProducts.Add(Mapper.Map<Product, ProductDto>(result));
                }
            }

            return returnProducts;
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

        public static byte[] GetProtectedImage(byte[] originalImageByte)
        {
            byte[] fileByte = new byte[1];
            if (!ReferenceEquals(originalImageByte, null))
            {
                fileByte = new byte[originalImageByte.Length];
                Array.Copy(originalImageByte, fileByte, originalImageByte.Length);
            }

            Bitmap originalImageBitmap;

            using (MemoryStream ms = new MemoryStream(fileByte))
            {
                originalImageBitmap = (Bitmap)Image.FromStream(ms);
            }

            var watermarkBitmap = Resources.WaterMark;
            using (Graphics gr = Graphics.FromImage(originalImageBitmap))
            {
                float opacity = (float)0.5;
                ImageAttributes attr = new ImageAttributes();
                ColorMatrix matrix = new ColorMatrix(new float[][]
                {
                    new float[] { opacity, 0f, 0f, 0f, 0f },
                    new float[] { 0f, opacity, 0f, 0f, 0f },
                    new float[] { 0f, 0f, opacity, 0f, 0f },
                    new float[] { 0f, 0f, 0f, opacity, 0f },
                    new float[] { 0f, 0f, 0f, 0f, opacity }
                });
                attr.SetColorMatrix(matrix);
                watermarkBitmap.MakeTransparent(Color.White);
                gr.DrawImage(watermarkBitmap, new Rectangle(0, 0, originalImageBitmap.Width, originalImageBitmap.Height), 0, 0, watermarkBitmap.Width, watermarkBitmap.Height, GraphicsUnit.Pixel, attr);
            }

            ImageConverter converter = new ImageConverter();
            var protectedImageByte = (byte[])converter.ConvertTo(originalImageBitmap, typeof(byte[]));

            return protectedImageByte;
        }

        public static byte[] GetCompressedImage(byte[] originalImage)
        {
            int compressedImageSize = 0;

            byte[] compressedImage = new byte[compressedImageSize];

            return compressedImage;
        }
    }
}

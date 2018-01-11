﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using MediaShop.BusinessLogic.Service;
using MediaShop.Common.Dto;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using SampleDataGenerator;
using Assert = NUnit.Framework.Assert;
using System.Linq.Expressions;
using MediaShop.BusinessLogic.ExtensionMethods;
using MediaShop.Common.Dto.Product;

namespace MediaShop.BusinessLogic.Tests.ProductTest
{

    [TestFixture]
    public class ProductServiceTest
    {

        private Mock<IProductRepository> _rep;

        private IProductRepository _mockRep;

        private ProductService _productService;

        private List<Product> _newProducts;

        public ProductServiceTest()
        {
            Mapper.Initialize(config => config.CreateMap<Product, ProductDto>());
        }

        [SetUp]
        public void Init()
        {
            _rep = new Mock<IProductRepository>();
            _rep.Setup(s => s.Add(It.IsAny<Product>())).Returns(new Product());
            _rep.Setup(s => s.Delete(It.IsAny<Product>())).Returns(new Product());
            _rep.Setup(s => s.Delete(It.IsAny<int>())).Returns(new Product());
            _rep.Setup(s => s.Get(It.IsAny<int>())).Returns(new Product());
            _rep.Setup(s => s.Update(It.IsAny<Product>())).Returns(new Product());
            _rep.Setup(x => x.Find(It.IsAny<Expression<Func<Product, bool>>>())).Returns(new List<Product>());

            _mockRep = _rep.Object;

            _productService = new ProductService(_mockRep);

            var productGenerator = Generator
                .For<Product>()
                .For(x => x.Id)
                .CreateUsing(() => new Random().Next())
                .For(x => x.CreatedDate)
                .CreateUsing(() => DateTime.Now)
                .For(x => x.CreatorId)
                .CreateUsing(() => new Random().Next())
                .For(x => x.ProductName)
                .ChooseFrom(StaticData.FirstNames)
                .For(x => x.ProductPrice)
                .CreateUsing(() => new Random().Next())
                .For(x => x.IsFavorite)
                .CreateUsing(() => false)
                .For(x => x.IsPremium)
                .CreateUsing(() => false);

            _newProducts = productGenerator.Generate(10).ToList();
        }

       [Test]
        public void AddProductTest()
        {  
            var returnProduct = _productService.Add(_newProducts[0]);

            Assert.IsNotNull(returnProduct);
        }

        [Test]
        public void AddProductListTest()
        {
            var returnProductList = _productService.Add(_newProducts);

            foreach (var returnProduct in returnProductList)
            {
                Assert.IsNotNull(returnProduct);
            }
        }

        [Test]
        public void GetProductTest()
        {
            _productService.Add(_newProducts[0]);
            var returnProduct = _productService.Get(0);

            Assert.IsNotNull(returnProduct);
        }

        [Test]
        public void DeleteProductTest()
        {
            _productService.Add(_newProducts[0]);
            var returnProduct = _productService.Delete(_productService.Get(0));

            Assert.IsNotNull(returnProduct);

        }

        [Test]
        public void DeleteProductListTest()
        {
            var returnProductList = _productService.Add(_newProducts);

            foreach (var returnProduct in returnProductList)
            {
                Assert.IsNotNull(returnProduct);
            }

        }

        [Test]
        public void DeleteProductByIdTest()
        {
            _productService.Add(_newProducts[0]);
            var returnProduct = _productService.Delete(0);

            Assert.IsNotNull(returnProduct);
        }

        

        [Test]
        public void UpdateProductTest()
        {
            _productService.Add(_newProducts[0]);
            var returnProduct = _productService.Update(_productService.Get(0));

            Assert.IsNotNull(returnProduct);
        }

        [Test]
        public void FindProductTest()
        {
            _productService.Add(new Product(){ProductName = "Image1"});

            Expression<Func<Product, bool>> filter = product => product.ProductName == "Image 1";
            var returnProducts = _productService.Find(filter);

            Assert.Less(0,returnProducts.Count());
        }

        [Test]
        public void WaterMarkAddTest()
        {
            FileStream stream = File.OpenRead(@"d:\1.jpg");
            var fileByte = new byte[stream.Length];
            stream.Read(fileByte, 0, fileByte.Length);
            stream.Close();

            var result = ExtensionProductMethods.GetProtectedImage(fileByte);

            using (Stream file = File.OpenWrite(@"d:\2.jpg"))
            {
                if (!ReferenceEquals(result, null))
                {
                    file.Write(result, 0, result.Length);
                }
            }
        }
    }
}

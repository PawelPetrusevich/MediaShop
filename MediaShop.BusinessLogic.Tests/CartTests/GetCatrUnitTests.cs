using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models;
using MediaShop.Common.Models.CartModels;
using MediaShop.Common;
using MediaShop.BusinessLogic.Services;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace MediaShop.BusinessLogic.Tests.CartTests
{
    /// <summary>
    /// Summary description for GetCatrUnitTests
    /// </summary>
    [TestClass]
    public class GetCatrUnitTests
    {
        // Field for Mock
        private Mock<ICartRepository<ContentCartDto>> mock;

        // Field for MockProduct
        private Mock<IProductRepository> mockProduct;

        [TestInitialize]
        public void Initialize()
        {
            Mapper.Reset();
            // Create Mapper for testing
            Mapper.Initialize(x =>
            {
                x.AddProfile<MapperProfile>();
            });

            // Create Mock
            var _mock = new Mock<ICartRepository<ContentCartDto>>();
            mock = _mock;
            var _mockProduct = new Mock<IProductRepository>();
            mockProduct = _mockProduct;
        }

        [TestMethod]
        public void Get_Cart()
        {
            var collectionItems = new Collection<ContentCartDto>()
            {
                new ContentCartDto { Id = 1, CreatorId = 1 , PriceItem = new decimal (9.99) },
                new ContentCartDto { Id = 2, CreatorId = 1 , PriceItem = new decimal (0.50) },
                new ContentCartDto { Id = 3, CreatorId = 1 , PriceItem = new decimal (1.01) }
            };
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCartDto, bool>>>()))
                .Returns(() => collectionItems);
            mock.Setup(item => item.GetAll(It.IsAny<long>()))
                .Returns(collectionItems);
            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object);

            var cart = service.GetCart(1);
            Assert.AreEqual((uint)3, cart.CountItemsInCollection);
            Assert.AreEqual(new decimal(11.50), cart.PriceAllItemsCollection);
            Assert.IsNotNull(cart.ContentCartDtoCollection);
        }

        [TestMethod]
        public void Get_EmptyCart()
        {
            var collectionItem = new Collection<ContentCartDto>();
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCartDto, bool>>>()))
                .Returns(() => collectionItem);

            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object);

            var cart = service.GetCart(1);
            Assert.AreEqual((uint)0, cart.CountItemsInCollection);
            Assert.AreEqual(new decimal(0), cart.PriceAllItemsCollection);
            Assert.IsNotNull(cart.ContentCartDtoCollection);
        }

        [TestMethod]
        public void Get_Price()
        {
            var collectionItems = new Collection<ContentCartDto>()
            {
                new ContentCartDto { Id = 1, CreatorId = 1 , PriceItem = new decimal (9.99) },
                new ContentCartDto { Id = 2, CreatorId = 1 , PriceItem = new decimal (0.50) },
                new ContentCartDto { Id = 3, CreatorId = 1 , PriceItem = new decimal (1.01) }
            };
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCartDto, bool>>>()))
                .Returns(() => collectionItems);
            mock.Setup(item => item.GetAll(It.IsAny<long>()))
                .Returns(collectionItems);
            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object);

            var price = service.GetPrice(5);
            Assert.AreEqual(new decimal(11.50), price);
        }

        [TestMethod]
        public void Get_Price_emptyCart()
        {
            var collectionItems = new Collection<ContentCartDto>();
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCartDto, bool>>>()))
                .Returns(() => collectionItems);
            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object);
            var price = service.GetPrice(5);
            Assert.AreEqual(0, price);
        }

        [TestMethod]
        public void Get_Count_Items_in_Cart()
        {
            var collectionItems = new Collection<ContentCartDto>()
            {
                new ContentCartDto { Id = 1, CreatorId = 1 , PriceItem = new decimal (9.99) },
                new ContentCartDto { Id = 2, CreatorId = 1 , PriceItem = new decimal (0.50) },
                new ContentCartDto { Id = 3, CreatorId = 1 , PriceItem = new decimal (1.01) }
            };
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCartDto, bool>>>()))
                .Returns(() => collectionItems);
            mock.Setup(item => item.GetAll(It.IsAny<long>()))
                .Returns(collectionItems);
            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object);
            var count = service.GetCountItems(5);
            Assert.AreEqual((uint)3, count);
        }

        [TestMethod]
        public void Get_Count_Items_in_emptyCart()
        {
            var collectionItems = new Collection<ContentCartDto>();
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCartDto, bool>>>()))
                .Returns(() => collectionItems);
            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object);
            var count = service.GetCountItems(5);
            Assert.AreEqual((uint)0, count);
        }

    }
}

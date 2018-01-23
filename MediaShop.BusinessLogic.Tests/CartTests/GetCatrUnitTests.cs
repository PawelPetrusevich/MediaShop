using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using MediaShop.Common.Interfaces.Services;
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
        private Mock<ICartRepository> mock;

        // Field for MockProduct
        private Mock<IProductRepository> mockProduct;

        // Field for MockPayment
        private Mock<IPaymentService> mockPayment;

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
            var _mock = new Mock<ICartRepository>();
            mock = _mock;
            var _mockProduct = new Mock<IProductRepository>();
            mockProduct = _mockProduct;
            var _mockPayment = new Mock<IPaymentService>();
            mockPayment = _mockPayment;
        }

        [TestMethod]
        public void Get_Cart()
        {
            var collectionItems = new Collection<ContentCart>()
            {
                new ContentCart { Id = 1, CreatorId = 1 , Product = new Product (){ ContentName = "Prod1", Id = 1, PriceItem = new decimal (9.99) } },
                new ContentCart { Id = 2, CreatorId = 1 , Product = new Product (){ ContentName = "Prod2", Id = 2, PriceItem = new decimal (0.50) } },
                new ContentCart { Id = 3, CreatorId = 1 , Product = new Product (){ ContentName = "Prod3", Id = 3, PriceItem = new decimal (1.01) } }
            };
            mock.Setup(item => item.GetAll(1))
                .Returns(() => collectionItems);
            mock.Setup(item => item.GetAll(It.IsAny<long>()))
                .Returns(collectionItems);
            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);

            var cart = service.GetCart(1);
            Assert.AreEqual((uint)3, cart.CountItemsInCollection);
            Assert.AreEqual(new decimal(11.50), cart.PriceAllItemsCollection);
            Assert.IsNotNull(cart.ContentCartDtoCollection);
        }

        [TestMethod]
        public void Get_EmptyCart()
        {
            var collectionItem = new Collection<ContentCart>();
            mock.Setup(item => item.GetAll(1))
                .Returns(() => collectionItem);

            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);

            var cart = service.GetCart(1);
            Assert.AreEqual((uint)0, cart.CountItemsInCollection);
            Assert.AreEqual(new decimal(0), cart.PriceAllItemsCollection);
            Assert.IsNotNull(cart.ContentCartDtoCollection);
        }

        [TestMethod]
        public void Get_Price()
        {
            var collectionItems = new Collection<ContentCart>()
            {
                new ContentCart { Id = 1, CreatorId = 1 , Product = new Product (){ ContentName = "Prod1", Id = 1, PriceItem = new decimal (9.99) } },
                new ContentCart { Id = 2, CreatorId = 1 , Product = new Product (){ ContentName = "Prod2", Id = 2, PriceItem = new decimal (0.50) } },
                new ContentCart { Id = 3, CreatorId = 1 , Product = new Product (){ ContentName = "Prod3", Id = 3, PriceItem = new decimal (1.01) } }
            };
            mock.Setup(item => item.GetAll(1))
                .Returns(() => collectionItems);
            mock.Setup(item => item.GetAll(It.IsAny<long>()))
                .Returns(collectionItems);
            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);

            var price = service.GetPrice(1);
            Assert.AreEqual(new decimal(11.50),price);           
        }

        [TestMethod]
        public void Get_Price_emptyCart()
        {
            var collectionItems = new Collection<ContentCart>();
            mock.Setup(item => item.GetAll(1))
                .Returns(() => collectionItems);
            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);
            var price = service.GetPrice(5);
            Assert.AreEqual(0, price);
        }

        [TestMethod]
        public void Get_Count_Items_in_Cart()
        {
            var collectionItems = new Collection<ContentCart>()
            {
                new ContentCart { Id = 1, CreatorId = 1 , Product = new Product (){ ContentName = "Prod1", Id = 1, PriceItem = new decimal (9.99) } },
                new ContentCart { Id = 2, CreatorId = 1 , Product = new Product (){ ContentName = "Prod2", Id = 2, PriceItem = new decimal (0.50) } },
                new ContentCart { Id = 3, CreatorId = 1 , Product = new Product (){ ContentName = "Prod3", Id = 3, PriceItem = new decimal (1.01) } }
            };
            mock.Setup(item => item.GetAll(1))
                .Returns(() => collectionItems);
            mock.Setup(item => item.GetAll(It.IsAny<long>()))
                .Returns(collectionItems);
            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);
            var count = service.GetCountItems(1);
            Assert.AreEqual((uint)3, count);
        }

        [TestMethod]
        public void Get_Count_Items_in_emptyCart()
        {
            var collectionItems = new Collection<ContentCart>();
            mock.Setup(item => item.GetAll(1))
                .Returns(() => collectionItems);
            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);
            var count = service.GetCountItems(1);
            Assert.AreEqual((uint)0, count);
        }

    }
}

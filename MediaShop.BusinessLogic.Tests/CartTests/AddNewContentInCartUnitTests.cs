using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using AutoMapper;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models;
using MediaShop.Common;
using MediaShop.Common.Exceptions.CartExceptions;
using MediaShop.BusinessLogic.Services;
using NUnit.Framework;
using MediaShop.Common.Models.Content;
using MediaShop.Common.Dto;
using MediaShop.Common.Dto.Messaging;

namespace MediaShop.BusinessLogic.Tests.CartTests
{
    [TestFixture]
    public class AddNewContentInCartUnitTests
    {
        // Field for Mock
        private Mock<ICartRepository> mock;

        // Field for MockProduct
        private Mock<IProductRepository> mockProduct;

        // Field for MockNotify
        private Mock<INotificationService> mockNotify;

        public AddNewContentInCartUnitTests()
        {
            Mapper.Reset();
            // Create Mapper for testing
            Mapper.Initialize(x =>
            {
                x.AddProfile<MapperProfile>();
            });
        }

        [SetUp]
        public void Initialize()
        {
            // Create Mock
            var _mock = new Mock<ICartRepository>();
            mock = _mock;
            var _mockProduct = new Mock<IProductRepository>();
            mockProduct = _mockProduct;
            var _mockNotify = new Mock<INotificationService>();
            mockNotify = _mockNotify; 
        }

        [Test]
        public void Add_New_Content_In_Cart()
        {
            var notifyDto = new NotificationDto();
            
            // Setup Mock Product
            mockNotify.Setup(item => item.AddToCartNotify(It.IsAny<AddToCartNotifyDto>()))
                .Returns(() => notifyDto);

            // Create object Product
            var product = new Product() { Id = 1 };

            // Setup Mock Product
            mockProduct.Setup(item => item.Get(It.IsAny<long>()))
                .Returns(() => product);

            // Create object ContentCart
            var objContentCart = new ContentCart() { Id = 5 };
            var actual1 = objContentCart.Id;

            // Create List for search by content.Id
            List<ContentCart> contentCartList = new List<ContentCart>();

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCart, bool>>>()))
                .Returns(() => contentCartList);
            mock.Setup(repo => repo.Add(It.IsAny<ContentCart>()))
                .Returns(() => objContentCart).Callback(() => objContentCart.Id++);

            // Create CartService object with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);

            // Write rezalt method AddNewContentInCart in actual3
            var actual3 = service.AddInCart(objContentCart.Id, 1);

            // Verification rezalt with neсуssary number
            Assert.AreEqual((long)5, actual1);
            //Assert.AreEqual((long)5, actual2);
            Assert.AreEqual((long)6, actual3.Id);
        }

        [Test]
        public void Add_New_Content_In_Cart_If_Not_Save_In_Repository()
        {
            var notifyDto = new NotificationDto();

            // Setup Mock Product
            mockNotify.Setup(item => item.AddToCartNotify(It.IsAny<AddToCartNotifyDto>()))
                .Returns(() => notifyDto);

            // Create object Product
            var product = new Product() { Id = 1 };

            // Setup Mock Product
            mockProduct.Setup(item => item.Get(It.IsAny<long>()))
                .Returns(() => product);

            // Create object ContentCart
            var objContentCart = new ContentCart() { Id = 5 };
            var actual1 = objContentCart.Id;

            // Create List for search by content.Id
            List<ContentCart> contentCartList = new List<ContentCart>();

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCart, bool>>>()))
                .Returns(() => contentCartList);
            mock.Setup(repo => repo.Add(It.IsAny<ContentCart>()))
                .Returns(() => null);

            // Create CartService object with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);

            // Write rezalt method AddNewContentInCart in actual3
            Assert.Throws<AddContentInCartExceptions>(() => service.AddInCart(objContentCart.Id, 1));
        }

        [Test]
        public void Get_Content_In_Cart_If_Exist_In_Cart()
        {
            // Create List for search by content.Id
            List<ContentCart> contentCartList = new List<ContentCart>()
            {
                new ContentCart() { Id = 1, CreatorId = 1 }
            };

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCart, bool>>>()))
                .Returns(() => contentCartList);

            // Create CartService with mock.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);

            // Call method for find content by id in repository
            var actual1 = service.ExistInCart(1,1,Common.Enums.CartEnums.StateCartContent.InCart);

            Assert.IsTrue(actual1);
        }

        [Test]
        public void Get_Content_In_Cart_If_Not_Exist_In_Cart()
        {
            // Create List for search by content.Id
            List<ContentCart> contentCartList = new List<ContentCart>();

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCart, bool>>>()))
                .Returns(() => contentCartList);

            // Create CartService with mock.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);

            // Call method for find content by id in repository
            var actual1 = service.ExistInCart(6,1, Common.Enums.CartEnums.StateCartContent.InCart);

            Assert.IsFalse(actual1);
        }
    }
}

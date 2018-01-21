using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Moq;
using AutoMapper;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.CartModels;
using MediaShop.Common.Models;
using MediaShop.Common;
using MediaShop.Common.Exceptions.CartExseptions;
using MediaShop.BusinessLogic.Services;

namespace MediaShop.BusinessLogic.Tests.CartTests
{
    [TestClass]
    public class AddNewContentInCartUnitTests
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
        public void Add_New_Content_In_Cart()
        {
            // Create object ContentCart
            var objContentCart = new ContentCart() { Id = 5 };
            var actual1 = objContentCart.Id;

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCart, bool>>>()))
                .Returns(() => null);
            mock.Setup(repo => repo.Add(It.IsAny<ContentCart>()))
                .Returns(() => objContentCart).Callback(() => objContentCart.Id++);

            // Create ProductDto object
            var objProduct = new Product() { Id = 5 };
            var actual2 = objProduct.Id;

            mockProduct.Setup(repo => repo.Get(It.IsAny<long>()))
                .Returns(() => objProduct);

            // Create CartService object with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);

            // Write rezalt method AddNewContentInCart in actual3
            var actual3 = service.AddInCart(objContentCart.Id);

            // Verification rezalt with neсуssary number
            Assert.AreEqual((long)5, actual1);
            Assert.AreEqual((long)5, actual2);
            Assert.AreEqual((long)6, actual3.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(AddContentInCartExceptions))]
        public void Add_New_Content_In_Cart_If_Not_Save_In_Repository()
        {
            // Create object ContentCart
            var objContentCart = new ContentCart() { Id = 5 };
            var actual1 = objContentCart.Id;

            // Setup mock object
            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCart, bool>>>()))
                .Returns(() => null);
            mock.Setup(repo => repo.Add(It.IsAny<ContentCart>()))
                .Returns(() => null);

            // Create ProductDto object
            var objProduct = new Product() { Id = 5 };
            var actual2 = objProduct.Id;

            mockProduct.Setup(repo => repo.Get(It.IsAny<long>()))
                .Returns(() => objProduct);

            // Create CartService object with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);

            // Write rezalt method AddNewContentInCart in actual3
            var actual3 = service.AddInCart(objContentCart.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(NotExistProductInDataBaseExceptions))]
        public void Add_New_Content_In_Cart_If_Not_Get_Product()
        {
            // Create object ContentCart
            var objContentCart = new ContentCart() { Id = 5 };
            var actual1 = objContentCart.Id;

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCart, bool>>>()))
                .Returns(() => null);
            mock.Setup(repo => repo.Add(It.IsAny<ContentCart>()))
                .Returns(() => objContentCart);

            // Create ProductDto object
            var objProduct = new Product() { Id = 5 };
            var actual2 = objProduct.Id;

            mockProduct.Setup(repo => repo.Get(It.IsAny<long>()))
                .Returns(() => null);

            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);

            // Write rezalt method AddNewContentInCart in actual1
            var actual3 = service.AddInCart(objContentCart.Id);
        }

        [TestMethod]
        public void Get_Content_In_Cart_If_Exist_In_Cart()
        {
            // collection for rezalt as return method 
            var collectionFindItems = new Collection<ContentCart>()
            {
                new ContentCart { Id = 1, CreatorId = 1 }
            };

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCart, bool>>>()))
                .Returns(() => collectionFindItems);

            // Create CartService with mock.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);

            // Call method for find content by id in repository
            var actual1 = service.ExistInCart(1);

            Assert.IsTrue(actual1);
        }

        [TestMethod]
        public void Get_Content_In_Cart_If_Not_Exist_In_Cart()
        {
            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCart, bool>>>()))
                .Returns(() => null);

            // Create CartService with mock.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);

            // Call method for find content by id in repository
            var actual1 = service.ExistInCart(6);

            Assert.IsFalse(actual1);
        }
    }
}

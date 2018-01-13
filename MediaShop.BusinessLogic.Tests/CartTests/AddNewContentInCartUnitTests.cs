using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using AutoMapper;
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

        [TestInitialize]
        public void Initialize()
        {
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
        }

        [TestMethod]
        public void Add_New_Content_In_Cart()
        {
            // Create object ContentCart
            var objContentCart = new ContentCart() { Id = 5, CategoryName = "My Category" };
            var actual1 = objContentCart.Id;

            // Setup mock object
            mock.Setup(repo => repo.Add(It.IsAny<ContentCart>()))
                .Returns(() => objContentCart).Callback(() => objContentCart.Id++);

            // Create ProductDto object
            var objProduct = new Product() { Id = 5 };
            var actual2 = objProduct.Id;

            mockProduct.Setup(repo => repo.Get(It.IsAny<long>()))
                .Returns(() => objProduct);

            // Create CartService object with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object);

            // Write rezalt method AddNewContentInCart in actual3
            var actual3 = service.AddInCart(objContentCart.Id, "My Category");

            // Verification rezalt with neсуssary number
            Assert.AreEqual((long)5, actual1);
            Assert.AreEqual((long)5, actual2);
            Assert.AreEqual((long)6, actual3.Id);
            Assert.AreEqual("My Category", actual3.CategoryName);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Add_New_Content_In_Cart_If_Invalid_Argument_CategoryName()
        {
            // Create object ContentCart
            var objContentCart = new ContentCart() { Id = 5, CategoryName = "My Category" };
            var actual1 = objContentCart.Id;

            // Setup mock object
            mock.Setup(repo => repo.Add(It.IsAny<ContentCart>()))
                .Returns(() => objContentCart).Callback(() => objContentCart.Id++);

            // Create ProductDto object
            var objProduct = new Product() { Id = 5 };
            var actual2 = objProduct.Id;

            mockProduct.Setup(repo => repo.Get(It.IsAny<long>()))
                .Returns(() => objProduct);

            // Create CartService object with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object);

            // Write rezalt method AddNewContentInCart in actual3
            var actual3 = service.AddInCart(objContentCart.Id, "");
        }

        [TestMethod]
        [ ExpectedException(typeof(AddContentInCartExceptions))]
        public void Add_New_Content_In_Cart_If_Not_Save_In_Repository()
        {
            // Create object ContentCart
            var objContentCart = new ContentCart() { Id = 5, CategoryName = "My Category" };
            var actual1 = objContentCart.Id;

            // Setup mock object
            mock.Setup(repo => repo.Add(It.IsAny<ContentCart>()))
                .Returns(() => null);

            // Create ProductDto object
            var objProduct = new Product() { Id = 5 };
            var actual2 = objProduct.Id;

            mockProduct.Setup(repo => repo.Get(It.IsAny<long>()))
                .Returns(() => objProduct);

            // Create CartService object with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object);

            // Write rezalt method AddNewContentInCart in actual3
            var actual3 = service.AddInCart(objContentCart.Id, "My Category");
        }

        [TestMethod]
        [ExpectedException(typeof(ExistContentInCartExceptions))]
        public void Add_New_Content_In_Cart_If_Not_Get_Product()
        {
            // Create object ContentCart
            var objContentCart = new ContentCart() { Id = 5, CategoryName = "My Category" };
            var actual1 = objContentCart.Id;

            // Setup mock object
            mock.Setup(repo => repo.Add(It.IsAny<ContentCart>()))
                .Returns(() => objContentCart);

            // Create ProductDto object
            var objProduct = new Product() { Id = 5 };
            var actual2 = objProduct.Id;

            mockProduct.Setup(repo => repo.Get(It.IsAny<long>()))
                .Returns(() => null);

            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object);

            // Write rezalt method AddNewContentInCart in actual1
            var actual3 = service.AddInCart(objContentCart.Id, "My Category");
        }

        [TestMethod]
        public void Get_Content_In_Cart_If_Exist_In_Cart()
        {
            // Setup object moc
            mock.Setup(repo => repo.Get(It.IsAny<long>()))
                 .Returns(() => new ContentCart());

            // Create CartService with mock.Object
            var service = new CartService(mock.Object, mockProduct.Object);

            // Call method for find content by id in repository
            var actual1 = service.FindInCart(6);

            Assert.IsTrue(actual1);
        }

        [TestMethod]
        public void Get_Content_In_Cart_If_Not_Exist_In_Cart()
        {
            // Setup object moc
            mock.Setup(repo => repo.Get(It.IsAny<long>()))
                .Returns(() => null);

            // Create CartService with mock.Object
            var service = new CartService(mock.Object, mockProduct.Object);

            // Call method for find content by id in repository
            var actual1 = service.FindInCart(6);

            Assert.IsFalse(actual1);
        }
    }
}

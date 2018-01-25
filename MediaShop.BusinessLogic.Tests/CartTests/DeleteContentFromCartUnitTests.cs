using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models;
using MediaShop.Common;
using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Exceptions.CartExseptions;
using MediaShop.Common.Enums;

namespace MediaShop.BusinessLogic.Tests.CartTests
{
    [TestClass]
    public class DeleteContentFromCartUnitTests
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
        public void Set_State_Content_Is_Bought()
        {
            //// Object ContentCartDtoDto
            //var objContentCart = new ContentCart() { StateContent = CartEnums.StateCartContent.InBought };

            // Create List for search by content.Id
            List<ContentCart> contentCartList = new List<ContentCart>()
            {
                new ContentCart() { Id = 1, CreatorId =1, StateContent = CartEnums.StateCartContent.InCart  }
            };

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCart, bool>>>()))
                .Returns(() => contentCartList);
            mock.Setup(item => item.Update(It.IsAny<ContentCart>()))
                .Returns(() => contentCartList[0]);

            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);

            var actual2 = service.SetState(5, CartEnums.StateCartContent.InBought);

            Assert.AreEqual(CartEnums.StateCartContent.InBought, actual2.StateContent);
        }

        [TestMethod]
        public void Set_State_Content_Is_Paid()
        {
            //// Object ContentCartDtoDto
            //var objContentCart = new ContentCart() { StateContent = CartEnums.StateCartContent.InPaid };

            // Create List for search by content.Id
            List<ContentCart> contentCartList = new List<ContentCart>()
            {
                new ContentCart() { Id = 1, CreatorId =1, StateContent = CartEnums.StateCartContent.InBought  }
            };

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCart, bool>>>()))
                .Returns(() => contentCartList);
            mock.Setup(item => item.Update(It.IsAny<ContentCart>()))
                .Returns(() => contentCartList[0]);

            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);

            var actual2 = service.SetState(5, CartEnums.StateCartContent.InPaid);

            Assert.AreEqual(CartEnums.StateCartContent.InPaid, actual2.StateContent);
        }

        [TestMethod]
        public void Delete_Content_From_Cart()
        {
            // collection for rezalt as return method 
            var collectionItems = new Collection<ContentCart>()
            {
                new ContentCart { Id = 5, CreatorId = 10 },
                new ContentCart { Id = 6, CreatorId = 10 }
            };

            var actual1 = collectionItems[0].Id;
            var actual2 = collectionItems[0].CreatorId;
            var actual3 = collectionItems[1].Id;
            var actual4 = collectionItems[1].CreatorId;

            // Setup mock object
            mock.SetupSequence(item => item.Delete(It.IsAny<long>()))
                .Returns(collectionItems[0])
                .Returns(collectionItems[1])
                .Throws(new InvalidOperationException());

            // Collection object`s id for delete
            var collectionId = new Collection<long>() { 5, 6 };

            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);

            // Delete content
            var actual5 = service.DeleteOfCart(collectionId);

            Assert.AreEqual((long)5, actual1);
            Assert.AreEqual((long)10, actual2);
            Assert.AreEqual((long)6, actual3);
            Assert.AreEqual((long)10, actual4);
            Assert.AreEqual(2, actual5.Count);
            mock.Verify(item => item.Delete(It.IsAny<long>()), Times.Exactly(2));
        }

        [TestMethod]
        public void Delete_of_Cart()
        {
            var collectionItems = new Collection<ContentCartDto>()
            {
                new ContentCartDto { Id = 1, CreatorId = 1 },
                new ContentCartDto { Id = 2, CreatorId = 1 }
            };

            var cart = new Cart()
            {
                ContentCartDtoCollection = collectionItems
            };

            mock.SetupSequence(item => item.Delete(It.IsAny<ContentCart>()))
                .Returns(new ContentCart { Id = 1, CreatorId = 1 })
                .Returns(new ContentCart { Id = 2, CreatorId = 1 })
                .Throws(new InvalidOperationException());
            var collectionId = new Collection<long>() { 1, 2 };
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);
            var result = service.DeleteOfCart(cart);

            Assert.AreEqual((uint)0,  result.CountItemsInCollection);
            Assert.AreEqual((decimal)0, result.PriceAllItemsCollection);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_of_Cart_is_null()
        {
            Cart cart = null;
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);
            var result = service.DeleteOfCart(cart);
        }

        [TestMethod]
        public void Delete_of_Cart_is_empty()
        {
            Cart cart = new Cart();
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);
            var result = service.DeleteOfCart(cart);
            Assert.AreEqual((uint)0, result.CountItemsInCollection);
            Assert.AreEqual((decimal)0, result.PriceAllItemsCollection);
        }

        [TestMethod]
        [ExpectedException(typeof(DeleteContentInCartExseptions))]
        public void Delete_Content_From_Cart_If_Not_All_Delete()
        {
            // collection for rezalt as return method 
            var collectionItems = new Collection<ContentCart>()
            {
                new ContentCart { Id = 5, CreatorId = 10 },
                new ContentCart { Id = 6, CreatorId = 11 },
                new ContentCart { Id = 7, CreatorId = 10 }
            };

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCart, bool>>>()))
                .Returns(() => collectionItems);
            mock.SetupSequence(item => item.Delete(It.IsAny<long>()))
                .Returns(collectionItems[0])
                .Returns(collectionItems[1])
                .Returns(null)
                .Throws(new InvalidOperationException());

            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);

            // Collection object`s id for delete
            var collectionId = new Collection<long>() { 5, 6, 7 };

            // Delete content
            var actual5 = service.DeleteOfCart(collectionId);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Delete_Content_From_Cart_If_Argument_Is_Null()
        {
            // collection for rezalt as return method 
            var collectionItems = new Collection<ContentCart>()
            {
                new ContentCart { Id = 5, CreatorId = 10 },
                new ContentCart { Id = 6, CreatorId = 11 }
            };

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCart, bool>>>()))
                .Returns(() => collectionItems);
            mock.SetupSequence(item => item.Delete(It.IsAny<long>()))
                .Returns(collectionItems[0])
                .Returns(null)
                .Throws(new InvalidOperationException());

            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockPayment.Object);

            // Collection object`s id for delete
            Collection<long> collectionId = null;

            // Delete content
            var actual5 = service.DeleteOfCart(collectionId);
        }
    }
}

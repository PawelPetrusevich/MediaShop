using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq.Expressions;
using Moq;
using AutoMapper;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models;
using MediaShop.Common.Models.PaymentModel;
using MediaShop.Common;
using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Exceptions.CartExceptions;
using MediaShop.Common.Enums;
using NUnit.Framework;

namespace MediaShop.BusinessLogic.Tests.CartTests
{
    [TestFixture]
    public class DeleteContentFromCartUnitTests
    {
        // Field for Mock
        private Mock<ICartRepository> mock;

        // Field for MockProduct
        private Mock<IProductRepository> mockProduct;

        // Field for MockNotify
        private Mock<INotificationService> mockNotify;

        public DeleteContentFromCartUnitTests()
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
        public void Set_State_Content_Is_Bought()
        {
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
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);

            var actual2 = service.SetState(5,1, CartEnums.StateCartContent.InBought);

            Assert.AreEqual(CartEnums.StateCartContent.InBought, actual2.StateContent);
        }

        [Test]
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
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);

            var actual2 = service.SetState(5,1, CartEnums.StateCartContent.InPaid);

            Assert.AreEqual(CartEnums.StateCartContent.InPaid, actual2.StateContent);
        }

        [Test]
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
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);

            // Delete content
            var actual5 = service.DeleteOfCart(collectionId);

            Assert.AreEqual((long)5, actual1);
            Assert.AreEqual((long)10, actual2);
            Assert.AreEqual((long)6, actual3);
            Assert.AreEqual((long)10, actual4);
            Assert.AreEqual(2, actual5.Count);
            mock.Verify(item => item.Delete(It.IsAny<long>()), Times.Exactly(2));
        }

        [Test]
        public void Delete_of_Cart()
        {
            var collectionItems = new Collection<ContentCart>()
            {
                new ContentCart { Id = 1, CreatorId = 10 },
                new ContentCart { Id = 2, CreatorId = 10 }
            };

            var counter = 0;

            mock.Setup(userId => userId.GetById(10))
                .Returns(collectionItems);
            mock.Setup(item => item.Delete(It.IsAny<ContentCart>()))
                .Returns(new ContentCart())
                .Callback(() => { collectionItems.RemoveAt(counter); });
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);
            var result = service.DeleteOfCart(10);

            Assert.AreEqual((uint)0,  result.CountItemsInCollection);
            Assert.AreEqual((decimal)0, result.PriceAllItemsCollection);
        }

        [Test]
        public void Delete_of_Cart_is_empty()
        {
            Cart cart = new Cart();
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);
            var result = service.DeleteOfCart(1);
            Assert.AreEqual((uint)0, result.CountItemsInCollection);
            Assert.AreEqual((decimal)0, result.PriceAllItemsCollection);
        }

        [Test]
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
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);

            // Collection object`s id for delete
            var collectionId = new Collection<long>() { 5, 6, 7 };

            // Delete content
            Assert.Throws<DeleteContentInCartExceptions>(() => service.DeleteOfCart(collectionId));
        }

        [Test]
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
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);

            // Collection object`s id for delete
            Collection<long> collectionId = null;

            // Delete content
            Assert.Throws<NullReferenceException>(() => service.DeleteOfCart(collectionId));
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models;
using MediaShop.Common;
using MediaShop.BusinessLogic.Services;

namespace MediaShop.BusinessLogic.Tests.CartTests
{
    [TestClass]
    public class DeleteContentFromCartUnitTests
    {
        // Field for Mock
        private Mock<ICartRepository<ContentCartDto>> mock;

        [TestInitialize]
        public void Initialize()
        {
            // Create Mapper for testing
            AutoMapperConfiguration.Configure();

            // Create Mock
            var _mock = new Mock<ICartRepository<ContentCartDto>>();
            mock = _mock;
        }

        [TestMethod]
        public void Checked_ContentCart()
        {
            // Object ContentCartDto
            var objContentCartDto = new ContentCartDto() { IsChecked = false };
            var actual1 = objContentCartDto.IsChecked;

            // Setup mock object
            mock.Setup(item => item.CheckedContent(It.IsAny<Expression<Func<ContentCartDto, bool>>>()))
                .Returns(() => objContentCartDto)
                .Callback(() => objContentCartDto.IsChecked = true);

            // Create CartService object with mock.Object
            var service = new CartService(mock.Object);

            var actual2 = service.CheckedContent(5, 6);
            
            Assert.IsFalse(actual1);
            Assert.IsTrue(actual2);
        }

        [TestMethod]
        public void UnChecked_ContentCart()
        {
            // Object ContentCartDto
            var objContentCartDto = new ContentCartDto() { IsChecked = true };
            var actual1 = objContentCartDto.IsChecked;

            // Setup mock object
            mock.Setup(item => item.CheckedContent(It.IsAny<Expression<Func<ContentCartDto, bool>>>()))
                .Returns(() => objContentCartDto)
                .Callback(() => objContentCartDto.IsChecked = false);

            // Create CartService object with mock.Object
            var service = new CartService(mock.Object);

            var actual2 = service.CheckedContent(5, 6);

            Assert.IsTrue(actual1);
            Assert.IsFalse(actual2);
        }

        [TestMethod]
        public void Delete_Content_From_Cart()
        {
            // collection for rezalt as return method 
            var collectionItem = new Collection<ContentCartDto>()
            {
                new ContentCartDto { Id = 5, CreatorId = 10 },
                new ContentCartDto { Id = 6, CreatorId = 10 }
            };

            var actual1 = collectionItem[0].Id;
            var actual2 = collectionItem[0].CreatorId;
            var actual3 = collectionItem[1].Id;
            var actual4 = collectionItem[1].CreatorId;

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCartDto, bool>>>()))
                .Returns(() => collectionItem);
            mock.SetupSequence(item => item.Delete(It.IsAny<ulong>()))
                .Returns(collectionItem[0])
                .Returns(collectionItem[1])
                .Throws(new InvalidOperationException());

            // Create CartService object with mock.Object
            var service = new CartService(mock.Object);

            // Delete content
            var actual5 = service.DeleteContentFromCart(10);

            Assert.AreEqual((ulong)5, actual1);
            Assert.AreEqual((ulong)10, actual2);
            Assert.AreEqual((ulong)6, actual3);
            Assert.AreEqual((ulong)10, actual4);
            Assert.AreEqual(2, actual5.Count);
            mock.Verify(item => item.Delete(It.IsAny<ulong>()), Times.Exactly(2));
        }

        [TestMethod]
        public void Delete_Content_From_Cart_If_Not_All_Delete()
        {
            // collection for rezalt as return method 
            var collectionItem = new Collection<ContentCartDto>()
            {
                new ContentCartDto { Id = 5, CreatorId = 10 },
                new ContentCartDto { Id = 6, CreatorId = 11 }
            };

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCartDto, bool>>>()))
                .Returns(() => collectionItem);
            mock.SetupSequence(item => item.Delete(It.IsAny<ulong>()))
                .Returns(collectionItem[0])
                .Returns(null)
                .Throws(new InvalidOperationException());

            // Create CartService object with mock.Object
            var service = new CartService(mock.Object);

            // Delete content
            var actual5 = service.DeleteContentFromCart(10);

            Assert.AreEqual(1, actual5.Count);
            mock.Verify(item => item.Delete(It.IsAny<ulong>()), Times.Exactly(2));
        }

        [TestMethod]
        public void Delete_All_Content_From_Cart()
        {
            // collection for rezalt as return method 
            var collectionItem = new Collection<ContentCartDto>()
            {
                new ContentCartDto { Id = 5, CreatorId = 10 },
                new ContentCartDto { Id = 6, CreatorId = 10 }
            };

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCartDto, bool>>>()))
                .Returns(() => collectionItem);
            mock.SetupSequence(item => item.Delete(It.IsAny<ulong>()))
                .Returns(collectionItem[0])
                .Returns(collectionItem[1])
                .Throws(new InvalidOperationException());

            // Create CartService object with mock.Object
            var service = new CartService(mock.Object);

            // Delete content
            var actual5 = service.DeleteAllContentFromCart(10);

            Assert.AreEqual(2, actual5);
            mock.Verify(item => item.Delete(It.IsAny<ulong>()), Times.Exactly(2));
        }

        [TestMethod]
        public void Delete_All_Content_From_Cart_If_Not_All_Content_Delete()
        {
            // collection for rezalt as return method 
            var collectionItem = new Collection<ContentCartDto>()
            {
                new ContentCartDto { Id = 5, CreatorId = 10 },
                new ContentCartDto { Id = 6, CreatorId = 10 }
            };

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCartDto, bool>>>()))
                .Returns(() => collectionItem);
            mock.SetupSequence(item => item.Delete(It.IsAny<ulong>()))
                .Returns(collectionItem[0])
                .Returns(null)
                .Throws(new InvalidOperationException());

            // Create CartService object with mock.Object
            var service = new CartService(mock.Object);

            // Delete content
            var actual5 = service.DeleteAllContentFromCart(10);

            Assert.AreEqual(1, actual5);
            mock.Verify(item => item.Delete(It.IsAny<ulong>()), Times.Exactly(2));
        }
    }
}

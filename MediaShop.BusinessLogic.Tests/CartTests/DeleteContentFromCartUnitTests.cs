using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models;
using MediaShop.Common.Models.CartModels;
using MediaShop.Common;
using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Enums;

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
            Mapper.Initialize(x =>
            {
                x.AddProfile<MapperProfile>();
            });

            // Create Mock
            var _mock = new Mock<ICartRepository<ContentCartDto>>();
            mock = _mock;
        }

        [TestMethod]
        public void Set_Content_Is_Bought()
        {
            // Object ContentCartDto
            var objContentCartDto = new ContentCartDto() { StateContent = CartEnums.StateCartContent.InCart };
            var actual1 = objContentCartDto.StateContent;

            // Setup mock object
            // Setup object moc
            mock.Setup(repo => repo.Get(It.IsAny<ulong>()))
                 .Returns(() => objContentCartDto);
            mock.Setup(item => item.Update(It.IsAny<ContentCartDto>()))
                .Returns(() => objContentCartDto);

            // Create CartService object with mock.Object
            var service = new CartService(mock.Object);

            var actual2 = service.SetBought(5, 6);

            Assert.AreEqual(CartEnums.StateCartContent.InCart, actual1);
            Assert.AreEqual(CartEnums.StateCartContent.InBought, actual2);
        }

        [TestMethod]
        public void Set_Content_Is_UnBought()
        {
            // Object ContentCartDto
            var objContentCartDto = new ContentCartDto() { StateContent = CartEnums.StateCartContent.InBought };
            var actual1 = objContentCartDto.StateContent;

            // Setup mock object
            // Setup object moc
            mock.Setup(repo => repo.Get(It.IsAny<ulong>()))
                 .Returns(() => objContentCartDto);
            mock.Setup(item => item.Update(It.IsAny<ContentCartDto>()))
                .Returns(() => objContentCartDto);

            // Create CartService object with mock.Object
            var service = new CartService(mock.Object);

            var actual2 = service.SetUnBought(5, 6);

            Assert.AreEqual(CartEnums.StateCartContent.InBought, actual1);
            Assert.AreEqual(CartEnums.StateCartContent.InCart, actual2);
        }

        [TestMethod]
        public void Set_Content_Is_Paid()
        {
            // Object ContentCartDto
            var objContentCartDto = new ContentCartDto() { StateContent = CartEnums.StateCartContent.InBought };
            var actual1 = objContentCartDto.StateContent;

            // Setup mock object
            // Setup object moc
            mock.Setup(repo => repo.Get(It.IsAny<ulong>()))
                 .Returns(() => objContentCartDto);
            mock.Setup(item => item.Update(It.IsAny<ContentCartDto>()))
                .Returns(() => objContentCartDto);

            // Create CartService object with mock.Object
            var service = new CartService(mock.Object);

            var actual2 = service.SetPaid(5, 6);

            Assert.AreEqual(CartEnums.StateCartContent.InBought, actual1);
            Assert.AreEqual(CartEnums.StateCartContent.InPaid, actual2);
        }

        [TestMethod]
        public void Set_Content_Is_UnPaid()
        {
            // Object ContentCartDto
            var objContentCartDto = new ContentCartDto() { StateContent = CartEnums.StateCartContent.InPaid };
            var actual1 = objContentCartDto.StateContent;

            // Setup mock object
            // Setup object moc
            mock.Setup(repo => repo.Get(It.IsAny<ulong>()))
                 .Returns(() => objContentCartDto);
            mock.Setup(item => item.Update(It.IsAny<ContentCartDto>()))
                .Returns(() => objContentCartDto);

            // Create CartService object with mock.Object
            var service = new CartService(mock.Object);

            var actual2 = service.SetUnPaid(5, 6);

            Assert.AreEqual(CartEnums.StateCartContent.InPaid, actual1);
            Assert.AreEqual(CartEnums.StateCartContent.InBought, actual2);
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
            mock.SetupSequence(item => item.Delete(It.IsAny<ulong>()))
                .Returns(collectionItem[0])
                .Returns(collectionItem[1])
                .Throws(new InvalidOperationException());

            // Collection object`s id for delete
            var collectionId = new Collection<ulong>() { 5, 6 };

            // Create CartService object with mock.Object
            var service = new CartService(mock.Object);

            // Delete content
            var actual5 = service.Delete(collectionId);

            Assert.AreEqual((ulong)5, actual1);
            Assert.AreEqual((ulong)10, actual2);
            Assert.AreEqual((ulong)6, actual3);
            Assert.AreEqual((ulong)10, actual4);
            Assert.AreEqual(2, actual5.Count);
            mock.Verify(item => item.Delete(It.IsAny<ulong>()), Times.Exactly(2));
        }

        [TestMethod]
        [ExpectedException(typeof(CartExceptions))]
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

            // Collection object`s id for delete
            var collectionId = new Collection<ulong>() { 5, 6 };

            // Delete content
            var actual5 = service.Delete(collectionId);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Delete_Content_From_Cart_If_Argument_Is_Null()
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

            // Collection object`s id for delete
            Collection<ulong> collectionId = null;

            // Delete content
            var actual5 = service.Delete(collectionId);
        }
    }
}

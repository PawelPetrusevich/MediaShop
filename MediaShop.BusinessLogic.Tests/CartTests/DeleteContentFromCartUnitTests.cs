﻿using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
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
        public void Set_State_Content_Is_Bought()
        {
            // Object ContentCartDtoDto
            var objContentCartDto = new ContentCartDto() { StateContent = CartEnums.StateCartContent.InCart };
            var actual1 = objContentCartDto.StateContent;

            // Setup object moc
            mock.Setup(repo => repo.Get(It.IsAny<long>()))
                 .Returns(() => objContentCartDto);
            mock.Setup(item => item.Update(It.IsAny<ContentCartDto>()))
                .Returns(() => objContentCartDto);

            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object);

            var actual2 = service.SetState(5, CartEnums.StateCartContent.InBought);

            Assert.AreEqual(CartEnums.StateCartContent.InCart, actual1);
            Assert.AreEqual(CartEnums.StateCartContent.InBought, actual2.StateContent);
        }

        [TestMethod]
        public void Set_State_Content_Is_Paid()
        {
            // Object ContentCartDtoDto
            var objContentCartDto = new ContentCartDto() { StateContent = CartEnums.StateCartContent.InBought };
            var actual1 = objContentCartDto.StateContent;

            // Setup object moc
            mock.Setup(repo => repo.Get(It.IsAny<long>()))
                 .Returns(() => objContentCartDto);
            mock.Setup(item => item.Update(It.IsAny<ContentCartDto>()))
                .Returns(() => objContentCartDto);

            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object);

            var actual2 = service.SetState(5, CartEnums.StateCartContent.InPaid);

            Assert.AreEqual(CartEnums.StateCartContent.InBought, actual1);
            Assert.AreEqual(CartEnums.StateCartContent.InPaid, actual2.StateContent);
        }

        [TestMethod]
        public void Delete_Content_From_Cart()
        {
            // collection for rezalt as return method
            var collectionItems = new Collection<ContentCartDto>()
            {
                new ContentCartDto { Id = 5, CreatorId = 10 },
                new ContentCartDto { Id = 6, CreatorId = 10 }
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
            var service = new CartService(mock.Object, mockProduct.Object);

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
        [ExpectedException(typeof(DeleteContentInCartExseptions))]
        public void Delete_Content_From_Cart_If_Not_All_Delete()
        {
            // collection for rezalt as return method
            var collectionItems = new Collection<ContentCartDto>()
            {
                new ContentCartDto { Id = 5, CreatorId = 10 },
                new ContentCartDto { Id = 6, CreatorId = 11 },
                new ContentCartDto { Id = 7, CreatorId = 10 }
            };

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCartDto, bool>>>()))
                .Returns(() => collectionItems);
            mock.SetupSequence(item => item.Delete(It.IsAny<long>()))
                .Returns(collectionItems[0])
                .Returns(collectionItems[1])
                .Returns(null)
                .Throws(new InvalidOperationException());

            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object);

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
            var collectionItems = new Collection<ContentCartDto>()
            {
                new ContentCartDto { Id = 5, CreatorId = 10 },
                new ContentCartDto { Id = 6, CreatorId = 11 }
            };

            // Setup mock object
            mock.Setup(item => item.Find(It.IsAny<Expression<Func<ContentCartDto, bool>>>()))
                .Returns(() => collectionItems);
            mock.SetupSequence(item => item.Delete(It.IsAny<long>()))
                .Returns(collectionItems[0])
                .Returns(null)
                .Throws(new InvalidOperationException());

            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object);

            // Collection object`s id for delete
            Collection<long> collectionId = null;

            // Delete content
            var actual5 = service.DeleteOfCart(collectionId);
        }
    }
}

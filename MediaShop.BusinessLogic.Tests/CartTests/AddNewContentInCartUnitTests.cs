using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Dto;
using MediaShop.Common.Models;
using MediaShop.Common;
using MediaShop.BusinessLogic.Services;


namespace MediaShop.BusinessLogic.Tests.CartTests
{
    [TestClass]
    public class AddNewContentInCartUnitTests
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
        public void Add_New_Content_In_Cart()
        {
            // Create object ContentCartDto
            var objContentCartDto = new ContentCartDto() { Id = 5, CreatorId = 50 };
            var actual1 = objContentCartDto.Id;

            // Setup mock object
            mock.Setup(repo => repo.Add(It.IsAny<ContentCartDto>()))
                .Returns(() => objContentCartDto).Callback(() => objContentCartDto.Id++);

            // Create ProductDto object
            var objProductDto = new ProductDto() { Id = 5 };
            var actual2 = objProductDto.Id;

            Mock<IProductRepository<ProductDto>> mockProductDto =
                new Mock<IProductRepository<ProductDto>>();

            mockProductDto.Setup(repo => repo.Get(It.IsAny<ulong>()))
                .Returns(() => objProductDto);
            mockProductDto.Setup(repo => repo.Dispose());

            // Create CartService object with mock.Object
            var service = new CartService(mock.Object);



            // Write rezalt method AddNewContentInCart in actual1
            var actual3 = service.Add(objContentCartDto.Id, 50);

            // Verification rezalt with neсуssary number
            Assert.AreEqual((ulong)5, actual1);
            Assert.AreEqual((ulong)5, actual2);
            Assert.AreEqual((ulong)6, actual3.Id);
            Assert.AreEqual((ulong)50, actual3.CreatorId);
        }

        [TestMethod]
        public void Add_New_Content_In_Cart_If_Not_Save_In_Repository()
        {
            // Create object ContentCartDto
            var objContentCartDto = new ContentCartDto() { Id = 5, CreatorId = 50 };

            // Setup mock object
            mock.Setup(repo => repo.Add(It.IsAny<ContentCartDto>()))
                .Returns(() => objContentCartDto);

            // Create CartService with mock.Object
            var service = new CartService(mock.Object);

            // Create ProductDto object
            var objProductDto = new ProductDto() { Id = 5 };
            var actual2 = objProductDto.Id;

            Mock<IProductRepository<ProductDto>> mockProductDto =
                new Mock<IProductRepository<ProductDto>>();

            mockProductDto.Setup(repo => repo.Get(It.IsAny<ulong>()))
                .Returns(() => objProductDto);

            // Write rezalt method AddNewContentInCart in actual1
            var actual1 = service.Add(objProductDto.Id, 50);

            // Verification rezalt with neсуssary number
            Assert.AreEqual((ulong)5, actual1.Id);
        }
        
        [TestMethod]
        public void Get_Content_In_Cart_If_Exist_In_Cart()
        {
            // Setup object moc
            mock.Setup(repo => repo.Get(It.IsAny<ulong>()))
                 .Returns(() => new ContentCartDto());

            // Create CartService with mock.Object
            var service = new CartService(mock.Object);

            // Call method for find content by id in repository
            var actual1 = service.Find(6);

            Assert.IsTrue(actual1);
        }

        [TestMethod]
        public void Get_Content_In_Cart_If_Not_Exist_In_Cart()
        {
            // Setup object moc
            mock.Setup(repo => repo.Get(It.IsAny<ulong>()))
                .Returns(() => null);

            // Create CartService with mock.Object
            var service = new CartService(mock.Object);

            // Call method for find content by id in repository
            var actual1 = service.Find(6);

            Assert.IsFalse(actual1);
        }
    }
}

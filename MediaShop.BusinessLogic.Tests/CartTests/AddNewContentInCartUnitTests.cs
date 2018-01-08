using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.CartModels;
using MediaShop.Common.Models;
using MediaShop.Common;
using MediaShop.BusinessLogic.Services;


namespace MediaShop.BusinessLogic.Tests.CartTests
{
    [TestClass]
    public class AddNewContentInCartUnitTests
    {
        // Field for Mock
        private Mock<ICartRepository<ContentCart>> mock;

        // Field for MockProduct
        private Mock<IProductRepository<Product>> mockProduct;

        [TestInitialize]
        public void Initialize()
        {
            // Create Mapper for testing
            Mapper.Initialize(x =>
            {
                x.AddProfile<MapperProfile>();
            });

            // Create Mock
            var _mock = new Mock<ICartRepository<ContentCart>>();
            mock = _mock;
            var _mockProduct = new Mock<IProductRepository<Product>>();
            mockProduct = _mockProduct;
        }

        [TestMethod]
        public void Add_New_Content_In_Cart()
        {
            // Create object ContentCartDto
            var objContentCart = new ContentCart() { Id = 5, CreatorId = 50 };
            var actual1 = objContentCart.Id;

            // Setup mock object
            mock.Setup(repo => repo.Add(It.IsAny<ContentCart>()))
                .Returns(() => objContentCart).Callback(() => objContentCart.Id++);

            // Create ProductDto object
            var objProductDto = new Product() { Id = 5 };
            var actual2 = objProductDto.Id;

            mockProduct.Setup(repo => repo.Get(It.IsAny<long>()))
                .Returns(() => objProductDto);

            // Create CartService object with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object);

            // Write rezalt method AddNewContentInCart in actual1
            var actual3 = service.AddInCart(objContentCart.Id, 50);

            // Verification rezalt with neсуssary number
            Assert.AreEqual((long)5, actual1);
            Assert.AreEqual((long)5, actual2);
            Assert.AreEqual((long)6, actual3.Id);
            Assert.AreEqual((long)50, actual3.CreatorId);
        }

        [TestMethod]
        [ExpectedException(typeof(CartExceptions))]
        public void Add_New_Content_In_Cart_If_Not_Save_In_Repository()
        {
            // Create object ContentCartDto
            var objContentCart = new ContentCart() { Id = 5, CreatorId = 50 };

            // Setup mock object
            mock.Setup(repo => repo.Add(It.IsAny<ContentCart>()))
                .Returns(() => null);

            // Create ProductDto object
            var objProduct = new Product() { Id = 5 };
            var actual2 = objProduct.Id;

            mockProduct.Setup(repo => repo.Get(It.IsAny<long>()))
                .Returns(() => objProduct);

            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object);

            // Write rezalt method AddNewContentInCart in actual1
            var actual3 = service.AddInCart(objContentCart.Id, 50);
        }

        [TestMethod]
        [ExpectedException(typeof(CartExceptions))]
        public void Add_New_Content_In_Cart_If_Not_Get_Product()
        {
            // Create object ContentCartDto
            var objContentCart = new ContentCart() { Id = 5, CreatorId = 50 };

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
            var actual3 = service.AddInCart(objContentCart.Id, 50);
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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models;
using MediaShop.Common.Interfaces.Services;

namespace MediaShop.Common.Tests.Cart
{
    [TestClass]
    public class AddNewContentInCart
    {
        // Field for Mock
        private Mock<ICartRespository<ContentCartDto>> mock;

        [TestInitialize]
        public void Initialize()
        {
            // Create Mapper for testing
            AutoMapperConfiguration.Configure();

            // Create Mock
            var _mock = new Mock<ICartRespository<ContentCartDto>>();
            mock = _mock;
        }

        [TestMethod]
        public void Add_New_Content_In_Cart()
        {
            // Create object ContentCartDto
            var obj = new ContentCartDto() { ContentId = 5 };

            // Setup mock object
            mock.Setup(repo => repo.Add(It.IsAny<ContentCartDto>()))
                .Returns(() => obj).Callback(() => obj.ContentId++);

            // Create CartService with mock.Object
            var service = new CartService(mock.Object);

            // Write rezalt method AddNewContentInCart in actual1
            var actual1 = service.AddNewContentInCart(obj);

            // Verification rezalt with neсуssary number
            Assert.AreEqual(6, actual1.ContentId);
        }

        [TestMethod]
        public void Add_New_Content_In_Cart_If_Not_Save_In_Repository()
        {
            // Create object ContentCartDto
            var obj = new ContentCartDto() { ContentId = 5 };

            // Setup mock object
            mock.Setup(repo => repo.Add(It.IsAny<ContentCartDto>()))
                .Returns(() => obj);

            // Create CartService with mock.Object
            var service = new CartService(mock.Object);

            // Write rezalt method AddNewContentInCart in actual1
            var actual1 = service.AddNewContentInCart(obj);

            // Verification rezalt with neсуssary number
            Assert.AreEqual(5, actual1.ContentId);
        }

        [TestMethod]
        public void Get_Content_In_Cart_If_Exist_In_Cart()
        {
            // Setup object moc
            mock.Setup(repo => repo.Get(It.IsAny<int>()))
                 .Returns(() => new ContentCartDto());

            // Create CartService with mock.Object
            var service = new CartService(mock.Object);

            // Call method for find content by id in repository
            var actual1 = service.FindContentInCart(6);

            Assert.IsTrue(actual1);
        }

        [TestMethod]
        public void Get_Content_In_Cart_If_Not_Exist_In_Cart()
        {
            // Setup object moc
            mock.Setup(repo => repo.Get(It.IsAny<int>()))
                .Returns(() => null);

            // Create CartService with mock.Object
            var service = new CartService(mock.Object);

            // Call method for find content by id in repository
            var actual1 = service.FindContentInCart(6);

            Assert.IsFalse(actual1);
        }
    }
}

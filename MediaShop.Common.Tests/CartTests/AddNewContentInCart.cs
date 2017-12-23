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
        [TestMethod]
        public void Add_New_Content_In_Cart()
        {
            // Create object MOC for interface ICartRepository<ContentCartDto>
            Mock<ICartRespository<ContentCartDto>> mock =
                new Mock<ICartRespository<ContentCartDto>>();

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
    }
}

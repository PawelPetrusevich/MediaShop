using Moq;
using AutoMapper;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models;
using MediaShop.Common;
using MediaShop.BusinessLogic.Services;
using System.Collections.ObjectModel;
using NUnit.Framework;
using MediaShop.Common.Models.Content;

namespace MediaShop.BusinessLogic.Tests.CartTests
{
    /// <summary>
    /// Summary description for GetCatrUnitTests
    /// </summary>
    [TestFixture]
    public class GetCatrUnitTests
    {
        // Field for Mock
        private Mock<ICartRepository> mock;

        // Field for MockProduct
        private Mock<IProductRepository> mockProduct;

        // Field for MockNotify
        private Mock<INotificationService> mockNotify;

        private Collection<ContentCart> _collectionContentCart;

        public GetCatrUnitTests()
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

            _collectionContentCart = new Collection<ContentCart>()
            {
                new ContentCart { Id = 1, CreatorId = 1 , Product = new Product() { ProductName = "Prod1", Id = 1, ProductPrice = new decimal(9.99) }},
                new ContentCart { Id = 2, CreatorId = 1 , Product = new Product() { ProductName = "Prod2", Id = 2, ProductPrice = new decimal(0.50) }},
                new ContentCart { Id = 3, CreatorId = 1 , Product = new Product() { ProductName = "Prod3", Id = 3, ProductPrice = new decimal(1.01) } }
            };
        }

        [Test]
        public void Get_Cart()
        {

            mock.Setup(item => item.GetById(1))
                .Returns(() => _collectionContentCart);
            mock.Setup(item => item.GetById(It.IsAny<long>()))
                .Returns(_collectionContentCart);
            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);

            var cart = service.GetCart(1);
            Assert.AreEqual((uint)3, cart.CountItemsInCollection);
            Assert.AreEqual(new decimal(11.50), cart.PriceAllItemsCollection);
            Assert.IsNotNull(cart.ContentCartDtoCollection);
        }

        [Test]
        public void Get_EmptyCart()
        {
            var collectionItem = new Collection<ContentCart>();
            mock.Setup(item => item.GetById(1))
                .Returns(() => collectionItem);

            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);

            var cart = service.GetCart(1);
            Assert.AreEqual((uint)0, cart.CountItemsInCollection);
            Assert.AreEqual(new decimal(0), cart.PriceAllItemsCollection);
            Assert.IsNotNull(cart.ContentCartDtoCollection);
        }

        [Test]
        public void Get_Price()
        {
            mock.Setup(item => item.GetById(1))
                .Returns(() => _collectionContentCart);
            mock.Setup(item => item.GetById(It.IsAny<long>()))
                .Returns(_collectionContentCart);
            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);

            var price = service.GetPrice(1);
            Assert.AreEqual(new decimal(11.50),price);           
        }

        [Test]
        public void Get_Price_emptyCart()
        {
            var collectionItems = new Collection<ContentCart>();
            mock.Setup(item => item.GetById(1))
                .Returns(() => collectionItems);
            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);
            var price = service.GetPrice(5);
            Assert.AreEqual(0, price);
        }

        [Test]
        public void Get_Count_Items_in_Cart()
        {
            mock.Setup(item => item.GetById(1))
                .Returns(() => _collectionContentCart);
            mock.Setup(item => item.GetById(It.IsAny<long>()))
                .Returns(_collectionContentCart);
            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);
            var count = service.GetCountItems(1);
            Assert.AreEqual((uint)3, count);
        }

        [Test]
        public void Get_Count_Items_in_emptyCart()
        {
            var collectionItems = new Collection<ContentCart>();
            mock.Setup(item => item.GetById(1))
                .Returns(() => collectionItems);
            // Create CartService with mock.Object and mockProduct.Object
            var service = new CartService(mock.Object, mockProduct.Object, mockNotify.Object);
            var count = service.GetCountItems(1);
            Assert.AreEqual((uint)0, count);
        }

    }
}

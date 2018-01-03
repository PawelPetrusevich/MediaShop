using System;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.Content;
using Moq;
using NUnit.Framework;
using SampleDataGenerator;

namespace MediaShop.BusinessLogic.Tests.ProductTest
{
    [TestFixture]
    public class ProductServiceTest
    {
        [Test]
        public void Test_Service_Add_Method()
        {
            var repository = new Mock<IProductRepository>();
            repository.Setup(r => r.Add(It.IsAny<Product>())).Returns(It.IsAny<Product>());

            var service = new ProductService(repository.Object);

            var productGenerator = Generator
                .For<Product>()
                .For(x => x.Id)
                .CreateUsing(() => new Random().Next())
                .For(x => x.CreatedDate)
                .CreateUsing(() => DateTime.Now)
                .For(x => x.CreatorId)
                .CreateUsing(() => new Random().Next())
                .For(x => x.ProductName)
                .ChooseFrom(StaticData.FirstNames)
                .For(x => x.ProductPrice)
                .CreateUsing(() => new Random().Next())
                .For(x => x.IsFavorite)
                .CreateUsing(() => false)
                .For(x => x.IsPremium)
                .CreateUsing(() => false);

            var product = productGenerator.Generate(1).ToList().First();
                

            Assert.AreEqual(product, service.Add(product));
        }
    }
}

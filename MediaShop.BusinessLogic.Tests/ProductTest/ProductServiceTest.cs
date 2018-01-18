using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Dto;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.Content;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using SampleDataGenerator;
using Assert = NUnit.Framework.Assert;
using System.Linq.Expressions;
using MediaShop.BusinessLogic.ExtensionMethods;
using MediaShop.BusinessLogic.Services;
using MediaShop.BusinessLogic.Tests.Properties;
using MediaShop.Common.Dto.Product;
using System.Drawing;
using System.Text;

namespace MediaShop.BusinessLogic.Tests.ProductTest
{

    [TestFixture]
    public class ProductServiceTest
    {

        private Mock<IProductRepository> _rep;

        private IProductRepository _mockRep;

        private ProductService _productService;

        private List<UploadModel> _newProducts;

        public ProductServiceTest()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Product, ProductDto>().ReverseMap();
                config.CreateMap<Product, UploadModel>().ReverseMap();
                config.CreateMap<Product, ProductContentDTO>().ReverseMap();
            });
        }

        [SetUp]
        public void Init()
        {
            _rep = new Mock<IProductRepository>();
            _rep.Setup(s => s.Add(It.IsAny<Product>())).Returns(new Product());
            _rep.Setup(s => s.Delete(It.IsAny<long>())).Returns(new Product());
            _rep.Setup(s => s.Get(It.IsAny<long>())).Returns(new Product());
            _rep.Setup(s => s.GetCompressedProduct(It.IsAny<long>())).Returns(new Product());
            _rep.Setup(s => s.GetProtectedProduct(It.IsAny<long>())).Returns(new Product());
            _rep.Setup(s => s.GetOriginalProduct(It.IsAny<long>())).Returns(new Product());
            _rep.Setup(s => s.Update(It.IsAny<Product>())).Returns(new Product());
            _rep.Setup(x => x.Find(It.IsAny<Expression<Func<Product, bool>>>())).Returns(new List<Product>());

            _mockRep = _rep.Object;

            _productService = new ProductService(_mockRep);

            var productGenerator = Generator
                .For<UploadModel>()
                .For(x => x.ProductName)
                .ChooseFrom(StaticData.FirstNames)
                .For(x => x.ProductPrice)
                .CreateUsing(() => new Random().Next())
                .For(x => x.Description)
                .ChooseFrom(StaticData.LastNames)
                .For(x => x.UploadProduct)
                .ChooseFrom(StaticData.LoremIpsum);

            _newProducts = productGenerator.Generate(10).ToList();

            ImageConverter converter = new ImageConverter();
            var sourceImageByte = (byte[])converter.ConvertTo(Resources.SourceImage, typeof(byte[]));
            var sourceImageString = Convert.ToBase64String(sourceImageByte);

            foreach (var product in _newProducts)
            {
                product.UploadProduct = sourceImageString;
            }
        }

        [Test]
        public void Product_UploadProductTest()
        {
            var returnProduct = _productService.UploadProducts(_newProducts[0]);

            Assert.IsNotNull(returnProduct);
        }

       [Test]
        public void Product_GetProductTest()
        {
            _productService.UploadProducts(_newProducts[0]);
            var returnProduct = _productService.GetProduct(1);

            Assert.IsNotNull(returnProduct);
        }

        [Test]
        public void Product_DeleteProductByIdTest()
        {
            _productService.UploadProducts(_newProducts[0]);
            var returnProduct = _productService.DeleteProduct(1);

            Assert.IsNotNull(returnProduct);
        }



        [Test]
        public void Product_UpdateProductTest()
        {
            _productService.UploadProducts(_newProducts[0]);
            var returnProduct = _productService.Update(Mapper.Map<Product>(_productService.GetProduct(1)));

            Assert.IsNotNull(returnProduct);
        }

        //[Test]
        //public void Product_FindProductTest()
        //{
        //    Expression<Func<Product, bool>> filter = product => product.ProductName == "Image 1";
        //    var returnProducts = _productService.Find(filter);

        //    Assert.IsNotNull(returnProducts);
        //}

        [Test]
        public void Product_GetProtectedImageTest()
        {
            ImageConverter converter = new ImageConverter();
            var sourceImageByte = (byte[])converter.ConvertTo(Resources.SourceImage, typeof(byte[]));

            var result = ExtensionProductMethods.GetProtectedImage(sourceImageByte);

            var currentPath = String.Format(@"{0}ImageWithWatermark.jpg", Path.GetTempPath());
            using (Stream file = File.OpenWrite(currentPath))
            {
                if (!ReferenceEquals(result, null))
                {
                    file.Write(result, 0, result.Length);
                }
            }
            Assert.IsNotNull(result);
        }

        [Test]
        public void Product_GetCompressImageTest()
        {
            ImageConverter converter = new ImageConverter();
            var sourceImageByte = (byte[])converter.ConvertTo(Resources.SourceImage, typeof(byte[]));

            var result = ExtensionProductMethods.GetCompressedImage(sourceImageByte);
            var sb = new StringBuilder();
            var currentPath = String.Format(@"{0}ResizedImage.jpg", Path.GetTempPath());
            using (Stream file = File.OpenWrite(currentPath))
            {
                if (!ReferenceEquals(result, null))
                {
                    file.Write(result, 0, result.Length);
                }
            }
            Assert.IsNotNull(result);
        }

        [Test]
        public void Product_FindTest()
        {
            var f = new List<FindDto>() {
                new FindDto() {Name = "ProductPrice", Op = ">", Value = 100},
                new FindDto() { Name = "ProductPrice", Op = "<", Value = 200 },
                new FindDto() { Name = "Name", Op = "=", Value = "pic"}
            };

            _productService.Find(f);

            Assert.IsNotNull(0);
        }

        //[Test]
        //public void Product_GetCompressMusicTest()
        //{
        //    FileStream stream = File.OpenRead(@"d:\1.mp3");
        //    var sourceMusicByte = new byte[stream.Length];
        //    stream.Read(sourceMusicByte, 0, sourceMusicByte.Length);
        //    stream.Close();

        //    var content = Encoding.ASCII.GetString(sourceMusicByte);

        //    var targetLength = sourceMusicByte.Length / 10;
        //    byte[] resultMusicByte = new byte[targetLength];
        //    Array.Copy(sourceMusicByte,resultMusicByte,targetLength);


        //    using (Stream file = File.OpenWrite(@"d:\2.mp3"))
        //    {
        //        if (!ReferenceEquals(resultMusicByte, null))
        //        {
        //            file.Write(resultMusicByte, 0, resultMusicByte.Length);
        //        }
        //    }
        //    Assert.IsNotNull(resultMusicByte);
        //}

        //[Test]
        //public void Product_GetCompressVideoTest()
        //{
        //    FileStream stream = File.OpenRead(@"d:\1.mp4");
        //    var sourceVideoByte = new byte[stream.Length];
        //    stream.Read(sourceVideoByte, 0, sourceVideoByte.Length);
        //    stream.Close();

        //    var content = Encoding.ASCII.GetString(sourceVideoByte);

        //    var targetLength = sourceVideoByte.Length / 10;
        //    byte[] resultVideoByte = new byte[targetLength];
        //    Array.Copy(sourceVideoByte, resultVideoByte, targetLength);


        //    using (Stream file = File.OpenWrite(@"d:\2.mp4"))
        //    {
        //        if (!ReferenceEquals(resultVideoByte, null))
        //        {
        //            file.Write(resultVideoByte, 0, resultVideoByte.Length);
        //        }
        //    }
        //    Assert.IsNotNull(resultVideoByte);
        //}
    }
}

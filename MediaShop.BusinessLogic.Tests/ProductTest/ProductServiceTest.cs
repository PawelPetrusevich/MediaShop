using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using AutoMapper;
using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Dto;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Models.Content;
using Moq;
using NUnit.Framework;
using SampleDataGenerator;
using System.Linq.Expressions;
using MediaShop.BusinessLogic.ExtensionMethods;
using MediaShop.BusinessLogic.Services;
using MediaShop.BusinessLogic.Tests.Properties;
using MediaShop.Common.Dto.Product;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Models;
using MediaShop.Common.Models.User;

namespace MediaShop.BusinessLogic.Tests.ProductTest
{

    [TestFixture]
    public class ProductServiceTest
    {

        private Mock<IProductRepository> _rep;

        private Mock<ICartRepository> _cartRep;

        private Mock<IAccountRepository> _accountRepository;

        private IProductRepository _mockRep;

        private ICartRepository _mockCartRep;

        private IAccountRepository _mockAccountRepository;

        private ProductService _productService;

        private List<UploadProductModel> _newProducts;

        private const long creatorId = 1;

        public ProductServiceTest()
        {
            Mapper.Reset();
            Mapper.Initialize(config =>
            {
                config.CreateMap<Product, ProductDto>().ReverseMap();
                config.CreateMap<Product, ProductInfoDto>().ReverseMap();
                config.CreateMap<Product, UploadProductModel>().ReverseMap();
                config.CreateMap<Product, CompressedProductDTO>().ReverseMap();
                config.CreateMap<Product, OriginalProductDTO>().ReverseMap();
                config.CreateMap<ContentCart, CompressedProductDTO>().ReverseMap();
                config.CreateMap<ContentCart, OriginalProductDTO>().ReverseMap();
            });
        }

        [SetUp]
        public void Init()
        {
            _rep = new Mock<IProductRepository>();
            _rep.Setup(s => s.Add(It.IsAny<Product>())).Returns(new Product());
            _rep.Setup(s => s.Delete(It.IsAny<long>())).Returns(new Product());
            _rep.Setup(s => s.SoftDelete(It.IsAny<long>())).Returns(new Product());
            _rep.Setup(s => s.Get(It.IsAny<long>())).Returns(new Product());
            _rep.Setup(s => s.GetListOnSale()).Returns(new List<Product>() { new Product() });
            _rep.Setup(s => s.Update(It.IsAny<Product>())).Returns(new Product());
            _rep.Setup(x => x.Find(It.IsAny<Expression<Func<Product, bool>>>())).Returns(new List<Product>() { new Product() });
            _rep.Setup(s => s.AddAsync(It.IsAny<Product>())).Returns(Task.FromResult(new Product()));
            _rep.Setup(s => s.DeleteAsync(It.IsAny<long>())).Returns(Task.FromResult(new Product()));
            _rep.Setup(s => s.FindAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .Returns(Task.FromResult<IEnumerable<Product>>(new List<Product>()));
            _rep.Setup(s => s.GetAsync(It.IsAny<long>())).Returns(Task.FromResult(new Product()));
            _rep.Setup(s => s.GetListOnSaleAsync()).Returns(Task.FromResult<IEnumerable<Product>>(new List<Product> { new Product() }));
            _rep.Setup(s => s.SoftDeleteAsync(It.IsAny<long>())).Returns(Task.FromResult(new Product()));
            _rep.Setup(s => s.UpdateAsync(It.IsAny<Product>())).Returns(Task.FromResult(new Product()));

            _mockRep = _rep.Object;

            _cartRep = new Mock<ICartRepository>();
            _cartRep.Setup(x => x.Find(It.IsAny<Expression<Func<ContentCart, bool>>>())).Returns(new List<ContentCart>() { new ContentCart() });
            _mockCartRep = _cartRep.Object;

            _accountRepository = new Mock<IAccountRepository>();
            _accountRepository.Setup(x => x.Get(It.IsAny<long>())).Returns(new AccountDbModel());
            _mockAccountRepository = _accountRepository.Object;

            _productService = new ProductService(_mockRep, _mockCartRep, _mockAccountRepository);

            var productGenerator = Generator
                .For<UploadProductModel>()
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
            var returnProduct = _productService.UploadProducts(_newProducts[0], creatorId);

            Assert.IsNotNull(returnProduct);
        }

        [Test]
        public void Product_UploadProductAsyncTest()
        {
            var product = _productService.UploadProductsAsync(_newProducts[1], creatorId);

            Assert.IsNotNull(product);
        }

        [Test]
        public void Product_GetProductTest()
        {
            _productService.UploadProducts(_newProducts[0], creatorId);
            var returnProduct = _productService.GetById(1);

            Assert.IsNotNull(returnProduct);
        }
        [Test]
        public void Product_GetProductTestAsync()
        {
            for (int i = 0; i < _newProducts.Count; i++)
            {
                _productService.UploadProductsAsync(_newProducts[i], creatorId);
            }
            for (int i = 0; i < _newProducts.Count; i++)
            {
                var returnProduct = _productService.GetById(i + 1);
                Assert.IsNotNull(returnProduct);
            }
        }

        [Test]
        public void Product_DeleteProductByIdTest()
        {
            _productService.UploadProducts(_newProducts[0], creatorId);

            long productId = 1;
            Assert.Throws(typeof(InvalidOperationException),()=>{ _productService.SoftDeleteById(1, creatorId); });

        }

        [Test]
        public void Product_DeleteProductByIdAsync()
        {
            for (int i = 0; i < _newProducts.Count; i++)
            {
                _productService.UploadProductsAsync(_newProducts[i], creatorId);
            }
            for (int i = 0; i < _newProducts.Count; i++)
            {
                var returnProduct = _productService.SoftDeleteByIdAsync(i + 1,creatorId);
                Assert.IsNotNull(returnProduct);
            }
        }


        [Test]
        public void Product_FindProductTest()
        {
            var filter = new List<ProductSearchModel>() {
                new ProductSearchModel() {LeftValue = "ProductPrice", Operand = ">", RightValue = 100},
                new ProductSearchModel() {LeftValue = "ProductName", Operand = "Contains",RightValue = "Image"}
            };

            var returnProducts = _productService.Find(filter);

            Assert.IsNotNull(returnProducts);
        }

        [Test]
        public void Product_FindProductTestAsync()
        {
            var filter = new List<ProductSearchModel>() {
                new ProductSearchModel() {LeftValue = "ProductPrice", Operand = ">", RightValue = 100},
                new ProductSearchModel() {LeftValue = "ProductName", Operand = "Contains",RightValue = "Image"}
            };

            var returnProducts = _productService.FindAsync(filter);

            Assert.IsNotNull(returnProducts);
        }

        [Test]
        public void Product_GetListPurshasedProducts()
        {
            var returnProducts = _productService.GetListPurshasedProducts(1);

            Assert.IsNotNull(returnProducts);
        }

        [Test]
        public void Product_GetListPurshasedProductsAsync()
        {
            var returnProducts = _productService.GetListPurshasedProductsAsync(1);

            Assert.IsNotNull(returnProducts);
        }

        [Test]
        public void Product_GetLisProductsOnSale()
        {
            var returnProducts = _productService.GetListOnSale();

            Assert.IsNotNull(returnProducts);
        }

        [Test]
        public void Product_GetLisProductsOnSaleAsync()
        {
            var returnProducts = _productService.GetListOnSaleAsync();

            Assert.IsNotNull(returnProducts);
        }

        [Test]
        public void Product_GetOriginalPurshasedProducts()
        {
            var returnProduct = _productService.GetOriginalPurshasedProduct(1, 1);

            Assert.IsNotNull(returnProduct);
        }

        [Test]
        public void Product_GetOriginalPurshasedProductsAsync()
        {
            var returnProduct = _productService.GetOriginalPurshasedProductAsync(1, 1);

            Assert.IsNotNull(returnProduct);
        }

        [Test]
        public void Product_GetProtectedImageTest()
        {
            ImageConverter converter = new ImageConverter();
            var sourceImageByte = (byte[])converter.ConvertTo(Resources.SourceImage, typeof(byte[]));

            var result = ExtensionProductMethods.GetProtectedImage(sourceImageByte);

            Assert.IsNotNull(result);
        }

        [Test]
        public void Product_GetCompressImageTest()
        {
            ImageConverter converter = new ImageConverter();
            var sourceImageByte = (byte[])converter.ConvertTo(Resources.SourceImage, typeof(byte[]));

            var result = ExtensionProductMethods.GetCompressedImage(sourceImageByte);

            Assert.IsNotNull(result);
        }

        [Test]
        public void Product_GetSearchPropeprtiesProduct()
        {
            Product product = new Product();

            Assert.GreaterOrEqual(ExtensionProductMethods.GetProductSearchPropeprties(null).Count(), 0);
        }
    }
}

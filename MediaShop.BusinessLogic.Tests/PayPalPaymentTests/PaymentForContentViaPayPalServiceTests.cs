using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AutoMapper;
using NUnit.Framework;
using PayPal.Api;
using MediaShop.BusinessLogic.Services;
using MediaShop.Common.Interfaces.Repositories;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models;
using MediaShop.Common.Models.PaymentModel;
using MediaShop.Common.Exceptions.PaymentExceptions;
using MediaShop.Common.Exceptions.CartExceptions;
using MediaShop.Common;



namespace MediaShop.BusinessLogic.Tests.PayPalPaymentTests
{
    [TestFixture]
    public class PaymentForContentViaPayPalServiceTests
    {
        private Mock<IPayPalPaymentRepository> mockPayment;

        private Mock<IDefrayalRepository> mockDefrayal;

        private Mock<ICartService<ContentCartDto>> mockCart;

        private Cart cart;

        private Cart emptyCart;

        private Payment payment;

        private PayPalPaymentDbModel payPalPaymentDbModel;


        public PaymentForContentViaPayPalServiceTests()
        {
            Mapper.Reset();

            Mapper.Reset();
            // Create Mapper for testing
            Mapper.Initialize(x =>
            {
                x.AddProfile<MapperProfile>();
            });
        }

        [SetUp]
        public void Inintialize()
        {
            var mockPayPalPaymentRepository = new Mock<IPayPalPaymentRepository>();
            mockPayment = mockPayPalPaymentRepository;
            var mockDefrayalRepository = new Mock<IDefrayalRepository>();
            mockDefrayal = mockDefrayalRepository;
            var mockCartService = new Mock<ICartService<ContentCartDto>>();
            mockCart = mockCartService;

            payment = new Payment()
            {
                id = "PAY-5TP98833VT941445HLJ242PI",
                state = "approved",
                transactions = new List<Transaction>
                {
                    new Transaction()
                    {
                        amount = new Amount()
                        {
                            currency = "USD",
                            total = "60.00"
                        },
                        item_list = new ItemList()
                        {
                            items = new List<Item>
                            {
                                new Item() { name = "Music1", price = "10", sku = "1" },
                                new Item() { name = "Music2", price = "20", sku = "2" },
                                new Item() { name = "Music3", price = "30", sku = "3" }
                            }
                        }
                    },
                }
            };

            cart = new Cart()
            {
                ContentCartDtoCollection = new List<ContentCartDto>
                {
                    new ContentCartDto { Id = 1, CreatorId = 1, ContentId = 1, ContentName = "Music1", PriceItem = new decimal(10.00)},
                    new ContentCartDto { Id = 2, CreatorId = 1, ContentId = 2, ContentName = "Music2", PriceItem = new decimal(20.00)},
                    new ContentCartDto { Id = 3, CreatorId = 1, ContentId = 2, ContentName = "Music3", PriceItem = new decimal(30.00)},
                },
                PriceAllItemsCollection = new decimal(60.00),
                CountItemsInCollection = 3
            };

            emptyCart = new Cart();

            payPalPaymentDbModel = new PayPalPaymentDbModel()
            {
                Id = 1,
                CreatorId = 1,
                PaymentId = "PAY-5TP98833VT941445HLJ242PI"
            };
        }

        [Test]
        public void Add_New_Payment_If_Data_Valid()
        {
            // Object as return in method SetState
            var contentCartDtoInPaid = new ContentCartDto()
            { ContentId = 5, StateContent = Common.Enums.CartEnums.StateCartContent.InPaid };

            // Setup mockCart
            mockCart.Setup(item => item.SetState(It.IsAny<long>(), It.IsAny<long>(),
                It.Is<MediaShop.Common.Enums.CartEnums.StateCartContent>
                (s => s == MediaShop.Common.Enums.CartEnums.StateCartContent.InPaid ||
                s == MediaShop.Common.Enums.CartEnums.StateCartContent.InCart)))
                .Returns(() => contentCartDtoInPaid);
            mockCart.Setup(item => item.GetCart(It.IsAny<long>()))
                .Returns(() => cart);

            // Object as return in method Add
            var defrayalAddObject = new DefrayalDbModel() { Id = 0, ContentId = 5 };

            //Setup mockDefrayal
            mockDefrayal.Setup(item => item.Add(It.IsAny<DefrayalDbModel>()))
                .Returns(() => defrayalAddObject).Callback(() => defrayalAddObject.Id++);

            // Setup mockPayment
            mockPayment.Setup(item => item.Find(It.IsAny<Expression<Func<PayPalPaymentDbModel, bool>>>()))
                .Returns(() => new Collection<PayPalPaymentDbModel>());
            mockPayment.Setup(item => item.Add(It.IsAny<PayPalPaymentDbModel>()))
                .Returns(() => payPalPaymentDbModel).Callback(() => payPalPaymentDbModel.Id++);
            
            // Create object PayPalService
            var payPalService = new PayPalPaymentService(mockPayment.Object, mockDefrayal.Object, mockCart.Object);

            // Write result method AddPayment in actual1
            var actual1 = payPalService.AddPayment(payment,1);

            Assert.AreEqual("USD", actual1.Currency);
            Assert.AreEqual(60, actual1.Total);
            Assert.AreEqual(3, actual1.Items.Count);
        }

        [Test]
        public void Add_New_Payment_If_Payment_Is_Null()
        {
            // Create object PayPalService
            var payPalService = new PayPalPaymentService(mockPayment.Object, mockDefrayal.Object, mockCart.Object);

            PayPal.Api.Payment paymentTest = null;

            // Write result method AddPayment in actual1
            Assert.Throws<InvalideDecerializableExceptions>(() => payPalService.AddPayment(paymentTest,1));
        }

        [Test]
        public void Add_New_Payment_If_Payment_Exist_In_Payment()
        {
            var collectionFind = new Collection<PayPalPaymentDbModel> { new PayPalPaymentDbModel() };

            // Setup mockPayment
            mockPayment.Setup(item => item.Find(It.IsAny<Expression<Func<PayPalPaymentDbModel, bool>>>()))
                .Returns(() => collectionFind);

            // Create object PayPalService
            var payPalService = new PayPalPaymentService(mockPayment.Object, mockDefrayal.Object, mockCart.Object);

            // Check result method AddPayment in actual1
            Assert.Throws<ExistPaymentException>(() => payPalService.AddPayment(payment,1));
        }

        [Test]
        public void Add_New_Payment_If_Payment_State_Is_Null()
        {
            // Setup mockPayment
            mockPayment.Setup(item => item.Find(It.IsAny<Expression<Func<PayPalPaymentDbModel, bool>>>()))
                .Returns(() => new Collection<PayPalPaymentDbModel>());

            payment.state = null;

            // Create object PayPalService
            var payPalService = new PayPalPaymentService(mockPayment.Object, mockDefrayal.Object, mockCart.Object);

            // Check result method AddPayment in actual1
            Assert.Throws<InvalideDecerializableExceptions>(() => payPalService.AddPayment(payment,1));
        }

        [Test]
        public void Add_New_Payment_If_Payment_State_Is_Created()
        {
            // Setup mockPayment
            mockPayment.Setup(item => item.Find(It.IsAny<Expression<Func<PayPalPaymentDbModel, bool>>>()))
                .Returns(() => new Collection<PayPalPaymentDbModel>());

            payment.state = "created";

            // Create object PayPalService
            var payPalService = new PayPalPaymentService(mockPayment.Object, mockDefrayal.Object, mockCart.Object);

            // Check result method AddPayment in actual1
            Assert.Throws<OperationPaymentException>(() => payPalService.AddPayment(payment,1));
        }

        [Test]
        public void Add_New_Payment_If_Payment_State_Is_Failed()
        {
            // Setup mockPayment
            mockPayment.Setup(item => item.Find(It.IsAny<Expression<Func<PayPalPaymentDbModel, bool>>>()))
                .Returns(() => new Collection<PayPalPaymentDbModel>());

            payment.state = "failed";

            // Create object PayPalService
            var payPalService = new PayPalPaymentService(mockPayment.Object, mockDefrayal.Object, mockCart.Object);

            // Check result method AddPayment in actual1
            Assert.Throws<OperationPaymentException>(() => payPalService.AddPayment(payment,1));
        }

        [Test]
        public void Add_New_Payment_If_Operation_SetState_Is_Invalid()
        {
            // Setup mockCart
            mockCart.Setup(item => item.SetState(It.IsAny<long>(), It.IsAny<long>(),
                It.Is<MediaShop.Common.Enums.CartEnums.StateCartContent>
                (s => s == MediaShop.Common.Enums.CartEnums.StateCartContent.InPaid ||
                s == MediaShop.Common.Enums.CartEnums.StateCartContent.InCart)))
                .Returns(() => throw new UpdateContentInCartExseptions());

            // Setup mockPayment
            mockPayment.Setup(item => item.Find(It.IsAny<Expression<Func<PayPalPaymentDbModel, bool>>>()))
                .Returns(() => new Collection<PayPalPaymentDbModel>());
            mockPayment.Setup(item => item.Add(It.IsAny<PayPalPaymentDbModel>()))
                .Returns(() => payPalPaymentDbModel).Callback(() => payPalPaymentDbModel.Id++);

            // Create object PayPalService
            var payPalService = new PayPalPaymentService(mockPayment.Object, mockDefrayal.Object, mockCart.Object);

            // Check result method AddPayment in actual1
            Assert.Throws<UpdateContentInCartExseptions>(() => payPalService.AddPayment(payment,1));
        }
    }
}

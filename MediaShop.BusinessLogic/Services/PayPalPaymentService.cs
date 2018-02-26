namespace MediaShop.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediaShop.BusinessLogic.ExtensionMethods;
    using MediaShop.BusinessLogic.Properties;
    using MediaShop.Common.Dto.Payment;
    using MediaShop.Common.Enums.PaymentEnums;
    using MediaShop.Common.Exceptions.CartExceptions;
    using MediaShop.Common.Exceptions.PaymentExceptions;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.PaymentModel;
    using PayPal;
    using PayPal.Api;

    /// <summary>
    /// Class PaymentService
    /// </summary>
    public class PayPalPaymentService : IPayPalPaymentService
    {
        private readonly IPayPalPaymentRepository repositoryPayment;

        private readonly IDefrayalRepository repositoryDefrayal;

        private readonly ICartService<ContentCartDto> serviceCart;

        private List<long> listIdForRollBack;

        /// <summary>
        /// Initializes a new instance of the <see cref="PayPalPaymentService"/> class.
        /// </summary>
        /// <param name="paymentRepository">repository IPayPalPaymentRepository</param>
        /// <param name="defrayalRepository">repository IDefrayalRepository</param>
        /// <param name="cartService">service ICartService</param>
        public PayPalPaymentService(IPayPalPaymentRepository paymentRepository, IDefrayalRepository defrayalRepository, ICartService<ContentCartDto> cartService)
        {
            this.repositoryPayment = paymentRepository;
            this.repositoryDefrayal = defrayalRepository;
            this.serviceCart = cartService;
        }

        /// <summary>
        /// Add object payment in repository
        /// </summary>
        /// <param name="payment">object Payment after decerializer</param>
        /// <param name="userId">userId</param>
        /// <returns>object payment</returns>
        public PayPalPaymentDto AddPayment(PayPal.Api.Payment payment, long userId)
        {
            // Check payment for null
            if (payment == null)
            {
                throw new InvalideDecerializableExceptions(Resources.BadDeserializer);
            }

            // Check exist Payment in repository
            if (this.ExistInPayment(payment.id, userId))
            {
                throw new ExistPaymentException(Resources.ExistPayment);
            }

            try
            {
                // Check state payment
                if (payment.state == null)
                {
                    throw new InvalideDecerializableExceptions(Resources.BadDeserializer);
                }

                var stringFirstUpper = $"{payment.state.Substring(0, 1).ToUpper()}{payment.state.Substring(1)}";

                var enumString = (PaymentStates)Enum.Parse(typeof(PaymentStates), stringFirstUpper);

                if (enumString != PaymentStates.Approved)
                {
                    switch (enumString)
                    {
                        case PaymentStates.Created:
                            throw new OperationPaymentException(Resources.NotApprovedOperationPayment);
                        case PaymentStates.Failed:
                            throw new OperationPaymentException(Resources.FailedOperationPayment);
                    }
                }

                // Change State content in Cart
                this.SetStateItems(payment, Common.Enums.CartEnums.StateCartContent.InPaid, userId);

                // Mapping PaymentTransaction to PaymentDbModel
                var paymentDbModel = Mapper.Map<PayPalPaymentDbModel>(payment);

                paymentDbModel.CreatorId = userId;

                // Add object PaymentDbModel in database
                var paymentResult = this.repositoryPayment.Add(paymentDbModel);

                // throw new AddPaymentException
                if (paymentResult == null)
                {
                    throw new AddPaymentException(Resources.BadAddPayment);
                }

                // else create PayPalPaymentDto and return user
                var paymentDto = this.CreatePayPalPaymentDto(payment);

                return paymentDto;
            }
            catch (ArgumentNullException error)
            {
                throw new ArgumentNullException(error.Message);
            }
            catch (ArgumentException error)
            {
                throw new ArgumentException(error.Message);
            }
            catch (OverflowException error)
            {
                throw new OverflowException(error.Message);
            }
        }

        /// <summary>
        /// Add object payment in repository
        /// </summary>
        /// <param name="payment">object Payment after decerializer</param>
        /// <param name="userId">userId</param>
        /// <returns>object payment</returns>
        public async Task<PayPalPaymentDto> AddPaymentAsync(PayPal.Api.Payment payment, long userId)
        {
            // Check payment for null
            if (payment == null)
            {
                throw new InvalideDecerializableExceptions(Resources.BadDeserializer);
            }

            // Check exist Payment in repository
            if (await this.ExistInPaymentAsync(payment.id, userId).ConfigureAwait(false))
            {
                throw new ExistPaymentException(Resources.ExistPayment);
            }

            try
            {
                // Check state payment
                if (payment.state == null)
                {
                    throw new InvalideDecerializableExceptions(Resources.BadDeserializer);
                }

                var stringFirstUpper = $"{payment.state.Substring(0, 1).ToUpper()}{payment.state.Substring(1)}";

                var enumString = (PaymentStates)Enum.Parse(typeof(PaymentStates), stringFirstUpper);

                if (enumString != PaymentStates.Approved)
                {
                    switch (enumString)
                    {
                        case PaymentStates.Created:
                            throw new OperationPaymentException(Resources.NotApprovedOperationPayment);
                        case PaymentStates.Failed:
                            throw new OperationPaymentException(Resources.FailedOperationPayment);
                    }
                }

                // Change State content in Cart
                await this.SetStateItemsAsync(payment, Common.Enums.CartEnums.StateCartContent.InPaid, userId)
                    .ConfigureAwait(false);

                // Mapping PaymentTransaction to PaymentDbModel
                var paymentDbModel = Mapper.Map<PayPalPaymentDbModel>(payment);

                paymentDbModel.CreatorId = userId;

                // Add object PaymentDbModel in database
                var paymentResult = await this.repositoryPayment.AddAsync(paymentDbModel).ConfigureAwait(false);

                // throw new AddPaymentException
                if (paymentResult == null)
                {
                    throw new AddPaymentException(Resources.BadAddPayment);
                }

                // else create PayPalPaymentDto and return user
                var paymentDto = this.CreatePayPalPaymentDto(payment);

                return paymentDto;
            }
            catch (ArgumentNullException error)
            {
                throw new ArgumentNullException(error.Message);
            }
            catch (ArgumentException error)
            {
                throw new ArgumentException(error.Message);
            }
            catch (OverflowException error)
            {
                throw new OverflowException(error.Message);
            }
        }

        /// <summary>
        /// Checking the existence of payment in repository
        /// </summary>
        /// <param name="paymentId">payment id</param>
        /// <param name="userId">userId</param>
        /// <returns>true - payment exist in repository
        /// false - payment doesn`t exist in repository</returns>
        public bool ExistInPayment(string paymentId, long userId) => this.repositoryPayment
            .Find(item => item.PaymentId.Equals(paymentId) & item.CreatorId == userId).Count() != 0;

        /// <summary>
        /// Async checking the existence of payment in repository
        /// </summary>
        /// <param name="paymentId">payment id</param>
        /// <param name="userId">userId</param>
        /// <returns>true - payment exist in repository
        /// false - payment doesn`t exist in repository</returns>
        public async Task<bool> ExistInPaymentAsync(string paymentId, long userId)
        {
            var result = await this.repositoryPayment
                .FindAsync(item => item.PaymentId.Equals(paymentId) & item.CreatorId == userId)
                .ConfigureAwait(false);
            return result.Count() != 0;
        }

        /// <summary>
        /// Method for create PayPalPaymentDto
        /// </summary>
        /// <param name="payment">payment</param>
        /// <returns>object typeof PayPalPaymentDto</returns>
        public PayPalPaymentDto CreatePayPalPaymentDto(PayPal.Api.Payment payment)
        {
            var payPalPaymentDto = new PayPalPaymentDto() { Items = new List<ItemDto>() };

            payPalPaymentDto.Currency = payment.transactions[0].amount.currency;
            var parseCulture = new CultureInfo("en-US");

            foreach (PayPal.Api.Transaction s in payment.transactions)
            {
                payPalPaymentDto.Total = payPalPaymentDto.Total + Convert.ToDecimal(s.amount.total.Replace('.', ','));
                foreach (PayPal.Api.Item item in s.item_list.items)
                {
                    payPalPaymentDto.Items.Add(Mapper.Map<PayPal.Api.Item, ItemDto>(item));
                }
            }

            return payPalPaymentDto;
        }

        /// <summary>
        /// Method for set state item all product in payment
        /// </summary>
        /// <param name="payment">payment</param>
        /// <param name="state">state </param>
        /// <param name="userId">userId</param>
        public void SetStateItems(PayPal.Api.Payment payment, MediaShop.Common.Enums.CartEnums.StateCartContent state, long userId)
        {
            try
            {
                // List for rollback
                this.listIdForRollBack = new List<long>();

                foreach (PayPal.Api.Transaction tran in payment.transactions)
                {
                    foreach (Item item in tran.item_list.items)
                    {
                        // 3.1 Change state product
                        var resultChangeState = this.serviceCart.SetState(Convert.ToInt64(item.sku), userId, state);

                        // Write id for rollback
                        this.listIdForRollBack.Add(resultChangeState.ContentId);

                        // 3.2 Transferring product in Defrayal from ContentCart
                        var objectDefrayal = Mapper.Map<ContentCartDto, DefrayalDbModel>(resultChangeState);

                        objectDefrayal.CreatorId = userId;

                        // 3.3 Save object Defrayal in repository
                        var resultSaveDefrayal = this.repositoryDefrayal.Add(objectDefrayal);
                    }
                }
            }
            catch (UpdateContentInCartExseptions error)
            {
                // Rollback change set state content
                foreach (long contentId in this.listIdForRollBack)
                {
                        // 3.1 Change state product in cart
                        var resultChangeState = this.serviceCart.SetState(contentId, userId, Common.Enums.CartEnums.StateCartContent.InBought);
                }

                throw new UpdateContentInCartExseptions(error.Message);
            }
            catch (AddDefrayalException error)
            {
                // Rollback change add new defrayal
                foreach (long contentId in this.listIdForRollBack)
                {
                    // 3.1 Change state product in cart
                    var resultChangeState = this.serviceCart.SetState(contentId, userId, Common.Enums.CartEnums.StateCartContent.InBought);

                    // 3.2 Transferring product in Defrayal from ContentCart
                    var objectDefrayal = Mapper.Map<ContentCartDto, DefrayalDbModel>(resultChangeState);

                        // 3.3 Save object Defrayal in repository
                    var resultSaveDefrayal = this.DeleteDefrayal(objectDefrayal);
                }

                throw new AddDefrayalException(error.Message);
            }
        }

        /// <summary>
        /// Async method for set state item all product in payment
        /// </summary>
        /// <param name="payment">payment</param>
        /// <param name="state">state </param>
        /// <param name="userId">userId</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.></returns>
        public async Task SetStateItemsAsync(PayPal.Api.Payment payment, MediaShop.Common.Enums.CartEnums.StateCartContent state, long userId)
        {
            try
            {
                // List for rollback
                this.listIdForRollBack = new List<long>();

                foreach (PayPal.Api.Transaction tran in payment.transactions)
                {
                    foreach (Item item in tran.item_list.items)
                    {
                        // 3.1 Change state product
                        var resultChangeState = await this.serviceCart.SetStateAsync(Convert.ToInt64(item.sku), userId, state)
                            .ConfigureAwait(false);

                        // Write id for rollback
                        this.listIdForRollBack.Add(resultChangeState.ContentId);

                        // 3.2 Transferring product in Defrayal from ContentCart
                        var objectDefrayal = Mapper.Map<ContentCartDto, DefrayalDbModel>(resultChangeState);

                        objectDefrayal.CreatorId = userId;

                        // 3.3 Save object Defrayal in repository
                        var resultSaveDefrayal = await this.repositoryDefrayal.AddAsync(objectDefrayal).ConfigureAwait(false);
                    }
                }
            }
            catch (UpdateContentInCartExseptions error)
            {
                // Rollback change set state content
                foreach (long contentId in this.listIdForRollBack)
                {
                    // 3.1 Change state product in cart
                    var resultChangeState = await this.serviceCart
                        .SetStateAsync(contentId, userId, Common.Enums.CartEnums.StateCartContent.InBought)
                        .ConfigureAwait(false);
                }

                throw new UpdateContentInCartExseptions(error.Message);
            }
            catch (AddDefrayalException error)
            {
                // Rollback change add new defrayal
                foreach (long contentId in this.listIdForRollBack)
                {
                    // 3.1 Change state product in cart
                    var resultChangeState = await this.serviceCart
                        .SetStateAsync(contentId, userId, Common.Enums.CartEnums.StateCartContent.InBought)
                        .ConfigureAwait(false);

                    // 3.2 Transferring product in Defrayal from ContentCart
                    var objectDefrayal = Mapper.Map<ContentCartDto, DefrayalDbModel>(resultChangeState);

                    // 3.3 Save object Defrayal in repository
                    var resultSaveDefrayal = await this.DeleteDefrayalAsync(objectDefrayal).ConfigureAwait(false);
                }

                throw new AddDefrayalException(error.Message);
            }
        }

        /// <summary>
        /// Method for delete object typeof DefrayalDbModel in repository
        /// </summary>
        /// <param name="modelDefrayal">object typeof DefrayalDbModel</param>
        /// <returns>object DefrayalDbModel after delete in repository</returns>
        public DefrayalDbModel DeleteDefrayal(DefrayalDbModel modelDefrayal)
        {
            if (modelDefrayal == null)
            {
                throw new ArgumentNullException(Resources.NullModelDefrayal);
            }

            var resultDeleteDefrayal = this.repositoryDefrayal.Delete(modelDefrayal);

            if (resultDeleteDefrayal == null)
            {
                throw new DeleteDefrayalException(Resources.InvalidDeleteOperationDefrayal);
            }

            return resultDeleteDefrayal;
        }

        /// <summary>
        /// Async method for delete object typeof DefrayalDbModel in repository
        /// </summary>
        /// <param name="modelDefrayal">object typeof DefrayalDbModel</param>
        /// <returns>object DefrayalDbModel after delete in repository</returns>
        public async Task<DefrayalDbModel> DeleteDefrayalAsync(DefrayalDbModel modelDefrayal)
        {
            if (modelDefrayal == null)
            {
                throw new ArgumentNullException(Resources.NullModelDefrayal);
            }

            var resultDeleteDefrayal = await this.repositoryDefrayal
                .DeleteAsync(modelDefrayal)
                .ConfigureAwait(false);

            if (resultDeleteDefrayal == null)
            {
                throw new DeleteDefrayalException(Resources.InvalidDeleteOperationDefrayal);
            }

            return resultDeleteDefrayal;
        }

        /// <summary>
        /// Method for delete object typeof DefrayalDbModel in repository
        /// </summary>
        /// <param name="modelDefrayal">object typeof DefrayalDbModel</param>
        /// <returns>object DefrayalDbModel after add in repository</returns>
        public DefrayalDbModel AddInDefrayal(DefrayalDbModel modelDefrayal)
        {
            if (modelDefrayal == null)
            {
                throw new ArgumentNullException(Resources.NullModelDefrayal);
            }

            var resultAddDefrayal = this.repositoryDefrayal.Add(modelDefrayal);

            if (resultAddDefrayal == null)
            {
                throw new AddDefrayalException(Resources.InvalidAddOperationDefrayal);
            }

            return resultAddDefrayal;
        }

        /// <summary>
        /// Method for delete object typeof DefrayalDbModel in repository
        /// </summary>
        /// <param name="modelDefrayal">object typeof DefrayalDbModel</param>
        /// <returns>object DefrayalDbModel after add in repository</returns>
        public async Task<DefrayalDbModel> AddInDefrayalAsync(DefrayalDbModel modelDefrayal)
        {
            if (modelDefrayal == null)
            {
                throw new ArgumentNullException(Resources.NullModelDefrayal);
            }

            var resultAddDefrayal = await this.repositoryDefrayal
                .AddAsync(modelDefrayal)
                .ConfigureAwait(false);

            if (resultAddDefrayal == null)
            {
                throw new AddDefrayalException(Resources.InvalidAddOperationDefrayal);
            }

            return resultAddDefrayal;
        }

        /// <summary>
        /// Create and return new Payment
        /// </summary>
        /// <param name="cart">user Cart</param>
        /// <param name="baseUrl">base uri of Requst</param>
        /// <returns>created Payment</returns>
        public string GetPayment(Cart cart, string baseUrl)
        {
            if (cart == null)
            {
                throw new ArgumentNullException(Resources.NullOrEmptyValue, nameof(cart));
            }

            if (cart.ContentCartDtoCollection == null || (cart.ContentCartDtoCollection.Count() <= 0))
            {
                throw new EmptyCartException(Resources.CountContentInCartIsNull);
            }

            if (cart.PriceAllItemsCollection != cart.ContentCartDtoCollection.Sum<ContentCartDto>(x => x.PriceItem) || cart.PriceAllItemsCollection <= 0)
            {
                throw new ContentCartPriceException(Resources.InvaliContentCartValueOfPrice);
            }

            var config = Configuration.GetConfig();

            var accessToken = new OAuthTokenCredential(config).GetAccessToken();

            var apiContext = new APIContext(accessToken);

            string payerId = "payerId"; // Request.Params["PayerID"];

            // ###Items
            // Items within a transaction.
            var itemList = new PayPal.Api.ItemList();

            itemList.items = this.GetItemList(cart.ContentCartDtoCollection);

            // ###Payer
            // A resource representing a Payer that funds a payment
            // Payment Method
            // as `paypal`
            var payer = new PayPal.Api.Payer() { payment_method = "paypal" };

            // ###Redirect URLS
            // These URLs will determine how the user is redirected from PayPal once they have either approved or canceled the payment.
            var redirUrls = this.GetRedirectUrls(baseUrl);

            // ###Details
            // Let's you specify details of a payment amount.
            var tax = this.GetTax(cart.PriceAllItemsCollection);

            var details = new PayPal.Api.Details()
            {
                tax = decimal.Round(tax, 2).ToString(CultureInfo.CreateSpecificCulture("en-US")),
                shipping = "0",
                subtotal = decimal.Round(cart.PriceAllItemsCollection, 2).ToString(CultureInfo.CreateSpecificCulture("en-US"))
            };

            // ###Amount
            // Let's you specify a payment amount.
            var amount = new PayPal.Api.Amount()
            {
                currency = "USD",
                total = decimal.Round(cart.PriceAllItemsCollection + tax, 2).ToString(CultureInfo.CreateSpecificCulture("en-US")),

                // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            // ###Transaction
            // A transaction defines the contract of a
            // payment - what is the payment for and who
            // is fulfilling it.
            var transactionList = new List<PayPal.Api.Transaction>();

            // The Payment creation API requires a list of
            // Transaction; add the created `Transaction`
            // to a List
            transactionList.Add(new PayPal.Api.Transaction()
            {
                description = "Payd contents.",
                invoice_number = new System.Random().Next(999999).ToString(), // Get id number transaction
                amount = amount,
                item_list = itemList
            });

            // ###Payment
            // A Payment Resource; create one using
            // the above types and intent as `sale` or `authorize`
            var payment = new PayPal.Api.Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a valid APIContext
            var createdPayment = payment.Create(apiContext);

            // return Redirect(createdPayment.GetApprovalUrl(true));
            return createdPayment.GetApprovalUrl(true);
        }

        /// <summary>
        /// Executes, or completes, a PayPal payment that the payer has approved
        /// </summary>
        /// <param name="paymentId">paymentId</param>
        /// <param name="userId">users id</param>
        /// <returns>Executed Payment</returns>
        public PayPalPaymentDto ExecutePayment(string paymentId, long userId)
        {
            var config = Configuration.GetConfig();
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);
            var payment = Payment.Get(apiContext, paymentId);
            payment = payment ?? throw new PaymentException(Resources.PaymentIsNullOrEmpty);
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payment.payer.payer_info.payer_id
            };

            var executedPayment = new Payment();
            executedPayment = payment.Execute(apiContext, paymentExecution);
            var result = this.AddPayment(executedPayment, userId);
            return result;
        }

        /// <summary>
        /// Executes, or completes, a PayPal payment that the payer has approved
        /// </summary>
        /// <param name="paymentId">paymentId</param>
        /// <param name="userId">users id</param>
        /// <returns>Executed Payment</returns>
        public async Task<PayPalPaymentDto> ExecutePaymentAsync(string paymentId, long userId)
        {
            var config = Configuration.GetConfig();
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);
            var payment = Payment.Get(apiContext, paymentId);
            payment = payment ?? throw new PaymentException(Resources.PaymentIsNullOrEmpty);
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payment.payer.payer_info.payer_id
            };

            var executedPayment = new Payment();
            executedPayment = payment.Execute(apiContext, paymentExecution);
            var result = await this.AddPaymentAsync(executedPayment, userId).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// Get new redirectUrls
        /// </summary>
        /// <param name="baseUrl">base Uri of Request</param>
        /// <returns>RedirectUrls</returns>
        private RedirectUrls GetRedirectUrls(string baseUrl)
        {
            return new RedirectUrls()
            {
                cancel_url = $"{baseUrl}/cart",
                return_url = $"{baseUrl}/payment-info"
            };
        }

        /// <summary>
        /// Get created ItemList from contentCart
        /// </summary>
        /// <param name="contentCart">content in Cart</param>
        /// <returns>ItemList</returns>
        private List<PayPal.Api.Item> GetItemList(IEnumerable<ContentCartDto> contentCart)
        {
            var itemList = new List<PayPal.Api.Item>();
            foreach (var content in contentCart)
            {
                itemList.Add(new PayPal.Api.Item
                {
                    name = content.ContentName,
                    currency = "USD",
                    price = decimal.Round(content.PriceItem, 2).ToString(CultureInfo.CreateSpecificCulture("en-US")),
                    quantity = "1",
                    sku = content.ContentId.ToString()
                });
            }

            return itemList;
        }

        /// <summary>
        /// Get tax for payment
        /// </summary>
        /// <param name="price">price of content</param>
        /// <returns>tax of payment</returns>
        private decimal GetTax(decimal price)
        {
            return price * new decimal(0.10);
        }
    }
}

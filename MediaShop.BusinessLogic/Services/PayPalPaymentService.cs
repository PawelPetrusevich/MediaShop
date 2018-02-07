namespace MediaShop.BusinessLogic.Services
{
    using System;
    using System.Collections.Generic;
    using MediaShop.BusinessLogic.ExtensionMethods;
    using MediaShop.BusinessLogic.Properties;
    using MediaShop.Common.Exceptions.PaymentExceptions;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.PaymentModel;
    using PayPal;
    using PayPal.Api;
    using MediaShop.Common.Enums.PaymentEnums;
    using MediaShop.Common.Interfaces.Repositories;
    using MediaShop.Common.Dto.Payment;
    using AutoMapper;

    /// <summary>
    /// Class PaymentService
    /// </summary>
    public class PayPalPaymentService : IPayPalPaymentService
    {
        private readonly IPayPalPaymentRepository repositoryPayment;

        /// <summary>
        /// Initializes a new instance of the <see cref="PayPalPaymentService"/> class.
        /// </summary>
        /// <param name="paymentRepository">instance repository PaymentRepository</param>
        public PayPalPaymentService(IPayPalPaymentRepository paymentRepository)
        {
            this.repositoryPayment = paymentRepository;
        }

        /// <summary>
        /// Add object payment in repository
        /// </summary>
        /// <param name="payment">object Payment after decerializer</param>
        /// <returns>object payment</returns>
        public PayPalPaymentDto AddPayment(PayPalPayment payment)
        {
            // 1. Check payment for null
            if (payment == null)
            {
                throw new InvalideDecerializableExceptions(Resources.BadDeserializer);
            }

            try
            {
                // 2. Check state payment
                if (payment.State == null)
                {
                    throw new InvalideDecerializableExceptions(Resources.BadDeserializer);
                }

                var stringFirstUpper = $"{payment.State.Substring(0, 1).ToUpper()}{payment.State.Substring(1)}";

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

                // 3. Mapping PaymentTransaction to PaymentDbModel
                var paymentDbModel = Mapper.Map<PayPalPaymentDbModel>(payment);

                // 4. Add object PaymentDbModel in database
                var paymentResult = this.repositoryPayment.Add(paymentDbModel);

                // throw new AddPaymentException
                if (paymentResult == null)
                {
                    throw new AddPaymentException(Resources.BadAddPayment);
                }

                // else mapping Payment => PaymentDto and return user
                var paymentDto = Mapper.Map<PayPalPaymentDto>(payment);

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

        ///// <summary>
        ///// Create and return new Payment
        ///// </summary>
        ///// <param name="cart">user Cart</param>
        ///// <returns>created Payment</returns>
        //public PayPal.Api.Payment GetPayment(Cart cart)
        //{
        //    var config = Configuration.GetConfig();
        //    var accessToken = new OAuthTokenCredential(config).GetAccessToken();
        //    var apiContext = new APIContext(accessToken);
        //    string payerId = "payerId"; // Request.Params["PayerID"];

        //    // ###Items
        //    // Items within a transaction.
        //    var itemList = new PayPal.Api.ItemList();
        //    itemList.items = this.GetItemList(cart.ContentCartDtoCollection);

        //    // ###Payer
        //    // A resource representing a Payer that funds a payment
        //    // Payment Method
        //    // as `paypal`
        //    var payer = new PayPal.Api.Payer() { payment_method = "paypal" };

        //    // ###Redirect URLS
        //    // These URLs will determine how the user is redirected from PayPal once they have either approved or canceled the payment.
        //    var redirUrls = this.GetRedirectUrls();

        //    // ###Details
        //    // Let's you specify details of a payment amount.
        //    var tax = cart.PriceAllItemsCollection * new decimal(0.10);
        //    var details = new PayPal.Api.Details()
        //    {
        //        tax = decimal.Round(tax, 2).ToString().Replace(',', '.'),
        //        shipping = "0",
        //        subtotal = decimal.Round(cart.PriceAllItemsCollection, 2).ToString().Replace(',', '.')
        //    };

        //    // ###Amount
        //    // Let's you specify a payment amount.
        //    var amount = new PayPal.Api.Amount()
        //    {
        //        currency = "USD",
        //        total = decimal.Round(cart.PriceAllItemsCollection + tax, 2).ToString().Replace(',', '.'), // Total must be equal to sum of shipping, tax and subtotal.
        //        details = details
        //    };

        //    // ###Transaction
        //    // A transaction defines the contract of a
        //    // payment - what is the payment for and who
        //    // is fulfilling it.
        //    var transactionList = new List<PayPal.Api.Transaction>();

        //    // The Payment creation API requires a list of
        //    // Transaction; add the created `Transaction`
        //    // to a List
        //    transactionList.Add(new PayPal.Api.Transaction()
        //    {
        //        description = "Payd contents.",
        //        invoice_number = new System.Random().Next(999999).ToString(), // Get id number transaction
        //        amount = amount,
        //        item_list = itemList
        //    });
        //}

        /// <summary>
        /// Get new redirectUrls
        /// </summary>
        /// <returns>RedirectUrls</returns>
        private RedirectUrls GetRedirectUrls()
        {
            // var baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Home/GoodPayd?";
            // var guid = Convert.ToString((new Random()).Next(100000));
            // var redirectUrl = baseURI + "guid=" + guid;
            // new RedirectUrls()
            // {
            //    return_url = redirectUrl,
            //    cancel_url = "http://localhost:8642/Home/FailPayd"
            // };
            return new RedirectUrls()
            {
                cancel_url = "url",
                return_url = "url"
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
                    price = content.PriceItem.ToString(),
                    quantity = "1",
                    sku = content.ContentId.ToString()
                });
            }

            return itemList;
        }
    }
}

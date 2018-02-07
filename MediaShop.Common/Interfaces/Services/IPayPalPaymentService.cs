namespace MediaShop.Common.Interfaces.Services
{
    using MediaShop.Common.Dto.Payment;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.PaymentModel;
    using PayPal.Api;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for payment
    /// </summary>
    public interface IPayPalPaymentService
    {
        /// <summary>
        /// Create and return new Payment
        /// </summary>
        /// <param name="cart">user Cart</param>
        /// <param name="baseUrl">base uri of Requst</param>
        /// <returns>created Payment</returns>
        string GetPayment(Cart cart, string baseUrl);

        /// <summary>
        /// Executes, or completes, a PayPal payment that the payer has approved
        /// </summary>
        /// <param name="paymentId">paymentId</param>
        /// <param name="payerId">payerId</param>
        /// <returns>Executed Payment</returns>
        PayPalPayment ExecutePayment(string paymentId, string payerId);

        /// <summary>
        /// Add new model
        /// </summary>
        /// <param name="payment">object Payment for save in repository</param>
        /// <returns>object Payment that save in repository</returns>
        PayPalPaymentDto AddPayment(PayPalPayment payment);
    }
}

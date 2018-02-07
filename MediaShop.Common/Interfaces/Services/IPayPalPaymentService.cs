namespace MediaShop.Common.Interfaces.Services
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.PaymentModel;
    using MediaShop.Common.Dto.Payment;

    /// <summary>
    /// Interface for payment
    /// </summary>
    public interface IPayPalPaymentService
    {
        ///// <summary>
        ///// Create and return new Payment
        ///// </summary>
        ///// <param name="cart">user Cart</param>
        ///// <returns>created Payment</returns>
        //Payment GetPayment(Cart cart);

        /// <summary>
        /// Add new model
        /// </summary>
        /// <param name="payment">object Payment for save in repository</param>
        /// <returns>object Payment that save in repository</returns>
        PayPalPaymentDto AddPayment(PayPalPayment payment);
    }
}

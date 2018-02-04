namespace MediaShop.Common.Interfaces.Services
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.PaymentModel;

    /// <summary>
    /// Interface for payment
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Method for payment by product
        /// </summary>
        /// <param name="cart">cart</param>
        /// <returns>http requect for redirect</returns>
        string Payment(Cart cart);

        /// <summary>
        /// Add new model
        /// </summary>
        /// <param name="payment">object Payment for save in repository</param>
        /// <returns>object Payment that save in repository</returns>
        Payment AddPayment(Payment payment);
    }
}

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
        /// Create model PaymentTransaction
        /// </summary>
        /// <param name="obj">rezalt request</param>
        /// <returns>model PaymentTransaction</returns>
        PaymentTransaction ConvertJsonToModel(object obj);

        /// <summary>
        /// Add new model
        /// </summary>
        /// <param name="paymentTransaction">model tranzaction</param>
        /// <returns>model add</returns>
        PaymentTransaction Add(PaymentTransaction paymentTransaction);
    }
}

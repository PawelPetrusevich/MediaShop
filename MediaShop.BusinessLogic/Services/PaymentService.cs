namespace MediaShop.BusinessLogic.Services
{
    using MediaShop.BusinessLogic.Properties;
    using MediaShop.Common.Exceptions.PaymentExceptions;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.PaymentModel;

    /// <summary>
    /// Class PaymentService
    /// </summary>
    public class PaymentService : IPaymentService
    {
        /// <summary>
        /// Add object payment in repository
        /// </summary>
        /// <param name="payment">object Payment after decerializer</param>
        /// <returns>object payment</returns>
        public Payment AddPayment(Payment payment)
        {
            // 1. Check payment for null
            if (payment == null)
            {
                throw new InvalideDecerializableExceptions(Resources.BadDeserializer);
            }

            // 2. Check state payment
            // 3. Add Payment to repository Async
            // 4. If add is successfuly then:
            // 4.1 Make Mapping Payment => PaymentDto and return user
            // else throw new Exception
            return payment;
        }

        public string Payment(Cart cart)
        {
            throw new System.NotImplementedException();
        }
    }
}

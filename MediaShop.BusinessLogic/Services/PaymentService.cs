namespace MediaShop.BusinessLogic.Services
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.PaymentModel;

    /// <summary>
    /// Class PaymentService
    /// </summary>
    public class PaymentService : IPaymentService
    {
        public PaymentTransaction Add(PaymentTransaction paymentTransaction)
        {
            throw new System.NotImplementedException();
        }

        public PaymentTransaction ConvertJsonToModel(object obj)
        {
            throw new System.NotImplementedException();
        }

        public string Payment(Cart cart)
        {
            throw new System.NotImplementedException();
        }
    }
}

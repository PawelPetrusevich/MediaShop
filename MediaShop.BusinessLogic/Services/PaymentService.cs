namespace MediaShop.BusinessLogic.Services
{
    using System.Collections.ObjectModel;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models.PaymentModel;

    /// <summary>
    /// Class PaymentService
    /// </summary>
    public class PaymentService : IPaymentService
    {
        public BayContentTransaction PaymentBayer(long contentId)
        {
            throw new System.NotImplementedException();
        }

        public RewardSellerTransaction PaymentSeller(Collection<BayContentTransaction> transaction, long sellerId)
        {
            throw new System.NotImplementedException();
        }

        public WriteDownPercentageStoreTransaction WriteDownPercentageStore(Collection<BayContentTransaction> transaction)
        {
            throw new System.NotImplementedException();
        }
    }
}

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
        /// <summary>
        /// Method of payment for the content by the buyer
        /// </summary>
        /// <param name="contentId">contents identifier</param>
        /// <returns>transaction`s identificator</returns>
        public BayContentTransaction PaymentBayer(long contentId)
        {
            return new BayContentTransaction(); // Need identify logic for PaymentBayer method
        }

        /// <summary>
        /// Method for the write down the percentage of the store for the transaction
        /// </summary>
        /// <param name="transaction">BayContentTransaction transaction</param>
        /// <returns>true - operation is successful
        /// false -  operation is fail</returns>
        public RewardSellerTransaction PaymentSeller(Collection<BayContentTransaction> transaction, long sellerId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Method for payment reward seller
        /// </summary>
        /// <param name="transaction">Collection of BayContentTransaction transaction</param>
        /// <param name="sellerId">seller`s identificator</param>
        /// <returns>true - operation is successful
        /// false -  operation is fail</returns>
        public WriteDownPercentageStoreTransaction WriteDownPercentageStore(Collection<BayContentTransaction> transaction)
        {
            throw new System.NotImplementedException();
        }
    }
}

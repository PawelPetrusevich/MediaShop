namespace MediaShop.Common.Interfaces.Services
{
    using System.Collections.ObjectModel;
    using MediaShop.Common.Models.PaymentModel;
    
    /// <summary>
    /// Interface for payment
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Method of payment for the content by the buyer
        /// </summary>
        /// <param name="contentId">contents identifier</param>
        /// <returns>transaction`s identificator</returns>
        BayContentTransaction PaymentBayer(long contentId);

        /// <summary>
        /// Method for the write down the percentage of the store for the transaction
        /// </summary>
        /// <param name="transaction">BayContentTransaction transaction</param>
        /// <returns>true - operation is successful
        /// false -  operation is fail</returns>
        WriteDownPercentageStoreTransaction WriteDownPercentageStore(Collection<BayContentTransaction> transaction);

        /// <summary>
        /// Method for payment reward seller
        /// </summary>
        /// <param name="transaction">Collection of BayContentTransaction transaction</param>
        /// <param name="sellerId">seller`s identificator</param>
        /// <returns>true - operation is successful
        /// false -  operation is fail</returns>
        RewardSellerTransaction PaymentSeller(Collection<BayContentTransaction> transaction, long sellerId);
    }
}

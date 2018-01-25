namespace MediaShop.Common.Interfaces.Services
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using MediaShop.Common.Models.PaymentModel;

    /// <summary>
    /// Interface for payment
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Get all transaction
        /// </summary>
        /// <returns>collection transactions</returns>
        ICollection<PaymentTransaction> GetAllTransactions();

        /// <summary>
        /// Get all transaction by account
        /// </summary>
        /// <param name="accountId">account`s identificator</param>
        /// <returns>collection transactions</returns>
        ICollection<PaymentTransaction> GetTransactionsByAccount(long accountId);

        /// <summary>
        /// Get all transaction by account
        /// </summary>
        /// <param name="id">transaction`s identificator</param>
        /// <returns>collection transactions</returns>
        PaymentTransaction GetTransaction(long id);

        /// <summary>
        /// Update transaction
        /// </summary>
        /// <param name="id">transaction`s identificator</param>
        /// <returns>transactions after update</returns>
        PaymentTransaction UpdateTransaction(long id);

        /// <summary>
        /// Delete transaction
        /// </summary>
        /// <param name="id">transaction`s identificator</param>
        /// <returns>transaction</returns>
        PaymentTransaction DeleteTransaction(long id);

        /// <summary>
        /// Method of payment for the content by the buyer
        /// </summary>
        /// <param name="contentId">contents identifier</param>
        /// <returns>transaction`s identificator</returns>
        PaymentTransaction PaymentBuyer(long contentId);

        /// <summary>
        /// Method for the write down the percentage of the store for the transaction
        /// </summary>
        /// <param name="transaction">BayContentTransaction transaction</param>
        /// <returns>true - operation is successful
        /// false -  operation is fail</returns>
        PaymentTransaction WriteDownPercentageStore(Collection<PaymentTransaction> transaction);

        /// <summary>
        /// Method for payment reward seller
        /// </summary>
        /// <param name="transaction">Collection of BayContentTransaction transaction</param>
        /// <param name="sellerId">seller`s identificator</param>
        /// <returns>true - operation is successful
        /// false -  operation is fail</returns>
        PaymentTransaction PaymentSeller(Collection<PaymentTransaction> transaction, long sellerId);
    }
}

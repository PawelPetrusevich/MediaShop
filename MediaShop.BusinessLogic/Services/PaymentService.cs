namespace MediaShop.BusinessLogic.Services
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using MediaShop.Common.Interfaces.Services;
    using MediaShop.Common.Models.PaymentModel;

    /// <summary>
    /// Class PaymentService
    /// </summary>
    public class PaymentService : IPaymentService
    {
        /// <summary>
        /// Get all transaction
        /// </summary>
        /// <returns>collection transactions</returns>
        public ICollection<PaymentTransaction> GetAllTransactions()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get all transaction by account
        /// </summary>
        /// <param name="id">transaction`s identificator</param>
        /// <returns>collection transactions</returns>
        public PaymentTransaction GetTransaction(long id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Get all transaction by account
        /// </summary>
        /// <param name="accountID">account`s identificator</param>
        /// <returns>collection transactions</returns>
        public ICollection<PaymentTransaction> GetTransactionsByAccount(long accountID)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Method of payment for the content by the buyer
        /// </summary>
        /// <param name="contentId">contents identifier</param>
        /// <returns>transaction`s identificator</returns>
        public PaymentTransaction PaymentBuyer(long contentId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Method for the write down the percentage of the store for the transactions
        /// </summary>
        /// <param name="transaction">BayContentTransaction transaction</param>
        /// <returns>true - operation is successful
        /// false -  operation is fail</returns>
        public PaymentTransaction PaymentSeller(Collection<PaymentTransaction> transaction, long sellerId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Update transaction
        /// </summary>
        /// <param name="id">transaction`s identificator</param>
        /// <returns>transactions after update</returns>
        public PaymentTransaction UpdateTransaction(long id)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Delete transaction
        /// </summary>
        /// <param name="id">transaction`s identificator</param>
        /// <returns>transaction</returns>
        public PaymentTransaction DeleteTransaction(long id)
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
        public PaymentTransaction WriteDownPercentageStore(Collection<PaymentTransaction> transaction)
        {
            throw new System.NotImplementedException();
        }
    }
}

namespace MediaShop.Common.Models.PaymentModel
{
    /// <summary>
    /// Class transaction of payment
    /// </summary>
    public abstract class PaymentTransaction : Entity
    {
        /// <summary>
        /// Gets or sets bank account number bayer
        /// </summary>
        public long BankAccountNumberSource { get; set; }

        /// <summary>
        /// Gets or sets bank account number bayer
        /// </summary>
        public long BankAccountNumberRezalt { get; set; }

        /// <summary>
        /// Gets or sets summTransaction
        /// </summary>
        public int SummTransaction { get; set; }
    }
}

namespace MediaShop.Common.Models.PaymentModel
{
    /// <summary>
    /// Class transaction of write down percentage store
    /// </summary>
    public class WriteDownPercentageStoreTransaction : PaymentTransaction
    {
        /// <summary>
        /// Gets or sets percentage store
        /// </summary>
        public float Percentage { get; set; }
    }
}

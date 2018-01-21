namespace MediaShop.Common.Models.PaymentModel
{
    /// <summary>
    /// Class transaction of payment reward seller
    /// </summary>
    public class RewardSellerTransaction : PaymentTransaction
    {
        /// <summary>
        /// Gets or sets SellerId
        /// </summary>
        public long SellerId { get; set; }

        /// <summary>
        /// Gets or sets ContentId
        /// </summary>
        public long ContentId { get; set; }
    }
}

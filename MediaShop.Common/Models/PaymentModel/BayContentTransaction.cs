namespace MediaShop.Common.Models.PaymentModel
{
    /// <summary>
    /// Class transaction of buy content
    /// </summary>
    public class BayContentTransaction : PaymentTransaction
    {
        /// <summary>
        /// Gets or sets BayerId
        /// </summary>
        public long BayerId { get; set; }

        /// <summary>
        /// Gets or sets ContentId
        /// </summary>
        public long ContentId { get; set; }
    }
}

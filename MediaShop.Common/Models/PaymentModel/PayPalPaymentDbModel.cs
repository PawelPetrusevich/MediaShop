namespace MediaShop.Common.Models.PaymentModel
{
    using MediaShop.Common.Enums.PaymentEnums;

    /// <summary>
    /// Model for save information about
    /// payment in repository
    /// </summary>
    public class PayPalPaymentDbModel : Entity
    {
        /// <summary>
        /// Gets or sets id paypal payment
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// Gets or sets state paypal payment
        /// </summary>
        public PaymentStates State { get; set; } = PaymentStates.None;
    }
}

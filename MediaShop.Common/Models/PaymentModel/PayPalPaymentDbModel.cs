namespace MediaShop.Common.Models.PaymentModel
{
    using MediaShop.Common.Enums.PaymentEnums;
    using MediaShop.Common.Models.User;

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

        /// <summary>
        /// Gets or sets property AccountDbModel
        /// </summary>
        public virtual AccountDbModel AccountDbModel { get; set; }
    }
}

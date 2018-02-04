namespace MediaShop.Common.Models.PaymentModel
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    ///  A resource representing a Payer that funds a payment.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Payer
    {
        /// <summary>
        /// Gets or sets payment method being used - PayPal Wallet payment, Bank Direct Debit or Direct
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "payment_method")]
        public string Payment_method { get; set; }

        /// <summary>
        /// Gets or sets status of payer's PayPal Account.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets information related to the Payer
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "payer_info")]
        public IList<PayerInfo> Payer_info { get; set; }
    }
}
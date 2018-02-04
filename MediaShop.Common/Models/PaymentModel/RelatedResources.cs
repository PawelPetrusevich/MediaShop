namespace MediaShop.Common.Models.PaymentModel
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Each one representing a financial transaction (Sale, Authorization, Capture,
    ///     Refund) related to the payment.
    ///     See PayPal Developer documentation for more information.
    /// </summary>
    public class RelatedResources
    {
        /// <summary>
        /// Gets or sets sale transaction
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "sale")]
        public Sale Sale { get; set; }
    }
}

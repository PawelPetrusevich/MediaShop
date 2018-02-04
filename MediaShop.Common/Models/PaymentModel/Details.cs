namespace MediaShop.Common.Models.PaymentModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// Additional details of the payment amount.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Details
    {
        /// <summary>
        /// Gets or sets amount of the subtotal of the items. **Required** if line items are specified.
        ///     10 characters max, with support for 2 decimal places.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "subtotal")]
        public string Subtotal { get; set; }

        /// <summary>
        /// Gets or sets amount charged for shipping. 10 characters max with support for 2 decimal places.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "shipping")]
        public string Shipping { get; set; }

        /// <summary>
        /// Gets or sets amount charged for tax. 10 characters max with support for 2 decimal places.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "handling_fee")]
        public string Tax { get; set; }

        /// <summary>
        /// Gets or sets amount being charged for the handling fee. Only supported when the `payment_method`
        ///     is set to `paypal`.
        /// </summary>
        public string Handling_fee { get; set; }

        /// <summary>
        /// Gets or sets amount being discounted for the shipping fee. Only supported when the `payment_method`
        ///     is set to `paypal`.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "shipping_discount")]
        public string Shipping_discount { get; set; }

        /// <summary>
        /// Gets or sets amount being charged for the insurance fee. Only supported when the `payment_method`
        ///     is set to `paypal`.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "insurance")]
        public string Insurance { get; set; }

        /// <summary>
        /// Gets or sets amount being charged as gift wrap fee.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "gift_wrap")]
        public string Gift_wrap { get; set; }

        /// <summary>
        /// Gets or sets fee charged by PayPal. In case of a refund, this is the fee amount refunded to
        ///     the original receipient of the payment.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "fee")]
        public string Fee { get; set; }
    }
}
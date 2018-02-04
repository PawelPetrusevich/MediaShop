namespace MediaShop.Common.Models.PaymentModel
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// payment amount with break-ups.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Amount
    {
        /// <summary>
        /// Gets or sets payPal does not support all currencies.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "currency")]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets total amount charged from the payer to the payee. In case of a refund, this is
        ///     the refunded amount to the original payer from the payee. 10 characters max with
        ///     support for 2 decimal places.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "total")]
        public string Total { get; set; }

        /// <summary>
        /// Gets or sets additional details of the payment amount.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "details")]
        public Details Details { get; set; }
    }
}
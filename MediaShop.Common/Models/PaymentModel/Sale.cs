namespace MediaShop.Common.Models.PaymentModel
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A sale transaction. This is the resource that is returned as a part related resources
    ///     in Payment
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Sale
    {
        /// <summary>
        /// Gets or sets identifier of the sale transaction.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets time of sale as defined in [RFC 3339 Section 5.6](http://tools.ietf.org/html/rfc3339#section-5.6)
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "create_time")]
        public string Create_time { get; set; }

        /// <summary>
        /// Gets or sets time the resource was last updated in UTC ISO8601 format.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "update_time")]
        public string Update_time { get; set; }

        /// <summary>
        /// Gets or sets amount being collected.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "amount")]
        public Amount Amount { get; set; }

        /// <summary>
        /// Gets or sets specifies payment mode of the transaction. Only supported when the `payment_method`
        ///     is set to `paypal`.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "payment_mode")]
        public string Payment_mode { get; set; }

        /// <summary>
        /// Gets or sets state of the sale transaction.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the level of seller protection in force for the transaction. Only supported when
        ///     the `payment_method` is set to `paypal`.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "protection_eligibility")]
        public string Protection_eligibility { get; set; }

        /// <summary>
        /// Gets or sets the kind of seller protection in force for the transaction. It is returned only
        ///     when protection_eligibility is ELIGIBLE or PARTIALLY_ELIGIBLE. Only supported
        ///     when the `payment_method` is set to `paypal`.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "protection_eligibility_type")]
        public string Protection_eligibility_type { get; set; }

        /// <summary>
        /// Gets or sets transaction fee applicable for this payment.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "transaction_fee")]
        public Currency Transaction_fee { get; set; }

        /// <summary>
        /// Gets or sets iD of the payment resource on which this transaction is based.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "parent_payment")]
        public string Parent_payment { get; set; }
    }
}

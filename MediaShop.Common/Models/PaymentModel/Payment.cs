namespace MediaShop.Common.Models.PaymentModel
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Class Payment
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Gets or sets identificator payment
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets payment creation time as defined in
        /// [RFC 3339 Section 5.6](http://tools.ietf.org/html/rfc3339#section-5.6).
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "create_time")]
        public string Create_time { get; set; }

        /// <summary>
        /// Gets or sets payment update time as defined in
        /// [RFC 3339 Section 5.6](http://tools.ietf.org/html/rfc3339#section-5.6).
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "update_time")]
        public string Update_time { get; set; }

        /// <summary>
        /// Gets or sets the state of the payment, authorization, or order transaction
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets payment intent.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "intent")]
        public string Intent { get; set; }

        /// <summary>
        /// Gets or sets transaction
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "payer")]
        public Payer Payer { get; set; }

        /// <summary>
        /// Gets or sets transaction
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "transactions")]
        public IList<Transaction> Transactions { get; set; }
    }
}

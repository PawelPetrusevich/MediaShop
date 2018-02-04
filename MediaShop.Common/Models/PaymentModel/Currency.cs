namespace MediaShop.Common.Models.PaymentModel
{
    using Newtonsoft.Json;

    /// <summary>
    ///  Base object for all financial value related fields (balance, payment due, etc.)
    ///    See PayPal Developer documentation for more information.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Currency
    {
        /// <summary>
        /// Gets or sets 3 letter currency code
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "currency")]
        public string CurrencyName { get; set; }

        /// <summary>
        /// Gets or sets amount upto 2 decimals represented as string
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "value")]
        public string Value { get; set; }
    }
}

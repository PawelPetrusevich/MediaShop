namespace MediaShop.Common.Models.PaymentModel
{
    using Newtonsoft.Json;

    /// <summary>
    ///  A resource representing a information about Payer.
    /// </summary>
    public class PayerInfo
    {
        /// <summary>
        /// Gets or sets two-letter registered country code of the payer to identify the buyer country.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "country_code")]
        public string Country_code { get; set; }

        /// <summary>
        /// Gets or sets payer’s tax ID type. Allowed values: `BR_CPF` or `BR_CNPJ`. Only supported when
        ///     the `payment_method` is set to `paypal`.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "tax_id_type")]
        public string Tax_id_type { get; set; }

        /// <summary>
        /// Gets or sets payer’s tax ID. Only supported when the `payment_method` is set to `paypal`.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "tax_id")]
        public string Tax_id { get; set; }

        /// <summary>
        /// Gets or sets payPal assigned encrypted Payer ID.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "payer_id")]
        public string Payer_id { get; set; }

        /// <summary>
        /// Gets or sets last name of the payer.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "last_name")]
        public string Last_name { get; set; }

        /// <summary>
        ///  Gets or sets middle name of the payer.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "middle_name")]
        public string Middle_name { get; set; }

        /// <summary>
        /// Gets or sets first name of the payer.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "first_name")]
        public string First_name { get; set; }

        /// <summary>
        /// Gets or sets account Number representing the Payer
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "buyer_account_number")]
        public string Buyer_account_number { get; set; }

        /// <summary>
        /// Gets or sets email address representing the payer. 127 characters max.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "email")]
        public string Email { get; set; }
    }
}

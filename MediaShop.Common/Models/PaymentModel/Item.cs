namespace MediaShop.Common.Models.PaymentModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// Item details.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Gets or sets stock keeping unit corresponding (SKU) to item.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "sku")]
        public string Sku { get; set; }

        /// <summary>
        /// Gets or sets item name. 127 characters max.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets description of the item. Only supported when the `payment_method` is set to `paypal`.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets number of a particular item. 10 characters max.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "quantity")]
        public string Quantity { get; set; }

        /// <summary>
        /// Gets or sets item cost. 10 characters max.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "price")]
        public string Price { get; set; }

        /// <summary>
        /// Gets or sets 3-letter [currency code]
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "currency")]
        public string Currency { get; set; }

        /// <summary>
        ///  Gets or sets tax of the item. Only supported when the `payment_method` is set to `paypal`.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "tax")]
        public string Tax { get; set; }
    }
}
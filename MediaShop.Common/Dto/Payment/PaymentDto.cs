namespace MediaShop.Common.Dto.Payment
{
    using MediaShop.Common.Models.PaymentModel;
    using Newtonsoft.Json;

    /// <summary>
    /// Dto for return user information about successful payment
    /// </summary>
    public class PaymentDto
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
        /// Gets or sets list of items.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "item_list")]
        public ItemList Item_list { get; set; }
    }
}

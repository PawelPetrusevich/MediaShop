namespace MediaShop.Common.Models.PaymentModel
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Details of a list of purchasable items and shipping information included with
    ///     a payment transaction.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ItemList
    {
        /// <summary>
        /// Gets or sets list of items.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "items")]
        public List<Item> Items { get; set; }
    }
}
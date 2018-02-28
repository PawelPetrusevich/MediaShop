namespace MediaShop.Common.Dto.Payment
{
    using System.Collections.Generic;
    using MediaShop.Common.Models.PaymentModel;
    using Newtonsoft.Json;

    /// <summary>
    /// Class information about product that buy buyer
    /// </summary>
    public class ItemDto
    {
        /// <summary>
        /// Gets or sets stock keeping unit corresponding (SKU) to item.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Sku")]
        public string Sku { get; set; }

        /// <summary>
        /// Gets or sets item name. 127 characters max.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets item cost. 10 characters max.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Price")]
        public string Price { get; set; }
    }
}

namespace MediaShop.Common.Models.PaymentModel
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class transaction of payment
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Gets or sets amount being collected.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "amount")]
        public Amount Amount { get; set; }

        /// <summary>
        /// Gets or sets description of what is being paid for.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets сollection of paid content
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "item_list")]
        public ItemList Item_list { get; set; }

        /// <summary>
        /// Gets or sets list of financial transactions (Sale, Authorization, Capture, Refund) related
        /// to the payment.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "related_resources")]
        public IList<RelatedResources> Related_resources { get; set; }
    }
}

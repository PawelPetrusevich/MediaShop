namespace MediaShop.Common.Models.CartModels
{
    /// <summary>
    /// Class Product
    /// </summary>
    public class Product : Entity
    {
        /// <summary>
        /// Gets or sets the content name in the shopping cart
        /// </summary>
        public string ContentName { get; set; }

        /// <summary>
        /// Gets or sets cart creator name
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// Gets or sets description media content
        /// </summary>
        public string DescriptionItem { get; set; }

        /// <summary>
        /// Gets or sets price media content
        /// </summary>
        public decimal PriceItem { get; set; }
    }
}

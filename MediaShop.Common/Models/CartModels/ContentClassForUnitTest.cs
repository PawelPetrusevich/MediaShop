namespace MediaShop.Common.Models
{
    using System;

    /// <summary>
    /// Class instead of class content for testing CartService
    /// </summary>
    public class ContentClassForUnitTest : Entity
    {
        /// <summary>
        /// Gets or sets the content name in the shopping cart
        /// </summary>
        public string ContentName { get; set; }

        /// <summary>
        /// Gets or sets cart creator name
        /// </summary>
        public int CreatorName { get; set; }

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

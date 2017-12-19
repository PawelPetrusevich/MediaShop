namespace MediaShop.Common.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Сlass describes model ShoppingCart
    /// </summary>
    public class ShoppingCart
    {
        /// <summary>
        /// Gets or sets Collection items
        /// </summary>
        public IEnumerable<ContentCart> ContentCartCollection { get; set; }

        /// <summary>
        /// Gets or sets Property to determine price all items in collection
        /// </summary>
        public float PriceAllItemsCollection { get; set; }

        /// <summary>
        /// Gets or sets Property to determine the amount of content in a collection
        /// </summary>
        public uint CountItemsInCollection { get; set; }
    }
}

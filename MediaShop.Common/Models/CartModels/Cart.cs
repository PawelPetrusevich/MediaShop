namespace MediaShop.Common.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Сlass describes model Cart
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Gets or sets Collection items
        /// </summary>
        public IEnumerable<ContentCartDto> ContentCartDtoCollection { get; set; } = new Collection<ContentCartDto>();

        /// <summary>
        /// Gets or sets Property to determine price all items in collection
        /// </summary>
        public decimal PriceAllItemsCollection { get; set; }

        /// <summary>
        /// Gets or sets Property to determine the amount of content in a collection
        /// </summary>
        public uint CountItemsInCollection { get; set; }
    }
}

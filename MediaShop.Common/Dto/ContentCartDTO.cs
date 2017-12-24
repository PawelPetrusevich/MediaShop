namespace MediaShop.Common.Models
{
    using System;

    /// <summary>
    /// Сlass describes model ContentCartDTO
    /// </summary>
    public class ContentCartDto : Entity
    {
        /// <summary>
        /// Gets or sets the content name in the shopping cart
        /// </summary>
        public string ContentName { get; set; }

        /// <summary>
        /// Gets or sets description media content
        /// </summary>
        public string DescriptionItem { get; set; }

        /// <summary>
        /// Gets or sets price media content
        /// </summary>
        public decimal PriceItem { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets
        /// the property to determine whether the content is bought
        /// </summary>
        public bool IsBought { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets
        /// the property to determine whether the content is delete
        /// </summary>
        public bool IsDelete { get; set; } = false;
    }
}

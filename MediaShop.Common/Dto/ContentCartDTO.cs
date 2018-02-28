namespace MediaShop.Common.Models
{
    using MediaShop.Common.Enums;

    /// <summary>
    /// Сlass describes model ContentCartDTO
    /// </summary>
    public class ContentCartDto
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the content id in the shopping cart
        /// </summary>
        public long ContentId { get; set; }

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
        /// Gets or sets a state of contents in cart
        /// </summary>
        public CartEnums.StateCartContent StateContent { get; set; } =
            CartEnums.StateCartContent.InCart;

        /// <summary>
        /// Gets or sets the creator identifier.
        /// </summary>
        /// <value>The creator identifier.</value>
        public long CreatorId { get; set; }
    }
}

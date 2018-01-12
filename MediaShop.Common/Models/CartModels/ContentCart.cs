namespace MediaShop.Common.Models
{
    using MediaShop.Common.Enums;

    /// <summary>
    /// Сlass describes content in the cart
    /// </summary>
    public class ContentCart : Entity
    {
        /// <summary>
        /// Gets or sets the content name in the shopping cart
        /// </summary>
        public string ContentName { get; set; }

        /// <summary>
        /// Gets or sets the name of the theme of the selected
        /// content group at the user's choice
        /// </summary>
        public string CategoryName { get; set; }

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

        /// <summary>
        /// Gets or sets a state of contents in cart
        /// </summary>
        public CartEnums.StateCartContent StateContent { get; set; } =
            CartEnums.StateCartContent.InCart;
    }
}

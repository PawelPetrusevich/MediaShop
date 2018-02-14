namespace MediaShop.Common.Models
{
    using MediaShop.Common.Enums;
    using MediaShop.Common.Models.Content;

    /// <summary>
    /// Сlass describes content in the cart
    /// </summary>
    public class ContentCart : Entity
    {
        /// <summary>
        /// Gets or sets Product in the shopping cart
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Gets or sets ProductId
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Gets or sets a state of contents in cart
        /// </summary>
        public CartEnums.StateCartContent StateContent { get; set; } =
            CartEnums.StateCartContent.InCart;
    }
}

namespace MediaShop.Common.Models
{
    using MediaShop.Common.Enums;

    /// <summary>
    /// Сlass describes content in the cart
    /// </summary>
    public class ContentCart : Entity
    {
        /// <summary>
        /// Gets or sets Product in the shopping cart
        /// </summary>
        public virtual CartModels.Product Product { get; set; }

        /// <summary>
        /// Gets or sets productId
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Gets or sets a state of contents in cart
        /// </summary>
        public CartEnums.StateCartContent StateContent { get; set; } =
            CartEnums.StateCartContent.InCart;
    }
}

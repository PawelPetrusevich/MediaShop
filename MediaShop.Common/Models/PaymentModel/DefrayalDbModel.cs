namespace MediaShop.Common.Models.PaymentModel
{
    using MediaShop.Common.Models.User;

    /// <summary>
    /// Class content that defrayal
    /// </summary>
    public class DefrayalDbModel : Entity
    {
        /// <summary>
        /// Gets or sets the content id in the shopping cart
        /// </summary>
        public long ContentId { get; set; }

        /// <summary>
        /// Gets or sets property AccountDbModel
        /// </summary>
        public virtual AccountDbModel AccountDbModel { get; set; }
    }
}

using MediaShop.Common.Enums;

namespace MediaShop.Common.Dto.Product
{
    /// <summary>
    /// Модель для получения картинки в оригинальном разме
    /// </summary>
    public class CompressedProductDTO
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the ProductName.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets ProductPrice
        /// </summary>
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// картинка, видео, музыка в формате Base64
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or Sets Product Type
        /// </summary>
        public ProductType ProductType { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace MediaShop.Common.Models.Content
{
    /// <summary>
    /// Уменьшенная копия загруженного контента
    /// </summary>
    public class CompressedProduct
    {
        public long CompressedProductId { get; set; }

        public byte[] Content { get; set; }

        public Product Product { get; set; }
    }
}

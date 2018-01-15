using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace MediaShop.Common.Models.Content
{
    /// <summary>
    /// Уменьшенная копия загруженного контента
    /// </summary>
    public class CompressedProduct : Entity
    {
        public byte[] Content { get; set; }

        public Product Product { get; set; }

        public long ProductId { get; set; }
    }
}

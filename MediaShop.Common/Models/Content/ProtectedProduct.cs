using System.ComponentModel.DataAnnotations.Schema;

namespace MediaShop.Common.Models.Content
{
    /// <summary>
    /// Защищенная копия контента
    /// </summary>
    public class ProtectedProduct : Entity
    {
        public byte[] Content { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public long ProductId { get; set; }
    }
}

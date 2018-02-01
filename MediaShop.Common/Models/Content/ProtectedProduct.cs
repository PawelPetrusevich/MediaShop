using System.ComponentModel.DataAnnotations.Schema;

namespace MediaShop.Common.Models.Content
{
    /// <summary>
    /// Защищенная копия контента
    /// </summary>
    public class ProtectedProduct
    {
        public byte[] Content { get; set; }

        public Product Product { get; set; }

        public long ProtectedProductId { get; set; }
    }
}

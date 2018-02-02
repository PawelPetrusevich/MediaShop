using System.ComponentModel.DataAnnotations.Schema;

namespace MediaShop.Common.Models.Content
{
    /// <summary>
    /// Загруженный файл контента в оригинальном размере
    /// </summary>
    public class OriginalProduct
    {
        public byte[] Content { get; set; }

        public Product Product { get; set; }

        public long OriginalProductId { get; set; }
    }
}

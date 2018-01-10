namespace MediaShop.Common.Models.Content
{
    /// <summary>
    /// Загруженный файл контента в оригинальном размере
    /// </summary>
    public class OriginalProduct : Entity
    {
        public byte[] File { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }
    }
}

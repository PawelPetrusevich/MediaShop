namespace MediaShop.Common.Models.Content
{
    /// <summary>
    /// Уменьшенная копия загруженного контента
    /// </summary>
    public class CompressedProduct : Entity
    {
        public byte[] File { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }
    }
}

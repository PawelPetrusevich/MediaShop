namespace MediaShop.Common.Models.Content
{
    /// <summary>
    /// Защищенная копия контента
    /// </summary>
    public class ProtectedProduct : Entity
    {
        public byte[] File { get; set; }

        public Product Product { get; set; }

        public long ProductId { get; set; }
    }
}

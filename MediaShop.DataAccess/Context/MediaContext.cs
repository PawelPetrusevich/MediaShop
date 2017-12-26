using System.Data.Entity;
using MediaShop.Common.Models.Content;

namespace MediaShop.DataAccess.Context
{
    public class MediaContext : DbContext
    {
        public MediaContext() : base("MediaShopConnection")
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ProductConfiguration());
        }
    }
}
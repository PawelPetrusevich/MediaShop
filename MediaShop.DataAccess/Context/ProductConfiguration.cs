using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Models.Content;

namespace MediaShop.DataAccess.Context
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            this.HasRequired<ProductType>(p => p.ProductType)
                .WithMany(p => p.Products)
                .HasForeignKey<int>(s => s.ProductTypeId);
            this.HasKey(p => p.Id);
        }
    }
}

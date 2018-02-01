using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Models.Content;

namespace MediaShop.DataAccess.Configurations
{
    public class CompressedProductConfiguration : EntityTypeConfiguration<CompressedProduct>
    {
        public CompressedProductConfiguration()
        {
            this.HasRequired(c => c.Product).WithRequiredPrincipal(c => c.CompressedProduct);
        }
    }
}

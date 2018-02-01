using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaShop.Common.Models.Content;

namespace MediaShop.DataAccess.Configurations
{
    public class ProtectedProductConfiguration : EntityTypeConfiguration<ProtectedProduct>
    {
        public ProtectedProductConfiguration()
        {
            this.HasRequired(c => c.Product).WithRequiredPrincipal(c => c.ProtectedProduct);
        }
    }
}

using MediaShop.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.DataAccess.Configurations
{
    public class ContentCartConfiguration : EntityTypeConfiguration<ContentCartDto>
    {
        public ContentCartConfiguration()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id)
                .IsRequired();
            this.Property(x => x.ContentName)
                .IsRequired()
                .IsVariableLength();
            this.Property(x => x.CategoryName)
                .IsRequired()
                .IsVariableLength();
            this.Property(x => x.DescriptionItem)
                .IsOptional()
                .IsVariableLength();
            this.Property(x => x.PriceItem)
                .IsRequired();
            this.Property(x => x.IsBought)
                .IsRequired();
            this.Property(x => x.IsDelete)
               .IsRequired();
            this.Property(x => x.IsChecked)
                .IsRequired();
            this.Property(x => x.CreatorId)
                .IsRequired();
            this.Property(x => x.CreatedDate)
                .IsRequired();
            this.Property(x => x.ModifierId)
                .IsOptional();
            this.Property(x => x.ModifiedDate)
                .IsOptional();
        }
    }
}

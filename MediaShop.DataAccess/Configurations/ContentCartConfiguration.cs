namespace MediaShop.DataAccess.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using MediaShop.Common.Models;

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
            this.Property(x => x.StateContent)
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

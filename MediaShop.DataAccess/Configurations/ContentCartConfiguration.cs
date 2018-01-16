namespace MediaShop.DataAccess.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using MediaShop.Common.Models;

    public class ContentCartConfiguration : EntityTypeConfiguration<ContentCart>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentCartConfiguration" /> class.
        /// </summary>
        public ContentCartConfiguration()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(x => x.ContentName)
                .IsRequired()
                .IsVariableLength()
                .IsUnicode(true);
            this.Property(x => x.CategoryName)
                .IsRequired()
                .IsVariableLength()
                .IsUnicode(true);
            this.Property(x => x.DescriptionItem)
                .IsOptional()
                .IsVariableLength()
                .IsUnicode(true);
            this.Property(x => x.PriceItem)
                .IsRequired();
            this.Property(x => x.StateContent)
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

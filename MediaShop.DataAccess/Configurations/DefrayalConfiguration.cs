namespace MediaShop.DataAccess.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;
    using MediaShop.Common.Models.PaymentModel;

    /// <summary>
    /// Class DefrayalConfiguration
    /// </summary>
    public class DefrayalConfiguration : EntityTypeConfiguration<DefrayalDbModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefrayalConfiguration" /> class.
        /// </summary>
        public DefrayalConfiguration()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id)
                .IsRequired();
            this.Property(x => x.CreatorId)
                .IsRequired();
            this.Property(x => x.CreatedDate)
                .IsRequired();
            this.Property(x => x.ModifierId)
                .IsOptional();
            this.Property(x => x.ModifiedDate)
                .IsOptional();
            this.HasRequired(x => x.AccountDbModel)
                .WithMany()
                .HasForeignKey(x => x.CreatorId);
        }
    }
}

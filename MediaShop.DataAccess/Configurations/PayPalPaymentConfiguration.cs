namespace MediaShop.DataAccess.Configurations
{
    using System.Data.Entity.ModelConfiguration;
    using MediaShop.Common.Models.PaymentModel;

    /// <summary>
    /// Configuration PaymentDbModel in database
    /// </summary>
    public class PayPalPaymentConfiguration : EntityTypeConfiguration<PayPalPaymentDbModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayPalPaymentConfiguration" /> class.
        /// </summary>
        public PayPalPaymentConfiguration()
        {
            this.HasKey(x => x.Id);
            this.Property(x => x.Id)
                .IsRequired();
            this.Property(x => x.State)
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

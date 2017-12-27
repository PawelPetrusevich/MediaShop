using System.Data.Entity.ModelConfiguration;
using MediaShop.Common.Models.User;

namespace MediaShop.DataAccess.Configurations
{
    /// <summary>
    /// Configuration connect accountProfile with  table account
    /// </summary>
    public class ProfileConfiguration : EntityTypeConfiguration<AccountProfile>
    {
        /// <summary>
        /// Initializes a new instance of the ProfileConfiguration class
        /// </summary>
        public ProfileConfiguration()
        {
            this.HasRequired(c => c.AccountOf).WithRequiredDependent(p => p.Profile);

            this.Property(p => p.Email).IsRequired();
            this.Property(p => p.FirstName).HasMaxLength(30);
            this.Property(p => p.LastName).HasMaxLength(30);
            this.Property(p => p.Email).HasMaxLength(30);
            this.Property(p => p.Phone).HasMaxLength(30);
        }
    }
}
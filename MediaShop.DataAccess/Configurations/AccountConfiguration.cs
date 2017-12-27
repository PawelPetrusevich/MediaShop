using System.Data.Entity.ModelConfiguration;
using MediaShop.Common.Models.User;

namespace MediaShop.DataAccess.Configurations
{
    /// <summary>
    /// Configuration connect account with other tables
    /// </summary>
    public class AccountConfiguration : EntityTypeConfiguration<Account>
    {
        /// <summary>
        /// Initializes a new instance of the AccountConfiguration class
        /// </summary>
        public AccountConfiguration()
        {
            this.HasRequired(c => c.Profile).WithRequiredPrincipal(p => p.AccountOf);
            this.HasRequired(c => c.Settings).WithRequiredPrincipal(p => p.AccountOf);
            this.HasKey(p => p.Id);

            this.Property(p => p.Login).IsRequired();
            this.Property(p => p.Password).IsRequired();
        }
    }
}
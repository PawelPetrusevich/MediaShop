using System.Data.Entity.ModelConfiguration;
using MediaShop.Common.Models.User;

namespace MediaShop.DataAccess.Configurations
{
    /// <summary>
    /// Configuration connect accountSeettings with  table account
    /// </summary>
    public class AccountSettingsConfiguration : EntityTypeConfiguration<AccountSettings>
    {
        /// <summary>
        /// Initializes a new instance of the AccountSettingsConfiguration class
        /// </summary>
        public AccountSettingsConfiguration()
        {
            this.HasRequired(c => c.AccountOf).WithRequiredDependent(p => p.Settings);
        }
    }
}
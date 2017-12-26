using System.Data.Entity.ModelConfiguration;
using MediaShop.Common.Models.User;

namespace MediaShop.DataAccess.Configurations
{
    public class AccountSettingsConfiguration : EntityTypeConfiguration<AccountSettings>
    {
        public AccountSettingsConfiguration()
        {
            this.HasRequired(c => c.AccountOf).WithRequiredDependent(p => p.Settings);
        }
    }
}
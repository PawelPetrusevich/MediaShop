using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using MediaShop.Common.Models;
using MediaShop.Common.Models.User;
using MediaShop.DataAccess.Configurations;

namespace MediaShop.DataAccess.Context
{
    public class UserContext : DbContext
    {
        public UserContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<UserContext>());
        }

        public IDbSet<Account> Accounts { get; set; }

        public IDbSet<AccountProfile> Profiles { get; set; }

        public IDbSet<AccountSettings> Settings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AccountConfiguration());
            modelBuilder.Configurations.Add(new ProfileConfiguration());
            modelBuilder.Configurations.Add(new AccountSettingsConfiguration());
        }
    }
}
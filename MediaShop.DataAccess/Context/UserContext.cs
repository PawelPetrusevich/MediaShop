using System.Data.Entity;
using MediaShop.Common.Models.User;
using MediaShop.DataAccess.Configurations;

namespace MediaShop.DataAccess.Context
{
    /// <summary>
    /// install UserContext
    /// </summary>
    public class UserContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the UserContext class
        /// </summary>
        public UserContext()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<UserContext>());
        }

        /// <summary>
        /// property Accounts for interacting with tables db
        /// </summary>
        public IDbSet<Account> Accounts { get; set; }

        /// <summary>
        /// property Profiles for interacting with tables db
        /// </summary>
        public IDbSet<AccountProfile> Profiles { get; set; }

        /// <summary>
        /// property Settings for interacting with tables db
        /// </summary>
        public IDbSet<AccountSettings> Settings { get; set; }

        /// <summary>
        /// Configuration tables
        /// </summary>
        /// <param name="modelBuilder">modelBuilder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AccountConfiguration());
            modelBuilder.Configurations.Add(new ProfileConfiguration());
            modelBuilder.Configurations.Add(new AccountSettingsConfiguration());
        }
    }
}
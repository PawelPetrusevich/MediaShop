using System.Data.Entity;
using MediaShop.Common.Models.Notification;
using MediaShop.DataAccess.Configurations;

namespace MediaShop.DataAccess.Context
{
    using MediaShop.DataAccess.Configurations;
    using System.Data.Entity;
    using MediaShop.Common.Models;

    using MediaShop.Common.Models.User;
    using MediaShop.DataAccess.Configurations;

    /// <summary>
    /// Class MediaContext.
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class MediaContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaContext"/> class.
        /// </summary>
        public MediaContext()
            : base("MediaShopConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<MediaContext>());
        }

        public IDbSet<Notification> Notifications { get; set; }

        public IDbSet<ContentCart> ContentCart { get; set; }

        /// <summary>
        /// Gets or sets the accounts.
        /// </summary>
        /// <value>The accounts.</value>
        public IDbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Gets or sets the profiles.
        /// </summary>
        /// <value>The profiles.</value>
        public IDbSet<Profile> Profiles { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public IDbSet<Settings> Settings { get; set; }

        /// <summary>
        /// Method configuration tables
        /// </summary>
        /// <param name="modelBuilder">modelBuilder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ContentCartConfiguration());

            modelBuilder.Configurations.Add(new AccountConfiguration());
            modelBuilder.Configurations.Add(new ProfileConfiguration());
            modelBuilder.Configurations.Add(new SettingsConfiguration());
            modelBuilder.Configurations.Add(new NotificationConfiguration());
        }
    }
}
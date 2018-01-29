﻿using MediaShop.Common.Models.Notification;
using MediaShop.Common.Models.Content;
using MediaShop.DataAccess.Migrations;

namespace MediaShop.DataAccess.Context
{
    using MediaShop.DataAccess.Configurations;
    using System.Data.Entity;
    using MediaShop.Common.Models;

    using MediaShop.Common.Models.User;

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
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MediaContext, Configuration>());
        }

        public IDbSet<Notification> Notifications { get; set; }

        public IDbSet<NotificationSubscribedUser> NotificationSubscribedUsers { get; set; }

        public IDbSet<ContentCart> ContentCart { get; set; }

        /// <summary>
        /// Gets or sets the accounts.
        /// </summary>
        /// <value>The accounts.</value>
        public IDbSet<AccountDbModel> Accounts { get; set; }

        /// <summary>
        /// Gets or sets the profiles.
        /// </summary>
        /// <value>The profiles.</value>
        public IDbSet<ProfileDbModel> Profiles { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public IDbSet<SettingsDbModel> Settings { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<OriginalProduct> OriginalProducts { get; set; }

        public IDbSet<CompressedProduct> CompressedProducts { get; set; }

        public IDbSet<ProtectedProduct> ProtectedProducts { get; set; }

        /// <summary>
        /// Method configuration tables
        /// </summary>
        /// <param name="modelBuilder">modelBuilder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ContentCartConfiguration());
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            modelBuilder.Configurations.Add(new AccountConfiguration());
            modelBuilder.Configurations.Add(new ProfileConfiguration());
            modelBuilder.Configurations.Add(new SettingsConfiguration());
            modelBuilder.Configurations.Add(new PermissionConfiguration());
            modelBuilder.Configurations.Add(new StatisticConfiguration());
            modelBuilder.Configurations.Add(new NotificationConfiguration());
            modelBuilder.Configurations.Add(new SubscribeNotificationConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ProtectedProductConfiguration());
            modelBuilder.Configurations.Add(new CompressedProductConfiguration());
            modelBuilder.Configurations.Add(new OriginalProductConfiguration());
        }
    }
}
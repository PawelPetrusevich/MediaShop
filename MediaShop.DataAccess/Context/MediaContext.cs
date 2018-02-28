using System.Data.Entity;
using MediaShop.Common.Models.Notification;
using MediaShop.DataAccess.Configurations;

using MediaShop.DataAccess.Migrations;

namespace MediaShop.DataAccess.Context
{
    using MediaShop.DataAccess.Configurations;
    using System.Data.Entity;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.Notification;
    using MediaShop.Common.Models.User;
    using MediaShop.Common.Models.Content;
    using MediaShop.Common.Models.PaymentModel;

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

        /// <summary>
        /// Gets or sets the ContentCarts.
        /// </summary>
        /// <value>The Product.</value>
        public IDbSet<ContentCart> ContentCarts { get; set; }

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

        /// <summary>
        /// Gets or sets the Products.
        /// </summary>
        /// <value>The Product.</value>
        public IDbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the OriginalProduct.
        /// </summary>
        /// <value>The OriginalProduct.</value>
        public IDbSet<OriginalProduct> OriginalProducts { get; set; }

        /// <summary>
        /// Gets or sets the CompressedProduct.
        /// </summary>
        /// <value>The CompressedProduct.</value>
        public IDbSet<CompressedProduct> CompressedProducts { get; set; }

        /// <summary>
        /// Gets or sets the ProtectedProduct.
        /// </summary>
        /// <value>The ProtectedProduct.</value>
        public IDbSet<ProtectedProduct> ProtectedProducts { get; set; }

        /// <summary>
        /// Gets or sets the PaymentDbModel
        /// </summary>
        public IDbSet<PayPalPaymentDbModel> PaymentDbModels { get; set; }

        /// <summary>
        /// Gets or sets the DefrayalDbModel
        /// </summary>
        public IDbSet<DefrayalDbModel> DefrayalDbModels { get; set; }

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
            modelBuilder.Configurations.Add(new StatisticConfiguration());
            modelBuilder.Configurations.Add(new NotificationConfiguration());
            modelBuilder.Configurations.Add(new SubscribeNotificationConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ProtectedProductConfiguration());
            modelBuilder.Configurations.Add(new CompressedProductConfiguration());
            modelBuilder.Configurations.Add(new OriginalProductConfiguration());
            modelBuilder.Configurations.Add(new PayPalPaymentConfiguration());
            modelBuilder.Configurations.Add(new DefrayalConfiguration());
        }
    }
}

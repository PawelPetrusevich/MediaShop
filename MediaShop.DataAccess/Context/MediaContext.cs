using System.Data.Entity;
using MediaShop.Common.Models.Notification;
using MediaShop.DataAccess.Configurations;

namespace MediaShop.DataAccess.Context
{
    using System.Data.Entity;

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
        }

        /// <summary>
        /// Method configuration tables
        /// </summary>
        /// <param name="modelBuilder">modelBuilder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // your config for Entity here placed in configuartion folder
            modelBuilder.Configurations.Add(new NotificationConfiguration());
        }
    }
}
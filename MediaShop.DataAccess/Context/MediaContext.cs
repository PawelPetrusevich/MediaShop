using System.Data.Entity;
using MediaShop.Common.Models.Notification;
using MediaShop.DataAccess.Configurations;

namespace MediaShop.DataAccess.Context
{
    public class MediaContext : DbContext
    {
        public MediaContext() : base("MediaShopConnection")
        {
        }

        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // your config for Entity here placed in configuartion folder
            modelBuilder.Configurations.Add(new NotificationConfiguration());
        }
    }
}
using System.Data.Entity;

namespace MediaShop.DataAccess.Context
{
    public class MediaContext : DbContext
    {
        public MediaContext() : base("MediaShopConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // your config for Entity here placed in configuartion folder
        }
    }
}
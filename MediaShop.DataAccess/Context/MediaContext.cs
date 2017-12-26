namespace MediaShop.DataAccess.Context
{
    using System.Data.Entity;

    /// <summary>
    /// Context for work with database
    /// </summary>
    public class MediaContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaContext"/> class.
        /// </summary>
        public MediaContext()
            : base("MediaShopConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // your config for Entity here placed in configuartion folder
        }
    }
}
namespace MediaShop.DataAccess.Context
{
    using System.Data.Entity;

    /// <summary>
    /// install media context
    /// </summary>
    public class MediaContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the MediaContext class
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
        }
    }
}
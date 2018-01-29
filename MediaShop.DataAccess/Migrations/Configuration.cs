namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Linq.Expressions;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.CartModels;
    using MediaShop.DataAccess.Context;

    internal sealed class Configuration : DbMigrationsConfiguration<MediaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MediaContext context)
        {
            // initializing database at first time
        }
    }
}

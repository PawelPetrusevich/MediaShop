namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.CartModels;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MediaShop.DataAccess.Context.MediaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MediaShop.DataAccess.Context.MediaContext context)
        {
            //context.ContentCarts.Add(
            //    new ContentCart
            //    {
            //        Id = 8,
            //        ContentName = "Song1",
            //        CategoryName = "Music",
            //        CreatorName = "Dick Trump",
            //        DescriptionItem = "Date create 03.05.2016ã",
            //        PriceItem = 50,
            //        StateContent = 0,
            //        CreatedDate = new DateTime(2017, 12, 05),
            //        CreatorId = 1,
            //        ModifiedDate = new DateTime(2018, 01, 03),
            //        ModifierId = 2
            //    });

            //context.ContentCarts.Add(
            //    new ContentCart
            //    {
            //        Id = 9,
            //        ContentName = "Song2",
            //        CategoryName = "Music",
            //        CreatorName = "Dick Trump",
            //        DescriptionItem = "Date create 03.08.2016ã",
            //        PriceItem = 50,
            //        StateContent = 0,
            //        CreatedDate = new DateTime(2017, 12, 06),
            //        CreatorId = 1,
            //        ModifiedDate = new DateTime(2018, 01, 05),
            //        ModifierId = 2
            //    });
            //context.ContentCarts.Add(
            //    new ContentCart
            //    {
            //        Id = 10,
            //        ContentName = "Song3",
            //        CategoryName = "Music",
            //        CreatorName = "Dick Trump",
            //        DescriptionItem = "Date create 03.07.2016ã",
            //        PriceItem = 50,
            //        StateContent = 0,
            //        CreatedDate = new DateTime(2017, 12, 07),
            //        CreatorId = 1,
            //        ModifiedDate = new DateTime(2018, 01, 06),
            //        ModifierId = 2
            //    });
            //context.Products.Add(
            //    new Product
            //    {
            //        Id = 5,
            //        ContentName = "Song4",
            //        CreatorName = "Dick Trump",
            //        DescriptionItem = "Date create 15.07.2016ã",
            //        PriceItem = 50,
            //        CreatedDate = new DateTime(2017, 12, 08),
            //        CreatorId = 1,
            //        ModifiedDate = new DateTime(2018, 01, 12),
            //        ModifierId = 2
            //    });
            //context.Products.Add(
            //    new Product
            //    {
            //        Id = 6,
            //        ContentName = "Song5",
            //        CreatorName = "Dick Trump",
            //        DescriptionItem = "Date create 18.07.2016ã",
            //        PriceItem = 50,
            //        CreatedDate = new DateTime(2017, 12, 10),
            //        CreatorId = 1,
            //        ModifiedDate = new DateTime(2018, 01, 15),
            //        ModifierId = 2
            //    });

            //context.SaveChanges();
        }
    }
}

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
            //var rez = context.ContentCarts.FirstOrDefault(x => x.ContentId == 8 || x.ContentId == 9 || x.ContentId == 10);
            //if (rez == null)
            //{
            //    context.ContentCarts.Add(
            //        new ContentCart
            //        {
            //            ContentId = 8,
            //            ContentName = "Song1",
            //            CreatorName = "Dick Trump",
            //            DescriptionItem = "Date create 03.05.2016",
            //            PriceItem = 50,
            //            StateContent = 0,
            //            CreatedDate = new DateTime(2017, 12, 05),
            //            CreatorId = 1,
            //            ModifiedDate = new DateTime(2018, 01, 03),
            //            ModifierId = 2
            //        });

            //    context.ContentCarts.Add(
            //        new ContentCart
            //        {
            //            ContentId = 9,
            //            ContentName = "Song2",
            //            CreatorName = "Dick Trump",
            //            DescriptionItem = "Date create 03.08.2016",
            //            PriceItem = 50,
            //            StateContent = 0,
            //            CreatedDate = new DateTime(2017, 12, 06),
            //            CreatorId = 1,
            //            ModifiedDate = new DateTime(2018, 01, 05),
            //            ModifierId = 2
            //        });
            //    context.ContentCarts.Add(
            //        new ContentCart
            //        {
            //            ContentId = 10,
            //            ContentName = "Song3",
            //            CreatorName = "Dick Trump",
            //            DescriptionItem = "Date create 03.07.2016",
            //            PriceItem = 50,
            //            StateContent = 0,
            //            CreatedDate = new DateTime(2017, 12, 07),
            //            CreatorId = 1,
            //            ModifiedDate = new DateTime(2018, 01, 06),
            //            ModifierId = 2
            //        });
            //    context.SaveChanges();
            //}

            //if (context.Products.FirstOrDefault(x => x.Id == 5 || x.Id == 6) == null)
            //{
            //    context.Products.Add(
            //      new Product
            //      {
            //          Id = 5,
            //          ContentName = "Song4",
            //          CreatorName = "Dick Trump",
            //          DescriptionItem = "Date create 15.07.2016",
            //          PriceItem = 50,
            //          CreatedDate = new DateTime(2017, 12, 08),
            //          CreatorId = 1,
            //          ModifiedDate = new DateTime(2018, 01, 12),
            //          ModifierId = 2
            //      });
            //    context.Products.Add(
            //        new Product
            //        {
            //            Id = 6,
            //            ContentName = "Song5",
            //            CreatorName = "Dick Trump",
            //            DescriptionItem = "Date create 18.07.2016",
            //            PriceItem = 50,
            //            CreatedDate = new DateTime(2017, 12, 10),
            //            CreatorId = 1,
            //            ModifiedDate = new DateTime(2018, 01, 15),
            //            ModifierId = 2
            //        });
            //    context.SaveChanges();
            //}
        }
    }
}

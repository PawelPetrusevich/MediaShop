namespace MediaShop.DataAccess.Context
{
    using System;
    using System.Data.Entity;
    using MediaShop.Common.Models;
    using MediaShop.Common.Models.CartModels;

    /// <summary>
    /// Class CartInitializer
    /// </summary>
    public class MediaInitializer : CreateDatabaseIfNotExists<MediaContext>
    {
        /// <inheritdoc/>
        protected override void Seed(MediaContext context)
        {
            context.ContentCarts.Add(
                new ContentCart
                {
                    Id = 8,
                    ContentName = "Rock Song",
                    CategoryName = "Music",
                    CreatorName = "Sasha Dock",
                    DescriptionItem = "Rock music",
                    PriceItem = 50,
                    StateContent = 0,
                    CreatedDate = new DateTime(2018, 01, 10),
                    CreatorId = 50,
                });

            context.ContentCarts.Add(
                new ContentCart
                {
                    Id = 9,
                    ContentName = "Melody Song",
                    CategoryName = "Music",
                    CreatorName = "Sasha Dock",
                    DescriptionItem = "Pank music",
                    PriceItem = 60,
                    StateContent = 0,
                    CreatedDate = new DateTime(2018, 01, 10),
                    CreatorId = 50,
                });
            context.ContentCarts.Add(
                new ContentCart
                {
                    Id = 10,
                    ContentName = "Classic Song",
                    CategoryName = "Music",
                    CreatorName = "Sasha Dock",
                    DescriptionItem = "Classic music",
                    PriceItem = 60,
                    StateContent = 0,
                    CreatedDate = new DateTime(2018, 01, 10),
                    CreatorId = 50,
                });
            context.Products.Add(
                new Product
                {
                    Id = 5,
                    ContentName = "Melody1 Song",
                    CreatorName = "David Pay",
                    DescriptionItem = "Melody song",
                    PriceItem = 60,
                    CreatedDate = new DateTime(2018, 01, 05),
                    CreatorId = 40,
                });
            context.Products.Add(
                new Product
                {
                    Id = 6,
                    ContentName = "Melody2 Song",
                    CreatorName = "David Pay",
                    DescriptionItem = "Melody song",
                    PriceItem = 60,
                    CreatedDate = new DateTime(2018, 01, 06),
                    CreatorId = 40,
                });

            // Save changes
            context.SaveChanges();
        }
    }
}

namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Product_Migration_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompressedProducts",
                c => new
                {
                    CompressedProductId = c.Long(nullable: false, identity: true),
                    Content = c.Binary(),
                })
                .PrimaryKey(t => t.CompressedProductId);

            CreateTable(
                "dbo.Products",
                c => new
                {
                    Id = c.Long(nullable: false),
                    ProductName = c.String(),
                    Description = c.String(),
                    ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    IsPremium = c.Boolean(nullable: false),
                    IsFavorite = c.Boolean(nullable: false),
                    ProductType = c.Int(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatorId = c.Long(nullable: false),
                    ModifiedDate = c.DateTime(),
                    ModifierId = c.Long(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OriginalProducts", t => t.Id)
                .ForeignKey("dbo.ProtectedProducts", t => t.Id)
                .ForeignKey("dbo.CompressedProducts", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "dbo.OriginalProducts",
                c => new
                {
                    OriginalProductId = c.Long(nullable: false, identity: true),
                    Content = c.Binary(),
                })
                .PrimaryKey(t => t.OriginalProductId);

            CreateTable(
                "dbo.ProtectedProducts",
                c => new
                {
                    ProtectedProductId = c.Long(nullable: false, identity: true),
                    Content = c.Binary(),
                })
                .PrimaryKey(t => t.ProtectedProductId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Products", "Id", "dbo.CompressedProducts");
            DropForeignKey("dbo.Products", "Id", "dbo.ProtectedProducts");
            DropForeignKey("dbo.Products", "Id", "dbo.OriginalProducts");
            DropIndex("dbo.Products", new[] { "Id" });
            DropTable("dbo.ProtectedProducts");
            DropTable("dbo.OriginalProducts");
            DropTable("dbo.Products");
            DropTable("dbo.CompressedProducts");
        }
    }
}

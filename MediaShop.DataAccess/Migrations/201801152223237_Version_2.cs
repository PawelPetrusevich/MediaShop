namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompressedProducts",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Content = c.Binary(),
                        ProductId = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatorId = c.Long(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifierId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ProductName = c.String(),
                        Description = c.String(),
                        ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsPremium = c.Boolean(nullable: false),
                        IsFavorite = c.Boolean(nullable: false),
                        ProductType = c.Int(nullable: false),
                        OriginalProductId = c.Int(nullable: false),
                        ProtectedProductId = c.Int(nullable: false),
                        CompressedProductId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatorId = c.Long(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifierId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OriginalProducts",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Content = c.Binary(),
                        ProductId = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatorId = c.Long(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifierId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ProtectedProducts",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        Content = c.Binary(),
                        ProductId = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatorId = c.Long(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifierId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Id)
                .Index(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.ProtectedProducts", "Id", "dbo.Products");
            DropForeignKey("dbo.OriginalProducts", "Id", "dbo.Products");
            DropForeignKey("dbo.CompressedProducts", "Id", "dbo.Products");
            DropIndex("dbo.ProtectedProducts", new[] { "Id" });
            DropIndex("dbo.OriginalProducts", new[] { "Id" });
            DropIndex("dbo.CompressedProducts", new[] { "Id" });
            DropTable("dbo.ProtectedProducts");
            DropTable("dbo.OriginalProducts");
            DropTable("dbo.Products");
            DropTable("dbo.CompressedProducts");
        }
    }
}

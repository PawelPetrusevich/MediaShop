namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationProducts : DbMigration
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
            
            RenameColumn("dbo.Products", "ContentName", "ProductName");
            RenameColumn("dbo.Products", "DescriptionItem", "Description");
            RenameColumn("dbo.Products", "PriceItem", "ProductPrice");
            AddColumn("dbo.Products", "IsPremium", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "IsFavorite", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "ProductType", c => c.Int(nullable: false));
            AddForeignKey("dbo.Products", "Id", "dbo.OriginalProducts", "OriginalProductId");
            AddForeignKey("dbo.Products", "Id", "dbo.ProtectedProducts", "ProtectedProductId");
            AddForeignKey("dbo.Products", "Id", "dbo.CompressedProducts", "CompressedProductId");
            DropColumn("dbo.Products", "CreatorName");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Products", "ProductName", "ContentName");
            RenameColumn("dbo.Products", "Description", "DescriptionItem");
            RenameColumn("dbo.Products", "ProductPrice", "PriceItem");
            AddColumn("dbo.Products", "CreatorName", c => c.String());
            DropForeignKey("dbo.Products", "Id", "dbo.CompressedProducts");
            DropForeignKey("dbo.Products", "Id", "dbo.ProtectedProducts");
            DropForeignKey("dbo.Products", "Id", "dbo.OriginalProducts");
            DropColumn("dbo.Products", "ProductType");
            DropColumn("dbo.Products", "IsFavorite");
            DropColumn("dbo.Products", "IsPremium");
            DropTable("dbo.ProtectedProducts");
            DropTable("dbo.OriginalProducts");
            DropTable("dbo.CompressedProducts");
        }
    }
}

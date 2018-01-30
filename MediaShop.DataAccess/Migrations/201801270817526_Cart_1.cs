namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ContentName = c.String(nullable: false),
                        CreatorName = c.String(),
                        DescriptionItem = c.String(),
                        PriceItem = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatorId = c.Long(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifierId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ContentCarts", "ProductId", c => c.Long(nullable: false));
            CreateIndex("dbo.ContentCarts", "ProductId");
            AddForeignKey("dbo.ContentCarts", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            DropColumn("dbo.ContentCarts", "ContentName");
            DropColumn("dbo.ContentCarts", "CategoryName");
            DropColumn("dbo.ContentCarts", "CreatorName");
            DropColumn("dbo.ContentCarts", "DescriptionItem");
            DropColumn("dbo.ContentCarts", "PriceItem");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContentCarts", "PriceItem", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ContentCarts", "DescriptionItem", c => c.String());
            AddColumn("dbo.ContentCarts", "CreatorName", c => c.String());
            AddColumn("dbo.ContentCarts", "CategoryName", c => c.String(nullable: false));
            AddColumn("dbo.ContentCarts", "ContentName", c => c.String(nullable: false));
            DropForeignKey("dbo.ContentCarts", "ProductId", "dbo.Products");
            DropIndex("dbo.ContentCarts", new[] { "ProductId" });
            DropColumn("dbo.ContentCarts", "ProductId");
            DropTable("dbo.Products");
        }
    }
}

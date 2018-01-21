namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContentCarts", "Product_Id", c => c.Long());
            CreateIndex("dbo.ContentCarts", "Product_Id");
            AddForeignKey("dbo.ContentCarts", "Product_Id", "dbo.Products", "Id");
            DropColumn("dbo.ContentCarts", "ContentId");
            DropColumn("dbo.ContentCarts", "ContentName");
            DropColumn("dbo.ContentCarts", "DescriptionItem");
            DropColumn("dbo.ContentCarts", "PriceItem");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContentCarts", "PriceItem", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ContentCarts", "DescriptionItem", c => c.String());
            AddColumn("dbo.ContentCarts", "ContentName", c => c.String(nullable: false));
            AddColumn("dbo.ContentCarts", "ContentId", c => c.Long(nullable: false));
            DropForeignKey("dbo.ContentCarts", "Product_Id", "dbo.Products");
            DropIndex("dbo.ContentCarts", new[] { "Product_Id" });
            DropColumn("dbo.ContentCarts", "Product_Id");
        }
    }
}

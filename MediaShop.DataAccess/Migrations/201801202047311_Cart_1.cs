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
                        Id = c.Long(nullable: false),
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
            
            AddColumn("dbo.ContentCarts", "ContentId", c => c.Long(nullable: false));
            DropColumn("dbo.ContentCarts", "CategoryName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ContentCarts", "CategoryName", c => c.String(nullable: false));
            DropColumn("dbo.ContentCarts", "ContentId");
            DropTable("dbo.Products");
        }
    }
}

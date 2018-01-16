namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cart_1 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ContentCarts");
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

            CreateTable(
                 "dbo.ContentCarts",
                 c => new
                 {
                     Id = c.Long(nullable: false, identity: false),
                     ContentName = c.String(nullable: false),
                     CategoryName = c.String(nullable: false),
                     CreatorName = c.String(),
                     DescriptionItem = c.String(),
                     PriceItem = c.Decimal(nullable: false, precision: 18, scale: 2),
                     StateContent = c.Byte(nullable: false),
                     CreatedDate = c.DateTime(nullable: false),
                     CreatorId = c.Long(nullable: false),
                     ModifiedDate = c.DateTime(),
                     ModifierId = c.Long(),
                 })
                 .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.ContentCarts");
            CreateTable(
                 "dbo.ContentCarts",
                 c => new
                 {
                     Id = c.Long(nullable: false, identity: false),
                     ContentName = c.String(nullable: false),
                     CategoryName = c.String(nullable: false),
                     CreatorName = c.String(),
                     DescriptionItem = c.String(),
                     PriceItem = c.Decimal(nullable: false, precision: 18, scale: 2),
                     StateContent = c.Byte(nullable: false),
                     CreatedDate = c.DateTime(nullable: false),
                     CreatorId = c.Long(nullable: false),
                     ModifiedDate = c.DateTime(),
                     ModifierId = c.Long(),
                 })
                 .PrimaryKey(t => t.Id);
            DropTable("dbo.Products");
        }
    }
}

namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PayPalPayment_2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "IsDeleted");
            AddColumn("dbo.Products", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsDeleted");
        }
    }
}

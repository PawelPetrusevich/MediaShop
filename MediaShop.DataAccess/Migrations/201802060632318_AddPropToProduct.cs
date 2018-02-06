namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsDeleted");
        }
    }
}

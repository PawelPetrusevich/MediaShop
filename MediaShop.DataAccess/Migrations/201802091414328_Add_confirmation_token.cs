namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_confirmation_token : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccountDbModels", "AccountConfirmationToken", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccountDbModels", "AccountConfirmationToken");
        }
    }
}

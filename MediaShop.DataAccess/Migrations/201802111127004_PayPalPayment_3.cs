namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PayPalPayment_3 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DefrayalDbModels", "CreatorId");
            DropColumn("dbo.PayPalPaymentDbModels", "CreatorId");
            RenameColumn(table: "dbo.DefrayalDbModels", name: "AccountId", newName: "CreatorId");
            RenameColumn(table: "dbo.PayPalPaymentDbModels", name: "AccountId", newName: "CreatorId");
            RenameIndex(table: "dbo.DefrayalDbModels", name: "IX_AccountId", newName: "IX_CreatorId");
            RenameIndex(table: "dbo.PayPalPaymentDbModels", name: "IX_AccountId", newName: "IX_CreatorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.PayPalPaymentDbModels", name: "IX_CreatorId", newName: "IX_AccountId");
            RenameIndex(table: "dbo.DefrayalDbModels", name: "IX_CreatorId", newName: "IX_AccountId");
            RenameColumn(table: "dbo.PayPalPaymentDbModels", name: "CreatorId", newName: "AccountId");
            RenameColumn(table: "dbo.DefrayalDbModels", name: "CreatorId", newName: "AccountId");
            AddColumn("dbo.PayPalPaymentDbModels", "CreatorId", c => c.Long(nullable: false));
            AddColumn("dbo.DefrayalDbModels", "CreatorId", c => c.Long(nullable: false));
        }
    }
}

namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PayPalPayment_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DefrayalDbModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ContentId = c.Long(nullable: false),
                        AccountId = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatorId = c.Long(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifierId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountDbModels", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);

            CreateTable(
                "dbo.PayPalPaymentDbModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PaymentId = c.String(),
                        State = c.Byte(nullable: false),
                        AccountId = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatorId = c.Long(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifierId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountDbModels", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.PayPalPaymentDbModels", "AccountId", "dbo.AccountDbModels");
            DropForeignKey("dbo.DefrayalDbModels", "AccountId", "dbo.AccountDbModels");
            DropIndex("dbo.PayPalPaymentDbModels", new[] { "AccountId" });
            DropIndex("dbo.DefrayalDbModels", new[] { "AccountId" });
            DropTable("dbo.PayPalPaymentDbModels");
            DropTable("dbo.DefrayalDbModels");
        }
    }
}

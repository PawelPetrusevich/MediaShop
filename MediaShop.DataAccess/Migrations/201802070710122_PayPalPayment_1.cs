namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PayPalPayment_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PayPalPaymentDbModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PaymentId = c.String(),
                        State = c.Byte(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatorId = c.Long(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifierId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.PayPalPaymentDbModels");
        }
    }
}

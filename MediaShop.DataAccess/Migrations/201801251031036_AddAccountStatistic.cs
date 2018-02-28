namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAccountStatistic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatisticDbModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateLogIn = c.DateTime(nullable: false),
                        DateLogOut = c.DateTime(),
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
            DropForeignKey("dbo.StatisticDbModels", "AccountId", "dbo.AccountDbModels");
            DropIndex("dbo.StatisticDbModels", new[] { "AccountId" });
            DropTable("dbo.StatisticDbModels");
        }
    }
}

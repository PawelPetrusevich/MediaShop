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
                        CreatedDate = c.DateTime(nullable: false),
                        CreatorId = c.Long(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifierId = c.Long(),
                        AccountDbModel_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountDbModels", t => t.AccountDbModel_Id, cascadeDelete: true)
                .Index(t => t.AccountDbModel_Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatisticDbModels", "AccountDbModel_Id", "dbo.AccountDbModels");
            DropIndex("dbo.StatisticDbModels", new[] { "AccountDbModel_Id" });
            DropTable("dbo.StatisticDbModels");
        }
    }
}

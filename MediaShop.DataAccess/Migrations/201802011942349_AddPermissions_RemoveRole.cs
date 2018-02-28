namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPermissions_RemoveRole : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PermissionDbModels", "AccountDbModel_Id", "dbo.AccountDbModels");
            DropIndex("dbo.PermissionDbModels", new[] { "AccountDbModel_Id" });
            AddColumn("dbo.AccountDbModels", "Permissions", c => c.Int(nullable: false));
            DropTable("dbo.PermissionDbModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PermissionDbModels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Role = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatorId = c.Long(nullable: false),
                        ModifiedDate = c.DateTime(),
                        ModifierId = c.Long(),
                        AccountDbModel_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.AccountDbModels", "Permissions");
            CreateIndex("dbo.PermissionDbModels", "AccountDbModel_Id");
            AddForeignKey("dbo.PermissionDbModels", "AccountDbModel_Id", "dbo.AccountDbModels", "Id", cascadeDelete: true);
        }
    }
}

namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationPermissions : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Accounts", newName: "AccountDbModels");
            DropForeignKey("dbo.Profiles", "Id", "dbo.Accounts");
            DropForeignKey("dbo.Settings", "Id", "dbo.Accounts");
            DropIndex("dbo.Profiles", new[] { "Id" });
            DropIndex("dbo.Settings", new[] { "Id" });
            DropPrimaryKey("dbo.Profiles");
            DropPrimaryKey("dbo.Settings");
            CreateTable(
                "dbo.Permissions",
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AccountDbModels", t => t.AccountDbModel_Id, cascadeDelete: true)
                .Index(t => t.AccountDbModel_Id);
            
            AddColumn("dbo.AccountDbModels", "Email", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.AccountDbModels", "IsBanned", c => c.Boolean(nullable: false));
            AddColumn("dbo.AccountDbModels", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Profiles", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.Settings", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Profiles", "Id");
            AddPrimaryKey("dbo.Settings", "Id");
            CreateIndex("dbo.AccountDbModels", "ProfileId");
            CreateIndex("dbo.AccountDbModels", "SettingsId");
            AddForeignKey("dbo.AccountDbModels", "ProfileId", "dbo.Profiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AccountDbModels", "SettingsId", "dbo.Settings", "Id", cascadeDelete: true);
            DropColumn("dbo.Profiles", "Email");
            DropColumn("dbo.Profiles", "AccountId");
            DropColumn("dbo.Settings", "AccountId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Settings", "AccountId", c => c.Long(nullable: false));
            AddColumn("dbo.Profiles", "AccountId", c => c.Long(nullable: false));
            AddColumn("dbo.Profiles", "Email", c => c.String(nullable: false, maxLength: 30));
            DropForeignKey("dbo.AccountDbModels", "SettingsId", "dbo.Settings");
            DropForeignKey("dbo.AccountDbModels", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.Permissions", "AccountDbModel_Id", "dbo.AccountDbModels");
            DropIndex("dbo.Permissions", new[] { "AccountDbModel_Id" });
            DropIndex("dbo.AccountDbModels", new[] { "SettingsId" });
            DropIndex("dbo.AccountDbModels", new[] { "ProfileId" });
            DropPrimaryKey("dbo.Settings");
            DropPrimaryKey("dbo.Profiles");
            AlterColumn("dbo.Settings", "Id", c => c.Long(nullable: false));
            AlterColumn("dbo.Profiles", "Id", c => c.Long(nullable: false));
            DropColumn("dbo.AccountDbModels", "IsDeleted");
            DropColumn("dbo.AccountDbModels", "IsBanned");
            DropColumn("dbo.AccountDbModels", "Email");
            DropTable("dbo.Permissions");
            AddPrimaryKey("dbo.Settings", "Id");
            AddPrimaryKey("dbo.Profiles", "Id");
            CreateIndex("dbo.Settings", "Id");
            CreateIndex("dbo.Profiles", "Id");
            AddForeignKey("dbo.Settings", "Id", "dbo.Accounts", "Id");
            AddForeignKey("dbo.Profiles", "Id", "dbo.Accounts", "Id");
            RenameTable(name: "dbo.AccountDbModels", newName: "Accounts");
        }
    }
}

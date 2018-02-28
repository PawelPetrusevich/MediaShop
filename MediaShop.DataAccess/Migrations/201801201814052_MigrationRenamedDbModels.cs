namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationRenamedDbModels : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Permissions", newName: "PermissionDbModels");
            RenameTable(name: "dbo.Profiles", newName: "ProfileDbModels");
            RenameTable(name: "dbo.Settings", newName: "SettingsDbModels");
            DropForeignKey("dbo.AccountDbModels", "ProfileId", "dbo.Profiles");
            DropForeignKey("dbo.AccountDbModels", "SettingsId", "dbo.Settings");
            DropIndex("dbo.AccountDbModels", new[] { "ProfileId" });
            DropIndex("dbo.AccountDbModels", new[] { "SettingsId" });
            AddColumn("dbo.AccountDbModels", "IsConfirmed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.AccountDbModels", "ProfileId", c => c.Long());
            AlterColumn("dbo.AccountDbModels", "SettingsId", c => c.Long());
            CreateIndex("dbo.AccountDbModels", "ProfileId");
            CreateIndex("dbo.AccountDbModels", "SettingsId");
            AddForeignKey("dbo.AccountDbModels", "ProfileId", "dbo.ProfileDbModels", "Id");
            AddForeignKey("dbo.AccountDbModels", "SettingsId", "dbo.SettingsDbModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountDbModels", "SettingsId", "dbo.SettingsDbModels");
            DropForeignKey("dbo.AccountDbModels", "ProfileId", "dbo.ProfileDbModels");
            DropIndex("dbo.AccountDbModels", new[] { "SettingsId" });
            DropIndex("dbo.AccountDbModels", new[] { "ProfileId" });
            AlterColumn("dbo.AccountDbModels", "SettingsId", c => c.Long(nullable: false));
            AlterColumn("dbo.AccountDbModels", "ProfileId", c => c.Long(nullable: false));
            DropColumn("dbo.AccountDbModels", "IsConfirmed");
            CreateIndex("dbo.AccountDbModels", "SettingsId");
            CreateIndex("dbo.AccountDbModels", "ProfileId");
            AddForeignKey("dbo.AccountDbModels", "SettingsId", "dbo.Settings", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AccountDbModels", "ProfileId", "dbo.Profiles", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.SettingsDbModels", newName: "Settings");
            RenameTable(name: "dbo.ProfileDbModels", newName: "Profiles");
            RenameTable(name: "dbo.PermissionDbModels", newName: "Permissions");
        }
    }
}

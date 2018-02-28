namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Login = c.String(nullable: false),
                    Password = c.String(nullable: false),
                    ProfileId = c.Long(nullable: false),
                    SettingsId = c.Long(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatorId = c.Long(nullable: false),
                    ModifiedDate = c.DateTime(),
                    ModifierId = c.Long(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Profiles",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    Email = c.String(nullable: false, maxLength: 30),
                    DateOfBirth = c.DateTime(nullable: false),
                    FirstName = c.String(maxLength: 30),
                    LastName = c.String(maxLength: 30),
                    Phone = c.String(maxLength: 30),
                    AccountId = c.Long(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatorId = c.Long(nullable: false),
                    ModifiedDate = c.DateTime(),
                    ModifierId = c.Long(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "dbo.Settings",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    TimeZoneId = c.String(),
                    InterfaceLanguage = c.Byte(nullable: false),
                    NotificationStatus = c.Boolean(nullable: false),
                    AccountId = c.Long(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatorId = c.Long(nullable: false),
                    ModifiedDate = c.DateTime(),
                    ModifierId = c.Long(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Id)
                .Index(t => t.Id);

            CreateTable(
                "dbo.ContentCarts",
                c => new
                {
                    Id = c.Long(nullable: false, identity: true),
                    ContentName = c.String(nullable: false),
                    CategoryName = c.String(nullable: false),
                    CreatorName = c.String(),
                    DescriptionItem = c.String(),
                    PriceItem = c.Decimal(nullable: false, precision: 18, scale: 2),
                    StateContent = c.Byte(nullable: false),
                    CreatedDate = c.DateTime(nullable: false),
                    CreatorId = c.Long(nullable: false),
                    ModifiedDate = c.DateTime(),
                    ModifierId = c.Long(),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Settings", "Id", "dbo.Accounts");
            DropForeignKey("dbo.Profiles", "Id", "dbo.Accounts");
            DropIndex("dbo.Settings", new[] { "Id" });
            DropIndex("dbo.Profiles", new[] { "Id" });
            DropTable("dbo.ContentCarts");
            DropTable("dbo.Settings");
            DropTable("dbo.Profiles");
            DropTable("dbo.Accounts");
        }
    }
}

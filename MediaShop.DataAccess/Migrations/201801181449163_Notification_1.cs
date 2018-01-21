namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notification_1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Message = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        ReceiverId = c.Long(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatorId = c.Long(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifierId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.ReceiverId, cascadeDelete: true)
                .Index(t => t.ReceiverId);
            
            CreateTable(
                "dbo.NotificationSubscribedUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        DeviceIdentifier = c.String(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatorId = c.Long(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifierId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationSubscribedUsers", "UserId", "dbo.Accounts");
            DropForeignKey("dbo.Notifications", "ReceiverId", "dbo.Accounts");
            DropIndex("dbo.NotificationSubscribedUsers", new[] { "UserId" });
            DropIndex("dbo.Notifications", new[] { "ReceiverId" });
            DropTable("dbo.NotificationSubscribedUsers");
            DropTable("dbo.Notifications");
        }
    }
}

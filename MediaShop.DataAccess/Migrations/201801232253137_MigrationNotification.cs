namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationNotification : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NotificationSubscribedUsers", "UserId", "dbo.Accounts");
            DropForeignKey("dbo.Notifications", "ReceiverId", "dbo.Accounts");

            AddForeignKey("dbo.NotificationSubscribedUsers", "UserId", "dbo.AccountDbModels");
            AddForeignKey("dbo.Notifications", "ReceiverId", "dbo.AccountDbModels");          
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationSubscribedUsers", "UserId", "dbo.AccountDbModels");
            DropForeignKey("dbo.Notifications", "ReceiverId", "dbo.AccountDbModels");
            AddForeignKey("dbo.NotificationSubscribedUsers", "UserId", "dbo.Accounts");
            AddForeignKey("dbo.Notifications", "ReceiverId", "dbo.Accounts");
        }
    }
}

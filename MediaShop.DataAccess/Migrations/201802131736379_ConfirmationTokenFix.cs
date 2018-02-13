namespace MediaShop.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ConfirmationTokenFix : DbMigration
    {
        public override void Up()
        {
            string query = @"IF NOT EXISTS (
  SELECT 1 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'DemoLab.[dbo].[AccountDbModels]') 
         AND name = 'AccountConfirmationToken'
)
alter table DemoLab.[dbo].[AccountDbModels] 
add AccountConfirmationToken varchar(max) null";

            Sql(query, true);
        }

        public override void Down()
        {
            string query = @"IF NOT EXISTS (
  SELECT 1 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'DemoLab.[dbo].[AccountDbModels]') 
         AND name = 'AccountConfirmationToken'
)
alter table DemoLab.[dbo].[AccountDbModels] 
drop column AccountConfirmationToken";

            Sql(query, true);
        }
    }
}

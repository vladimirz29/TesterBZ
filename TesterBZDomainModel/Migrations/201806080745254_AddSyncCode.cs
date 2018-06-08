namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSyncCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AnswerTypes", "SyncCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AnswerTypes", "SyncCode");
        }
    }
}

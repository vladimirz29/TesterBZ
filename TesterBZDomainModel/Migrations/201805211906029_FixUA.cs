namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixUA : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserAnswers");
            AlterColumn("dbo.UserAnswers", "UserAnswerId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserAnswers", "UserAnswerId");
            DropColumn("dbo.UserAnswers", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserAnswers", "Id", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.UserAnswers");
            AlterColumn("dbo.UserAnswers", "UserAnswerId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.UserAnswers", "Id");
        }
    }
}

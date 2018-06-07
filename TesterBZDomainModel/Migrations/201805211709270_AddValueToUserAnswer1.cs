namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValueToUserAnswer1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserAnswers", "Value", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserAnswers", "Value", c => c.Int(nullable: false));
        }
    }
}

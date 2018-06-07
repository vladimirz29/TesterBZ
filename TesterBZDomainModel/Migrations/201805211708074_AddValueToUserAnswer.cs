namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValueToUserAnswer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserAnswers", "Value", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserAnswers", "Value");
        }
    }
}

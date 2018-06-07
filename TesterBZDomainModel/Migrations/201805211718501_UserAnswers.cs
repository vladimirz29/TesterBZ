namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAnswers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserAnswers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.UserAnswers", new[] { "QuestionId" });
            DropPrimaryKey("dbo.UserAnswers");
            AddColumn("dbo.UserAnswers", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserAnswers", "UserAnswerId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserAnswers", "QuestionId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.UserAnswers", "Id");
            CreateIndex("dbo.UserAnswers", "QuestionId");
            AddForeignKey("dbo.UserAnswers", "QuestionId", "dbo.Questions", "QuestionId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAnswers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.UserAnswers", new[] { "QuestionId" });
            DropPrimaryKey("dbo.UserAnswers");
            AlterColumn("dbo.UserAnswers", "QuestionId", c => c.Int());
            AlterColumn("dbo.UserAnswers", "UserAnswerId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.UserAnswers", "Id");
            AddPrimaryKey("dbo.UserAnswers", "UserAnswerId");
            CreateIndex("dbo.UserAnswers", "QuestionId");
            AddForeignKey("dbo.UserAnswers", "QuestionId", "dbo.Questions", "QuestionId");
        }
    }
}

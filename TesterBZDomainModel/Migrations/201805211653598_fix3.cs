namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserAnswers", "AnswerId", "dbo.Answers");
            DropIndex("dbo.UserAnswers", new[] { "AnswerId" });
            AddColumn("dbo.UserAnswers", "QuestionId", c => c.Int());
            CreateIndex("dbo.UserAnswers", "QuestionId");
            AddForeignKey("dbo.UserAnswers", "QuestionId", "dbo.Questions", "QuestionId");
            DropColumn("dbo.UserAnswers", "AnswerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserAnswers", "AnswerId", c => c.Int());
            DropForeignKey("dbo.UserAnswers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.UserAnswers", new[] { "QuestionId" });
            DropColumn("dbo.UserAnswers", "QuestionId");
            CreateIndex("dbo.UserAnswers", "AnswerId");
            AddForeignKey("dbo.UserAnswers", "AnswerId", "dbo.Answers", "AnswerId");
        }
    }
}

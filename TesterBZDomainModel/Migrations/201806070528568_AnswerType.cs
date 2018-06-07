namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnswerType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerTypes",
                c => new
                    {
                        AnswerTypeId = c.Int(nullable: false, identity: true),
                        AnswerTypeName = c.String(),
                    })
                .PrimaryKey(t => t.AnswerTypeId);
            
            AddColumn("dbo.Answers", "AnswerImage", c => c.String());
            AddColumn("dbo.Questions", "QuestionImage", c => c.String());
            AddColumn("dbo.Questions", "AnswerTypeId", c => c.Int());
            CreateIndex("dbo.Questions", "AnswerTypeId");
            AddForeignKey("dbo.Questions", "AnswerTypeId", "dbo.AnswerTypes", "AnswerTypeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "AnswerTypeId", "dbo.AnswerTypes");
            DropIndex("dbo.Questions", new[] { "AnswerTypeId" });
            DropColumn("dbo.Questions", "AnswerTypeId");
            DropColumn("dbo.Questions", "QuestionImage");
            DropColumn("dbo.Answers", "AnswerImage");
            DropTable("dbo.AnswerTypes");
        }
    }
}

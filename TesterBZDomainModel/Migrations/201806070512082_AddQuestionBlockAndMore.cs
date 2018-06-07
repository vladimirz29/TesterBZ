namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuestionBlockAndMore : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionBlocks",
                c => new
                    {
                        QuestionBlockId = c.Int(nullable: false, identity: true),
                        BlockName = c.String(),
                    })
                .PrimaryKey(t => t.QuestionBlockId);
            
            AddColumn("dbo.Questions", "QuestionBlockId", c => c.Int());
            CreateIndex("dbo.Questions", "QuestionBlockId");
            AddForeignKey("dbo.Questions", "QuestionBlockId", "dbo.QuestionBlocks", "QuestionBlockId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "QuestionBlockId", "dbo.QuestionBlocks");
            DropIndex("dbo.Questions", new[] { "QuestionBlockId" });
            DropColumn("dbo.Questions", "QuestionBlockId");
            DropTable("dbo.QuestionBlocks");
        }
    }
}

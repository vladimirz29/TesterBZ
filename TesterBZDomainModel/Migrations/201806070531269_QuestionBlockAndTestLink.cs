namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionBlockAndTestLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionBlocks", "TestId", c => c.Int());
            CreateIndex("dbo.QuestionBlocks", "TestId");
            AddForeignKey("dbo.QuestionBlocks", "TestId", "dbo.Tests", "TestId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionBlocks", "TestId", "dbo.Tests");
            DropIndex("dbo.QuestionBlocks", new[] { "TestId" });
            DropColumn("dbo.QuestionBlocks", "TestId");
        }
    }
}

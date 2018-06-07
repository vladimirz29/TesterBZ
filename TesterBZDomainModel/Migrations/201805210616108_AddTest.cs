namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestId = c.Int(nullable: false, identity: true),
                        TestName = c.String(),
                    })
                .PrimaryKey(t => t.TestId);
            
            AddColumn("dbo.Questions", "TestId", c => c.Int(nullable: false));
            CreateIndex("dbo.Questions", "TestId");
            AddForeignKey("dbo.Questions", "TestId", "dbo.Tests", "TestId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "TestId", "dbo.Tests");
            DropIndex("dbo.Questions", new[] { "TestId" });
            DropColumn("dbo.Questions", "TestId");
            DropTable("dbo.Tests");
        }
    }
}

namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTestAdmission : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestAdmissions",
                c => new
                    {
                        TestAdmissionId = c.Int(nullable: false, identity: true),
                        TestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TestAdmissionId)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: false)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.TestAdmissionApplicationUsers",
                c => new
                    {
                        TestAdmission_TestAdmissionId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.TestAdmission_TestAdmissionId, t.ApplicationUser_Id })
                .ForeignKey("dbo.TestAdmissions", t => t.TestAdmission_TestAdmissionId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: false)
                .Index(t => t.TestAdmission_TestAdmissionId)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TestAdmissionApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TestAdmissionApplicationUsers", "TestAdmission_TestAdmissionId", "dbo.TestAdmissions");
            DropForeignKey("dbo.TestAdmissions", "TestId", "dbo.Tests");
            DropIndex("dbo.TestAdmissionApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.TestAdmissionApplicationUsers", new[] { "TestAdmission_TestAdmissionId" });
            DropIndex("dbo.TestAdmissions", new[] { "TestId" });
            DropTable("dbo.TestAdmissionApplicationUsers");
            DropTable("dbo.TestAdmissions");
        }
    }
}

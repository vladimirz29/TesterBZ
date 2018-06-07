namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestAdmissionApplicationUsers", "TestAdmission_TestAdmissionId", "dbo.TestAdmissions");
            DropForeignKey("dbo.TestAdmissionApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TestAdmissionApplicationUsers", new[] { "TestAdmission_TestAdmissionId" });
            DropIndex("dbo.TestAdmissionApplicationUsers", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.TestAdmissions", "Id", c => c.Int(nullable: false));
            AddColumn("dbo.TestAdmissions", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.TestAdmissions", "User_Id");
            AddForeignKey("dbo.TestAdmissions", "User_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.TestAdmissionApplicationUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TestAdmissionApplicationUsers",
                c => new
                    {
                        TestAdmission_TestAdmissionId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.TestAdmission_TestAdmissionId, t.ApplicationUser_Id });
            
            DropForeignKey("dbo.TestAdmissions", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TestAdmissions", new[] { "User_Id" });
            DropColumn("dbo.TestAdmissions", "User_Id");
            DropColumn("dbo.TestAdmissions", "Id");
            CreateIndex("dbo.TestAdmissionApplicationUsers", "ApplicationUser_Id");
            CreateIndex("dbo.TestAdmissionApplicationUsers", "TestAdmission_TestAdmissionId");
            AddForeignKey("dbo.TestAdmissionApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: false);
            AddForeignKey("dbo.TestAdmissionApplicationUsers", "TestAdmission_TestAdmissionId", "dbo.TestAdmissions", "TestAdmissionId", cascadeDelete: false);
        }
    }
}

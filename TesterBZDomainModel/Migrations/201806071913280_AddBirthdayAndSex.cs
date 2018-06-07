namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBirthdayAndSex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "Sex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Sex");
            DropColumn("dbo.AspNetUsers", "Birthday");
        }
    }
}

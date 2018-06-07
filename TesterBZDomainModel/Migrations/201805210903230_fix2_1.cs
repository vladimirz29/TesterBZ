namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix2_1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TestAdmissions", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestAdmissions", "Id", c => c.Int(nullable: false));
        }
    }
}

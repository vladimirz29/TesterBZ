namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmallMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchemeMasks", "SmallMessage", c => c.String());
            AddColumn("dbo.SchemeMasks", "LargeMessage", c => c.String());
            DropColumn("dbo.SchemeMasks", "Message");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SchemeMasks", "Message", c => c.String());
            DropColumn("dbo.SchemeMasks", "LargeMessage");
            DropColumn("dbo.SchemeMasks", "SmallMessage");
        }
    }
}

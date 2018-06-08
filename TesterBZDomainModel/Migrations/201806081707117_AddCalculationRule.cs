namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCalculationRule : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalculationSchemes", "CalculationRule", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CalculationSchemes", "CalculationRule");
        }
    }
}

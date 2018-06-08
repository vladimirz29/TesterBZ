namespace TesterBZDomainModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCalculationScheme : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalculationSchemes",
                c => new
                    {
                        CalculationSchemeId = c.Int(nullable: false, identity: true),
                        CalculationSchemeName = c.String(),
                    })
                .PrimaryKey(t => t.CalculationSchemeId);
            
            CreateTable(
                "dbo.SchemeMasks",
                c => new
                    {
                        SchemeMaskId = c.Int(nullable: false, identity: true),
                        Mask = c.String(),
                        Value = c.Int(),
                        Message = c.String(),
                        CalculationSchemeId = c.Int(),
                    })
                .PrimaryKey(t => t.SchemeMaskId)
                .ForeignKey("dbo.CalculationSchemes", t => t.CalculationSchemeId)
                .Index(t => t.CalculationSchemeId);
            
            AddColumn("dbo.QuestionBlocks", "CalculationSchemeId", c => c.Int());
            CreateIndex("dbo.QuestionBlocks", "CalculationSchemeId");
            AddForeignKey("dbo.QuestionBlocks", "CalculationSchemeId", "dbo.CalculationSchemes", "CalculationSchemeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchemeMasks", "CalculationSchemeId", "dbo.CalculationSchemes");
            DropForeignKey("dbo.QuestionBlocks", "CalculationSchemeId", "dbo.CalculationSchemes");
            DropIndex("dbo.SchemeMasks", new[] { "CalculationSchemeId" });
            DropIndex("dbo.QuestionBlocks", new[] { "CalculationSchemeId" });
            DropColumn("dbo.QuestionBlocks", "CalculationSchemeId");
            DropTable("dbo.SchemeMasks");
            DropTable("dbo.CalculationSchemes");
        }
    }
}

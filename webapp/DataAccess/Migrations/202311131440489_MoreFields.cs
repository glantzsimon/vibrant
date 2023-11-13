namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "AntiInflammatory", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "AntiInflammatory", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "AntiInflammatory", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "AntiInflammatory", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "MemoryProblems", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "ConcentrationProblems", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Autoimmunity", c => c.Int());
            AddColumn("dbo.Ingredient", "AntiInflammatory", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "AntiInflammatory", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "AntiInflammatory", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "AntiInflammatory");
            DropColumn("dbo.ProductPack", "AntiInflammatory");
            DropColumn("dbo.Ingredient", "AntiInflammatory");
            DropColumn("dbo.HealthQuestionnaire", "Autoimmunity");
            DropColumn("dbo.HealthQuestionnaire", "ConcentrationProblems");
            DropColumn("dbo.HealthQuestionnaire", "MemoryProblems");
            DropColumn("dbo.FoodItem", "AntiInflammatory");
            DropColumn("dbo.DietaryRecommendation", "AntiInflammatory");
            DropColumn("dbo.Product", "AntiInflammatory");
            DropColumn("dbo.Activity", "AntiInflammatory");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Correction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Fertility", c => c.Boolean(nullable: false));
            DropColumn("dbo.Activity", "FertilityA");
            DropColumn("dbo.Product", "FertilityA");
            DropColumn("dbo.DietaryRecommendation", "FertilityA");
            DropColumn("dbo.FoodItem", "FertilityA");
            DropColumn("dbo.Ingredient", "FertilityA");
            DropColumn("dbo.ProductPack", "FertilityA");
            DropColumn("dbo.Protocol", "FertilityA");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Protocol", "FertilityA", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "FertilityA", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "FertilityA", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "FertilityA", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "FertilityA", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "FertilityA", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "FertilityA", c => c.Boolean(nullable: false));
            DropColumn("dbo.Protocol", "Fertility");
            DropColumn("dbo.ProductPack", "Fertility");
            DropColumn("dbo.Ingredient", "Fertility");
            DropColumn("dbo.FoodItem", "Fertility");
            DropColumn("dbo.DietaryRecommendation", "Fertility");
            DropColumn("dbo.Product", "Fertility");
            DropColumn("dbo.Activity", "Fertility");
        }
    }
}

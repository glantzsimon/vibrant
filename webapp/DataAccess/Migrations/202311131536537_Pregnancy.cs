namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pregnancy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "MensHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "WomensHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Pregnancy", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "MensHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "WomensHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Pregnancy", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "MensHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "WomensHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Pregnancy", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "MensHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "WomensHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "Pregnancy", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "MensHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "WomensHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Pregnancy", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "MensHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "WomensHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Pregnancy", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "MensHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "WomensHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Pregnancy", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Fertility", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "Fertility");
            DropColumn("dbo.Protocol", "Pregnancy");
            DropColumn("dbo.Protocol", "WomensHealth");
            DropColumn("dbo.Protocol", "MensHealth");
            DropColumn("dbo.ProductPack", "Fertility");
            DropColumn("dbo.ProductPack", "Pregnancy");
            DropColumn("dbo.ProductPack", "WomensHealth");
            DropColumn("dbo.ProductPack", "MensHealth");
            DropColumn("dbo.Ingredient", "Fertility");
            DropColumn("dbo.Ingredient", "Pregnancy");
            DropColumn("dbo.Ingredient", "WomensHealth");
            DropColumn("dbo.Ingredient", "MensHealth");
            DropColumn("dbo.FoodItem", "Fertility");
            DropColumn("dbo.FoodItem", "Pregnancy");
            DropColumn("dbo.FoodItem", "WomensHealth");
            DropColumn("dbo.FoodItem", "MensHealth");
            DropColumn("dbo.DietaryRecommendation", "Fertility");
            DropColumn("dbo.DietaryRecommendation", "Pregnancy");
            DropColumn("dbo.DietaryRecommendation", "WomensHealth");
            DropColumn("dbo.DietaryRecommendation", "MensHealth");
            DropColumn("dbo.Product", "Fertility");
            DropColumn("dbo.Product", "Pregnancy");
            DropColumn("dbo.Product", "WomensHealth");
            DropColumn("dbo.Product", "MensHealth");
            DropColumn("dbo.Activity", "Fertility");
            DropColumn("dbo.Activity", "Pregnancy");
            DropColumn("dbo.Activity", "WomensHealth");
            DropColumn("dbo.Activity", "MensHealth");
        }
    }
}

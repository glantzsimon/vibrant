namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDietaryPreference : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "Vegetarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Vegan", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Fruitarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Carnivore", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Pescatarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Vegetarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Vegan", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Fruitarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Carnivore", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Pescatarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Vegetarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Vegan", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Fruitarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Carnivore", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Pescatarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "Vegetarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "Vegan", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "Fruitarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "Carnivore", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "Pescatarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Vegetarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Vegan", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Fruitarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Carnivore", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Pescatarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Vegetarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Vegan", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Fruitarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Carnivore", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Pescatarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Vegetarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Vegan", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Fruitarian", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Carnivore", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Pescatarian", c => c.Boolean(nullable: false));
            DropColumn("dbo.Activity", "DietaryPreference");
            DropColumn("dbo.Product", "DietaryPreference");
            DropColumn("dbo.DietaryRecommendation", "DietaryPreference");
            DropColumn("dbo.FoodItem", "DietaryPreference");
            DropColumn("dbo.Ingredient", "DietaryPreference");
            DropColumn("dbo.ProductPack", "DietaryPreference");
            DropColumn("dbo.Protocol", "DietaryPreference");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Protocol", "DietaryPreference", c => c.Int());
            AddColumn("dbo.ProductPack", "DietaryPreference", c => c.Int());
            AddColumn("dbo.Ingredient", "DietaryPreference", c => c.Int());
            AddColumn("dbo.FoodItem", "DietaryPreference", c => c.Int());
            AddColumn("dbo.DietaryRecommendation", "DietaryPreference", c => c.Int());
            AddColumn("dbo.Product", "DietaryPreference", c => c.Int());
            AddColumn("dbo.Activity", "DietaryPreference", c => c.Int());
            DropColumn("dbo.Protocol", "Pescatarian");
            DropColumn("dbo.Protocol", "Carnivore");
            DropColumn("dbo.Protocol", "Fruitarian");
            DropColumn("dbo.Protocol", "Vegan");
            DropColumn("dbo.Protocol", "Vegetarian");
            DropColumn("dbo.ProductPack", "Pescatarian");
            DropColumn("dbo.ProductPack", "Carnivore");
            DropColumn("dbo.ProductPack", "Fruitarian");
            DropColumn("dbo.ProductPack", "Vegan");
            DropColumn("dbo.ProductPack", "Vegetarian");
            DropColumn("dbo.Ingredient", "Pescatarian");
            DropColumn("dbo.Ingredient", "Carnivore");
            DropColumn("dbo.Ingredient", "Fruitarian");
            DropColumn("dbo.Ingredient", "Vegan");
            DropColumn("dbo.Ingredient", "Vegetarian");
            DropColumn("dbo.FoodItem", "Pescatarian");
            DropColumn("dbo.FoodItem", "Carnivore");
            DropColumn("dbo.FoodItem", "Fruitarian");
            DropColumn("dbo.FoodItem", "Vegan");
            DropColumn("dbo.FoodItem", "Vegetarian");
            DropColumn("dbo.DietaryRecommendation", "Pescatarian");
            DropColumn("dbo.DietaryRecommendation", "Carnivore");
            DropColumn("dbo.DietaryRecommendation", "Fruitarian");
            DropColumn("dbo.DietaryRecommendation", "Vegan");
            DropColumn("dbo.DietaryRecommendation", "Vegetarian");
            DropColumn("dbo.Product", "Pescatarian");
            DropColumn("dbo.Product", "Carnivore");
            DropColumn("dbo.Product", "Fruitarian");
            DropColumn("dbo.Product", "Vegan");
            DropColumn("dbo.Product", "Vegetarian");
            DropColumn("dbo.Activity", "Pescatarian");
            DropColumn("dbo.Activity", "Carnivore");
            DropColumn("dbo.Activity", "Fruitarian");
            DropColumn("dbo.Activity", "Vegan");
            DropColumn("dbo.Activity", "Vegetarian");
        }
    }
}

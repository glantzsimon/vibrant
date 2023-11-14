namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLongColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "FertilityA", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "FertilityA", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "FertilityA", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "FertilityA", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "FertilityA", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "FertilityA", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "FertilityA", c => c.Boolean(nullable: false));
            DropColumn("dbo.Activity", "Fertility");
            DropColumn("dbo.Activity", "NourishesYin");
            DropColumn("dbo.Activity", "NourishesYang");
            DropColumn("dbo.Product", "Fertility");
            DropColumn("dbo.Product", "NourishesYin");
            DropColumn("dbo.Product", "NourishesYang");
            DropColumn("dbo.DietaryRecommendation", "Fertility");
            DropColumn("dbo.DietaryRecommendation", "NourishesYin");
            DropColumn("dbo.DietaryRecommendation", "NourishesYang");
            DropColumn("dbo.FoodItem", "Fertility");
            DropColumn("dbo.FoodItem", "NourishesYin");
            DropColumn("dbo.FoodItem", "NourishesYang");
            DropColumn("dbo.Ingredient", "Fertility");
            DropColumn("dbo.Ingredient", "NourishesYin");
            DropColumn("dbo.Ingredient", "NourishesYang");
            DropColumn("dbo.ProductPack", "Fertility");
            DropColumn("dbo.ProductPack", "NourishesYin");
            DropColumn("dbo.ProductPack", "NourishesYang");
            DropColumn("dbo.Protocol", "Fertility");
            DropColumn("dbo.Protocol", "NourishesYin");
            DropColumn("dbo.Protocol", "NourishesYang");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Protocol", "NourishesYang", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "NourishesYin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "NourishesYang", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "NourishesYin", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "NourishesYang", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "NourishesYin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "NourishesYang", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "NourishesYin", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "NourishesYang", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "NourishesYin", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "NourishesYang", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "NourishesYin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Fertility", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "NourishesYang", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "NourishesYin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Fertility", c => c.Boolean(nullable: false));
            DropColumn("dbo.Protocol", "FertilityA");
            DropColumn("dbo.ProductPack", "FertilityA");
            DropColumn("dbo.Ingredient", "FertilityA");
            DropColumn("dbo.FoodItem", "FertilityA");
            DropColumn("dbo.DietaryRecommendation", "FertilityA");
            DropColumn("dbo.Product", "FertilityA");
            DropColumn("dbo.Activity", "FertilityA");
        }
    }
}

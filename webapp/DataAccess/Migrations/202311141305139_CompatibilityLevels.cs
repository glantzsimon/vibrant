namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompatibilityLevels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "HunterCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Activity", "GathererCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Activity", "TeacherCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Activity", "ExplorerCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Activity", "WarriorCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Activity", "NomadCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "HunterCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "GathererCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "TeacherCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "ExplorerCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "WarriorCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "NomadCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "HunterCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "GathererCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "TeacherCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "ExplorerCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "WarriorCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "NomadCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.FoodItem", "HunterCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.FoodItem", "GathererCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.FoodItem", "TeacherCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.FoodItem", "ExplorerCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.FoodItem", "WarriorCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.FoodItem", "NomadCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredient", "HunterCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredient", "GathererCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredient", "TeacherCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredient", "ExplorerCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredient", "WarriorCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredient", "NomadCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.ProductPack", "HunterCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.ProductPack", "GathererCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.ProductPack", "TeacherCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.ProductPack", "ExplorerCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.ProductPack", "WarriorCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.ProductPack", "NomadCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Protocol", "HunterCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Protocol", "GathererCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Protocol", "TeacherCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Protocol", "ExplorerCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Protocol", "WarriorCompatibilityLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Protocol", "NomadCompatibilityLevel", c => c.Int(nullable: false));
            DropColumn("dbo.Activity", "GenoTypeScore");
            DropColumn("dbo.Product", "GenoTypeScore");
            DropColumn("dbo.DietaryRecommendation", "GenoTypeScore");
            DropColumn("dbo.FoodItem", "GenoTypeScore");
            DropColumn("dbo.Ingredient", "GenoTypeScore");
            DropColumn("dbo.ProductPack", "GenoTypeScore");
            DropColumn("dbo.Protocol", "GenoTypeScore");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Protocol", "GenoTypeScore", c => c.Int(nullable: false));
            AddColumn("dbo.ProductPack", "GenoTypeScore", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredient", "GenoTypeScore", c => c.Int(nullable: false));
            AddColumn("dbo.FoodItem", "GenoTypeScore", c => c.Int(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "GenoTypeScore", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "GenoTypeScore", c => c.Int(nullable: false));
            AddColumn("dbo.Activity", "GenoTypeScore", c => c.Int(nullable: false));
            DropColumn("dbo.Protocol", "NomadCompatibilityLevel");
            DropColumn("dbo.Protocol", "WarriorCompatibilityLevel");
            DropColumn("dbo.Protocol", "ExplorerCompatibilityLevel");
            DropColumn("dbo.Protocol", "TeacherCompatibilityLevel");
            DropColumn("dbo.Protocol", "GathererCompatibilityLevel");
            DropColumn("dbo.Protocol", "HunterCompatibilityLevel");
            DropColumn("dbo.ProductPack", "NomadCompatibilityLevel");
            DropColumn("dbo.ProductPack", "WarriorCompatibilityLevel");
            DropColumn("dbo.ProductPack", "ExplorerCompatibilityLevel");
            DropColumn("dbo.ProductPack", "TeacherCompatibilityLevel");
            DropColumn("dbo.ProductPack", "GathererCompatibilityLevel");
            DropColumn("dbo.ProductPack", "HunterCompatibilityLevel");
            DropColumn("dbo.Ingredient", "NomadCompatibilityLevel");
            DropColumn("dbo.Ingredient", "WarriorCompatibilityLevel");
            DropColumn("dbo.Ingredient", "ExplorerCompatibilityLevel");
            DropColumn("dbo.Ingredient", "TeacherCompatibilityLevel");
            DropColumn("dbo.Ingredient", "GathererCompatibilityLevel");
            DropColumn("dbo.Ingredient", "HunterCompatibilityLevel");
            DropColumn("dbo.FoodItem", "NomadCompatibilityLevel");
            DropColumn("dbo.FoodItem", "WarriorCompatibilityLevel");
            DropColumn("dbo.FoodItem", "ExplorerCompatibilityLevel");
            DropColumn("dbo.FoodItem", "TeacherCompatibilityLevel");
            DropColumn("dbo.FoodItem", "GathererCompatibilityLevel");
            DropColumn("dbo.FoodItem", "HunterCompatibilityLevel");
            DropColumn("dbo.DietaryRecommendation", "NomadCompatibilityLevel");
            DropColumn("dbo.DietaryRecommendation", "WarriorCompatibilityLevel");
            DropColumn("dbo.DietaryRecommendation", "ExplorerCompatibilityLevel");
            DropColumn("dbo.DietaryRecommendation", "TeacherCompatibilityLevel");
            DropColumn("dbo.DietaryRecommendation", "GathererCompatibilityLevel");
            DropColumn("dbo.DietaryRecommendation", "HunterCompatibilityLevel");
            DropColumn("dbo.Product", "NomadCompatibilityLevel");
            DropColumn("dbo.Product", "WarriorCompatibilityLevel");
            DropColumn("dbo.Product", "ExplorerCompatibilityLevel");
            DropColumn("dbo.Product", "TeacherCompatibilityLevel");
            DropColumn("dbo.Product", "GathererCompatibilityLevel");
            DropColumn("dbo.Product", "HunterCompatibilityLevel");
            DropColumn("dbo.Activity", "NomadCompatibilityLevel");
            DropColumn("dbo.Activity", "WarriorCompatibilityLevel");
            DropColumn("dbo.Activity", "ExplorerCompatibilityLevel");
            DropColumn("dbo.Activity", "TeacherCompatibilityLevel");
            DropColumn("dbo.Activity", "GathererCompatibilityLevel");
            DropColumn("dbo.Activity", "HunterCompatibilityLevel");
        }
    }
}

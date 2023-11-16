namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FoodFrequency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "HunterFrequency", c => c.Int());
            AddColumn("dbo.Activity", "GathererFrequency", c => c.Int());
            AddColumn("dbo.Activity", "TeacherFrequency", c => c.Int());
            AddColumn("dbo.Activity", "ExplorerFrequency", c => c.Int());
            AddColumn("dbo.Activity", "WarriorFrequency", c => c.Int());
            AddColumn("dbo.Activity", "NomadFrequency", c => c.Int());
            AddColumn("dbo.Product", "HunterFrequency", c => c.Int());
            AddColumn("dbo.Product", "GathererFrequency", c => c.Int());
            AddColumn("dbo.Product", "TeacherFrequency", c => c.Int());
            AddColumn("dbo.Product", "ExplorerFrequency", c => c.Int());
            AddColumn("dbo.Product", "WarriorFrequency", c => c.Int());
            AddColumn("dbo.Product", "NomadFrequency", c => c.Int());
            AddColumn("dbo.DietaryRecommendation", "HunterFrequency", c => c.Int());
            AddColumn("dbo.DietaryRecommendation", "GathererFrequency", c => c.Int());
            AddColumn("dbo.DietaryRecommendation", "TeacherFrequency", c => c.Int());
            AddColumn("dbo.DietaryRecommendation", "ExplorerFrequency", c => c.Int());
            AddColumn("dbo.DietaryRecommendation", "WarriorFrequency", c => c.Int());
            AddColumn("dbo.DietaryRecommendation", "NomadFrequency", c => c.Int());
            AddColumn("dbo.FoodItem", "HunterFrequency", c => c.Int());
            AddColumn("dbo.FoodItem", "GathererFrequency", c => c.Int());
            AddColumn("dbo.FoodItem", "TeacherFrequency", c => c.Int());
            AddColumn("dbo.FoodItem", "ExplorerFrequency", c => c.Int());
            AddColumn("dbo.FoodItem", "WarriorFrequency", c => c.Int());
            AddColumn("dbo.FoodItem", "NomadFrequency", c => c.Int());
            AddColumn("dbo.Ingredient", "HunterFrequency", c => c.Int());
            AddColumn("dbo.Ingredient", "GathererFrequency", c => c.Int());
            AddColumn("dbo.Ingredient", "TeacherFrequency", c => c.Int());
            AddColumn("dbo.Ingredient", "ExplorerFrequency", c => c.Int());
            AddColumn("dbo.Ingredient", "WarriorFrequency", c => c.Int());
            AddColumn("dbo.Ingredient", "NomadFrequency", c => c.Int());
            AddColumn("dbo.ProductPack", "HunterFrequency", c => c.Int());
            AddColumn("dbo.ProductPack", "GathererFrequency", c => c.Int());
            AddColumn("dbo.ProductPack", "TeacherFrequency", c => c.Int());
            AddColumn("dbo.ProductPack", "ExplorerFrequency", c => c.Int());
            AddColumn("dbo.ProductPack", "WarriorFrequency", c => c.Int());
            AddColumn("dbo.ProductPack", "NomadFrequency", c => c.Int());
            AddColumn("dbo.Protocol", "HunterFrequency", c => c.Int());
            AddColumn("dbo.Protocol", "GathererFrequency", c => c.Int());
            AddColumn("dbo.Protocol", "TeacherFrequency", c => c.Int());
            AddColumn("dbo.Protocol", "ExplorerFrequency", c => c.Int());
            AddColumn("dbo.Protocol", "WarriorFrequency", c => c.Int());
            AddColumn("dbo.Protocol", "NomadFrequency", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "NomadFrequency");
            DropColumn("dbo.Protocol", "WarriorFrequency");
            DropColumn("dbo.Protocol", "ExplorerFrequency");
            DropColumn("dbo.Protocol", "TeacherFrequency");
            DropColumn("dbo.Protocol", "GathererFrequency");
            DropColumn("dbo.Protocol", "HunterFrequency");
            DropColumn("dbo.ProductPack", "NomadFrequency");
            DropColumn("dbo.ProductPack", "WarriorFrequency");
            DropColumn("dbo.ProductPack", "ExplorerFrequency");
            DropColumn("dbo.ProductPack", "TeacherFrequency");
            DropColumn("dbo.ProductPack", "GathererFrequency");
            DropColumn("dbo.ProductPack", "HunterFrequency");
            DropColumn("dbo.Ingredient", "NomadFrequency");
            DropColumn("dbo.Ingredient", "WarriorFrequency");
            DropColumn("dbo.Ingredient", "ExplorerFrequency");
            DropColumn("dbo.Ingredient", "TeacherFrequency");
            DropColumn("dbo.Ingredient", "GathererFrequency");
            DropColumn("dbo.Ingredient", "HunterFrequency");
            DropColumn("dbo.FoodItem", "NomadFrequency");
            DropColumn("dbo.FoodItem", "WarriorFrequency");
            DropColumn("dbo.FoodItem", "ExplorerFrequency");
            DropColumn("dbo.FoodItem", "TeacherFrequency");
            DropColumn("dbo.FoodItem", "GathererFrequency");
            DropColumn("dbo.FoodItem", "HunterFrequency");
            DropColumn("dbo.DietaryRecommendation", "NomadFrequency");
            DropColumn("dbo.DietaryRecommendation", "WarriorFrequency");
            DropColumn("dbo.DietaryRecommendation", "ExplorerFrequency");
            DropColumn("dbo.DietaryRecommendation", "TeacherFrequency");
            DropColumn("dbo.DietaryRecommendation", "GathererFrequency");
            DropColumn("dbo.DietaryRecommendation", "HunterFrequency");
            DropColumn("dbo.Product", "NomadFrequency");
            DropColumn("dbo.Product", "WarriorFrequency");
            DropColumn("dbo.Product", "ExplorerFrequency");
            DropColumn("dbo.Product", "TeacherFrequency");
            DropColumn("dbo.Product", "GathererFrequency");
            DropColumn("dbo.Product", "HunterFrequency");
            DropColumn("dbo.Activity", "NomadFrequency");
            DropColumn("dbo.Activity", "WarriorFrequency");
            DropColumn("dbo.Activity", "ExplorerFrequency");
            DropColumn("dbo.Activity", "TeacherFrequency");
            DropColumn("dbo.Activity", "GathererFrequency");
            DropColumn("dbo.Activity", "HunterFrequency");
        }
    }
}

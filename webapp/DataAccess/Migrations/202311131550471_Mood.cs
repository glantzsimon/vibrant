namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mood : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "Mood", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Cognition", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Sleep", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Mood", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Cognition", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Sleep", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Mood", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Cognition", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Sleep", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "Mood", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "Cognition", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "Sleep", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Mood", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Cognition", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Sleep", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Mood", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Cognition", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Sleep", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Mood", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Cognition", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Sleep", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "Sleep");
            DropColumn("dbo.Protocol", "Cognition");
            DropColumn("dbo.Protocol", "Mood");
            DropColumn("dbo.ProductPack", "Sleep");
            DropColumn("dbo.ProductPack", "Cognition");
            DropColumn("dbo.ProductPack", "Mood");
            DropColumn("dbo.Ingredient", "Sleep");
            DropColumn("dbo.Ingredient", "Cognition");
            DropColumn("dbo.Ingredient", "Mood");
            DropColumn("dbo.FoodItem", "Sleep");
            DropColumn("dbo.FoodItem", "Cognition");
            DropColumn("dbo.FoodItem", "Mood");
            DropColumn("dbo.DietaryRecommendation", "Sleep");
            DropColumn("dbo.DietaryRecommendation", "Cognition");
            DropColumn("dbo.DietaryRecommendation", "Mood");
            DropColumn("dbo.Product", "Sleep");
            DropColumn("dbo.Product", "Cognition");
            DropColumn("dbo.Product", "Mood");
            DropColumn("dbo.Activity", "Sleep");
            DropColumn("dbo.Activity", "Cognition");
            DropColumn("dbo.Activity", "Mood");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DietaryPreference : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "DietaryPreference", c => c.Int());
            AddColumn("dbo.Product", "DietaryPreference", c => c.Int());
            AddColumn("dbo.DietaryRecommendation", "DietaryPreference", c => c.Int());
            AddColumn("dbo.FoodItem", "DietaryPreference", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "DietaryPreference", c => c.Int());
            AddColumn("dbo.Ingredient", "DietaryPreference", c => c.Int());
            AddColumn("dbo.ProductPack", "DietaryPreference", c => c.Int());
            AddColumn("dbo.Protocol", "DietaryPreference", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "DietaryPreference");
            DropColumn("dbo.ProductPack", "DietaryPreference");
            DropColumn("dbo.Ingredient", "DietaryPreference");
            DropColumn("dbo.HealthQuestionnaire", "DietaryPreference");
            DropColumn("dbo.FoodItem", "DietaryPreference");
            DropColumn("dbo.DietaryRecommendation", "DietaryPreference");
            DropColumn("dbo.Product", "DietaryPreference");
            DropColumn("dbo.Activity", "DietaryPreference");
        }
    }
}

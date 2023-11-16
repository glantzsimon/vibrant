namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Longevity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "Longevity", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Longevity", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Longevity", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "Longevity", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Longevity", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Longevity", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Longevity", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "Longevity");
            DropColumn("dbo.ProductPack", "Longevity");
            DropColumn("dbo.Ingredient", "Longevity");
            DropColumn("dbo.FoodItem", "Longevity");
            DropColumn("dbo.DietaryRecommendation", "Longevity");
            DropColumn("dbo.Product", "Longevity");
            DropColumn("dbo.Activity", "Longevity");
        }
    }
}

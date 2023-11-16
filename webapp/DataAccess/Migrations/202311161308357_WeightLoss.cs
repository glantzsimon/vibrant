namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WeightLoss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "WeightLoss", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "WeightLoss", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "WeightLoss", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "WeightLoss", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "WeightLoss", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "WeightLoss", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "WeightLoss", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "WeightLoss");
            DropColumn("dbo.ProductPack", "WeightLoss");
            DropColumn("dbo.Ingredient", "WeightLoss");
            DropColumn("dbo.FoodItem", "WeightLoss");
            DropColumn("dbo.DietaryRecommendation", "WeightLoss");
            DropColumn("dbo.Product", "WeightLoss");
            DropColumn("dbo.Activity", "WeightLoss");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScoresToDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProtocolActivity", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolActivity", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolDietaryRecommendation", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolDietaryRecommendation", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolFoodItem", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolFoodItem", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolProductPack", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolProductPack", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolProduct", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.ProtocolProduct", "RelativeScore", c => c.Int(nullable: false));
            DropColumn("dbo.Activity", "Score");
            DropColumn("dbo.Activity", "RelativeScore");
            DropColumn("dbo.Product", "Score");
            DropColumn("dbo.Product", "RelativeScore");
            DropColumn("dbo.DietaryRecommendation", "Score");
            DropColumn("dbo.DietaryRecommendation", "RelativeScore");
            DropColumn("dbo.FoodItem", "Score");
            DropColumn("dbo.FoodItem", "RelativeScore");
            DropColumn("dbo.Ingredient", "Score");
            DropColumn("dbo.Ingredient", "RelativeScore");
            DropColumn("dbo.ProductPack", "Score");
            DropColumn("dbo.ProductPack", "RelativeScore");
            DropColumn("dbo.Protocol", "Score");
            DropColumn("dbo.Protocol", "RelativeScore");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Protocol", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.Protocol", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.ProductPack", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.ProductPack", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredient", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredient", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.FoodItem", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.FoodItem", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.Activity", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.Activity", "Score", c => c.Int(nullable: false));
            DropColumn("dbo.ProtocolProduct", "RelativeScore");
            DropColumn("dbo.ProtocolProduct", "Score");
            DropColumn("dbo.ProtocolProductPack", "RelativeScore");
            DropColumn("dbo.ProtocolProductPack", "Score");
            DropColumn("dbo.ProtocolFoodItem", "RelativeScore");
            DropColumn("dbo.ProtocolFoodItem", "Score");
            DropColumn("dbo.ProtocolDietaryRecommendation", "RelativeScore");
            DropColumn("dbo.ProtocolDietaryRecommendation", "Score");
            DropColumn("dbo.ProtocolActivity", "RelativeScore");
            DropColumn("dbo.ProtocolActivity", "Score");
        }
    }
}

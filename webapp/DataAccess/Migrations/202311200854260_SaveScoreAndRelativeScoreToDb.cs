namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaveScoreAndRelativeScoreToDb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.Activity", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.FoodItem", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.FoodItem", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredient", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredient", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.ProductPack", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.ProductPack", "RelativeScore", c => c.Int(nullable: false));
            AddColumn("dbo.Protocol", "Score", c => c.Int(nullable: false));
            AddColumn("dbo.Protocol", "RelativeScore", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "RelativeScore");
            DropColumn("dbo.Protocol", "Score");
            DropColumn("dbo.ProductPack", "RelativeScore");
            DropColumn("dbo.ProductPack", "Score");
            DropColumn("dbo.Ingredient", "RelativeScore");
            DropColumn("dbo.Ingredient", "Score");
            DropColumn("dbo.FoodItem", "RelativeScore");
            DropColumn("dbo.FoodItem", "Score");
            DropColumn("dbo.DietaryRecommendation", "RelativeScore");
            DropColumn("dbo.DietaryRecommendation", "Score");
            DropColumn("dbo.Product", "RelativeScore");
            DropColumn("dbo.Product", "Score");
            DropColumn("dbo.Activity", "RelativeScore");
            DropColumn("dbo.Activity", "Score");
        }
    }
}

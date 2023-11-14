namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenoTypeScoreProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "GenoTypeScore", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "GenoTypeScore", c => c.Int(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "GenoTypeScore", c => c.Int(nullable: false));
            AddColumn("dbo.FoodItem", "GenoTypeScore", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredient", "GenoTypeScore", c => c.Int(nullable: false));
            AddColumn("dbo.ProductPack", "GenoTypeScore", c => c.Int(nullable: false));
            AddColumn("dbo.Protocol", "GenoTypeScore", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "GenoTypeScore");
            DropColumn("dbo.ProductPack", "GenoTypeScore");
            DropColumn("dbo.Ingredient", "GenoTypeScore");
            DropColumn("dbo.FoodItem", "GenoTypeScore");
            DropColumn("dbo.DietaryRecommendation", "GenoTypeScore");
            DropColumn("dbo.Product", "GenoTypeScore");
            DropColumn("dbo.Activity", "GenoTypeScore");
        }
    }
}

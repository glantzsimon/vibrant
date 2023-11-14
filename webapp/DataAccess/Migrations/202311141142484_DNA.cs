namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DNA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "DNAIntegrity", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "DNAIntegrity", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "DNAIntegrity", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "DNAIntegrity", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "DNAIntegrity", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "DNAIntegrity", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "DNAIntegrity", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "DNAIntegrity");
            DropColumn("dbo.ProductPack", "DNAIntegrity");
            DropColumn("dbo.Ingredient", "DNAIntegrity");
            DropColumn("dbo.FoodItem", "DNAIntegrity");
            DropColumn("dbo.DietaryRecommendation", "DNAIntegrity");
            DropColumn("dbo.Product", "DNAIntegrity");
            DropColumn("dbo.Activity", "DNAIntegrity");
        }
    }
}

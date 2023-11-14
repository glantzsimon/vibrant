namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AntiOxidant : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "AntiOxidant", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "AntiOxidant", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "AntiOxidant", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "AntiOxidant", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "AntiOxidant", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "AntiOxidant", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "AntiOxidant", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "AntiOxidant");
            DropColumn("dbo.ProductPack", "AntiOxidant");
            DropColumn("dbo.Ingredient", "AntiOxidant");
            DropColumn("dbo.FoodItem", "AntiOxidant");
            DropColumn("dbo.DietaryRecommendation", "AntiOxidant");
            DropColumn("dbo.Product", "AntiOxidant");
            DropColumn("dbo.Activity", "AntiOxidant");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CellularHealth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "CellularHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "CellularHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "CellularHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "CellularHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "CellularHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "CellularHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "CellularHealth", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "CellularHealth");
            DropColumn("dbo.ProductPack", "CellularHealth");
            DropColumn("dbo.Ingredient", "CellularHealth");
            DropColumn("dbo.FoodItem", "CellularHealth");
            DropColumn("dbo.DietaryRecommendation", "CellularHealth");
            DropColumn("dbo.Product", "CellularHealth");
            DropColumn("dbo.Activity", "CellularHealth");
        }
    }
}

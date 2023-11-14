namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BloodBuilding : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "BloodBuilding", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Restorative", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "BloodBuilding", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Restorative", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "BloodBuilding", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Restorative", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "BloodBuilding", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "Restorative", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "BloodBuilding", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Restorative", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "BloodBuilding", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Restorative", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "BloodBuilding", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Restorative", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "Restorative");
            DropColumn("dbo.Protocol", "BloodBuilding");
            DropColumn("dbo.ProductPack", "Restorative");
            DropColumn("dbo.ProductPack", "BloodBuilding");
            DropColumn("dbo.Ingredient", "Restorative");
            DropColumn("dbo.Ingredient", "BloodBuilding");
            DropColumn("dbo.FoodItem", "Restorative");
            DropColumn("dbo.FoodItem", "BloodBuilding");
            DropColumn("dbo.DietaryRecommendation", "Restorative");
            DropColumn("dbo.DietaryRecommendation", "BloodBuilding");
            DropColumn("dbo.Product", "Restorative");
            DropColumn("dbo.Product", "BloodBuilding");
            DropColumn("dbo.Activity", "Restorative");
            DropColumn("dbo.Activity", "BloodBuilding");
        }
    }
}

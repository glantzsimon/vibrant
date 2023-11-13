namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vitality : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "Vitality", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Vitality", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Vitality", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "Vitality", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Vitality", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Vitality", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Vitality", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "Vitality");
            DropColumn("dbo.ProductPack", "Vitality");
            DropColumn("dbo.Ingredient", "Vitality");
            DropColumn("dbo.FoodItem", "Vitality");
            DropColumn("dbo.DietaryRecommendation", "Vitality");
            DropColumn("dbo.Product", "Vitality");
            DropColumn("dbo.Activity", "Vitality");
        }
    }
}

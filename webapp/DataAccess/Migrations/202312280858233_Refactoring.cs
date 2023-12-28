namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Refactoring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItem", "IsAggravatesVata", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsAggravatesPitta", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsAggravatesKapha", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsSattvic", c => c.Boolean(nullable: false));
            DropColumn("dbo.FoodItem", "IsWaterElement");
            DropColumn("dbo.FoodItem", "IsTreeElement");
            DropColumn("dbo.FoodItem", "IsFireElement");
            DropColumn("dbo.FoodItem", "IsEarthElement");
            DropColumn("dbo.FoodItem", "IsMetalElement");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FoodItem", "IsMetalElement", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsEarthElement", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsFireElement", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsTreeElement", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsWaterElement", c => c.Boolean(nullable: false));
            DropColumn("dbo.FoodItem", "IsSattvic");
            DropColumn("dbo.FoodItem", "IsAggravatesKapha");
            DropColumn("dbo.FoodItem", "IsAggravatesPitta");
            DropColumn("dbo.FoodItem", "IsAggravatesVata");
        }
    }
}

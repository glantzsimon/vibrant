namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FoodCorrections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItem", "IsEarthElement", c => c.Boolean(nullable: false));
            DropColumn("dbo.FoodItem", "IsEarthlElement");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FoodItem", "IsEarthlElement", c => c.Boolean(nullable: false));
            DropColumn("dbo.FoodItem", "IsEarthElement");
        }
    }
}

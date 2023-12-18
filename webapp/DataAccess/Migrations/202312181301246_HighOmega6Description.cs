namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HighOmega6Description : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItem", "IsHighOmega6Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodItem", "IsHighOmega6Description");
        }
    }
}

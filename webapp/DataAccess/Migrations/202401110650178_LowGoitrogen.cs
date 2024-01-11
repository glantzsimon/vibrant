namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LowGoitrogen : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItem", "IsHighGoitrogen", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "IsLowGoitrogen", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "IsLowGoitrogen");
            DropColumn("dbo.FoodItem", "IsHighGoitrogen");
        }
    }
}

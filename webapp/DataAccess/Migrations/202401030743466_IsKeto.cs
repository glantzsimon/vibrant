namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsKeto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItem", "IsKeto", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "IsKeto", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "IsKeto");
            DropColumn("dbo.FoodItem", "IsKeto");
        }
    }
}

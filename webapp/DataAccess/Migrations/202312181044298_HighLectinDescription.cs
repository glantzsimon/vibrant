namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HighLectinDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItem", "HighLectinDescription", c => c.String());
            DropColumn("dbo.FoodItem", "IsVataDosha");
            DropColumn("dbo.FoodItem", "IsPittaDosha");
            DropColumn("dbo.FoodItem", "IsKaphaDosha");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FoodItem", "IsKaphaDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsPittaDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsVataDosha", c => c.Boolean(nullable: false));
            DropColumn("dbo.FoodItem", "HighLectinDescription");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LowSulphur : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItem", "IsHighSulphur", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "IsLowSulphur", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "IsLowSulphur");
            DropColumn("dbo.FoodItem", "IsHighSulphur");
        }
    }
}

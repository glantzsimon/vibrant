namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AutoFilterFoods : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "AutomaticallyFilterFoods", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "AutomaticallyFilterFoods");
        }
    }
}

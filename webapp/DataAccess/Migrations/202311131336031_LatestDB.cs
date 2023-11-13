namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LatestDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "ToothPain", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "TMJ", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "CrackedTeeth", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Cavities", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "Cavities");
            DropColumn("dbo.HealthQuestionnaire", "CrackedTeeth");
            DropColumn("dbo.HealthQuestionnaire", "TMJ");
            DropColumn("dbo.HealthQuestionnaire", "ToothPain");
        }
    }
}

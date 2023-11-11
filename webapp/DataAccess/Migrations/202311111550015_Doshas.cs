namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Doshas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "SkinType", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "SleepQuality", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "WeightGain", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "BodyTemperature", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "StressResponse", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "EyesType", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Disposition", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "HairType", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "HairType");
            DropColumn("dbo.HealthQuestionnaire", "Disposition");
            DropColumn("dbo.HealthQuestionnaire", "EyesType");
            DropColumn("dbo.HealthQuestionnaire", "StressResponse");
            DropColumn("dbo.HealthQuestionnaire", "BodyTemperature");
            DropColumn("dbo.HealthQuestionnaire", "WeightGain");
            DropColumn("dbo.HealthQuestionnaire", "SleepQuality");
            DropColumn("dbo.HealthQuestionnaire", "SkinType");
        }
    }
}

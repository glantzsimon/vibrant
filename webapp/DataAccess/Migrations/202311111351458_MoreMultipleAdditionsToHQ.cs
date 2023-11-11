namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreMultipleAdditionsToHQ : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "ChronicViralInfections", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "SpiderVeins", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "StretchMarks", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "FrequentNighttimeUrination", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Herpes", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Irritability", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "Irritability");
            DropColumn("dbo.HealthQuestionnaire", "Herpes");
            DropColumn("dbo.HealthQuestionnaire", "FrequentNighttimeUrination");
            DropColumn("dbo.HealthQuestionnaire", "StretchMarks");
            DropColumn("dbo.HealthQuestionnaire", "SpiderVeins");
            DropColumn("dbo.HealthQuestionnaire", "ChronicViralInfections");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewHQFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "Dyspnea", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "MucusInLungs", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Wheezing", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "ChronicCough", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "TightnessInChest", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "CovidVaccine", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "LongCovid", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "DentalAbcsesses", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "DentalAbcsesses");
            DropColumn("dbo.HealthQuestionnaire", "LongCovid");
            DropColumn("dbo.HealthQuestionnaire", "CovidVaccine");
            DropColumn("dbo.HealthQuestionnaire", "TightnessInChest");
            DropColumn("dbo.HealthQuestionnaire", "ChronicCough");
            DropColumn("dbo.HealthQuestionnaire", "Wheezing");
            DropColumn("dbo.HealthQuestionnaire", "MucusInLungs");
            DropColumn("dbo.HealthQuestionnaire", "Dyspnea");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MulipleChoiceAyurveda : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "DrySkin", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "ThickSkin", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "OilySkin", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "LightSleep", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "ModerateSleep", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "DeepSleep", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "WeightGainDifficult", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "WeightGainModerate", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "WeightGainEasy", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "BodyCold", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "BodyHot", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "BodyColdMoist", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "StressAnxious", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "StressIrritable", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "StressReclusive", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "SmallEyes", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "PenetratingEyes", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "LargeEyes", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "LivelyDisposition", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "DrivenDisposition", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "EvenDisposition", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "FrizzyHairType", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "FineHairType", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "FullHairType", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "FullHairType");
            DropColumn("dbo.HealthQuestionnaire", "FineHairType");
            DropColumn("dbo.HealthQuestionnaire", "FrizzyHairType");
            DropColumn("dbo.HealthQuestionnaire", "EvenDisposition");
            DropColumn("dbo.HealthQuestionnaire", "DrivenDisposition");
            DropColumn("dbo.HealthQuestionnaire", "LivelyDisposition");
            DropColumn("dbo.HealthQuestionnaire", "LargeEyes");
            DropColumn("dbo.HealthQuestionnaire", "PenetratingEyes");
            DropColumn("dbo.HealthQuestionnaire", "SmallEyes");
            DropColumn("dbo.HealthQuestionnaire", "StressReclusive");
            DropColumn("dbo.HealthQuestionnaire", "StressIrritable");
            DropColumn("dbo.HealthQuestionnaire", "StressAnxious");
            DropColumn("dbo.HealthQuestionnaire", "BodyColdMoist");
            DropColumn("dbo.HealthQuestionnaire", "BodyHot");
            DropColumn("dbo.HealthQuestionnaire", "BodyCold");
            DropColumn("dbo.HealthQuestionnaire", "WeightGainEasy");
            DropColumn("dbo.HealthQuestionnaire", "WeightGainModerate");
            DropColumn("dbo.HealthQuestionnaire", "WeightGainDifficult");
            DropColumn("dbo.HealthQuestionnaire", "DeepSleep");
            DropColumn("dbo.HealthQuestionnaire", "ModerateSleep");
            DropColumn("dbo.HealthQuestionnaire", "LightSleep");
            DropColumn("dbo.HealthQuestionnaire", "OilySkin");
            DropColumn("dbo.HealthQuestionnaire", "ThickSkin");
            DropColumn("dbo.HealthQuestionnaire", "DrySkin");
        }
    }
}

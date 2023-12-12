namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreDoshaQuestions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "NeuropathicPain", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "DryFlakySkin", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "IrregularDigestion", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "TroubleFallingAsleep", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Fretting", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "UlcersAcidReflux", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Nausea", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Acne", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "HyperMetabolism", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "ExcessBodyHeat", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "AngerFrustration", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "ImpatientIdealistic", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "RedEyes", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "SensitiveToLight", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "HighLibido", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "ExcessMucous", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "SlowInfrequentBowelMovements", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "MoodDrivenEating", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "EasyWeightGain", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "HardToWakeUp", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Lethargic", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Possessive", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Obstinacy", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "IBS", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "IBS");
            DropColumn("dbo.HealthQuestionnaire", "Obstinacy");
            DropColumn("dbo.HealthQuestionnaire", "Possessive");
            DropColumn("dbo.HealthQuestionnaire", "Lethargic");
            DropColumn("dbo.HealthQuestionnaire", "HardToWakeUp");
            DropColumn("dbo.HealthQuestionnaire", "EasyWeightGain");
            DropColumn("dbo.HealthQuestionnaire", "MoodDrivenEating");
            DropColumn("dbo.HealthQuestionnaire", "SlowInfrequentBowelMovements");
            DropColumn("dbo.HealthQuestionnaire", "ExcessMucous");
            DropColumn("dbo.HealthQuestionnaire", "HighLibido");
            DropColumn("dbo.HealthQuestionnaire", "SensitiveToLight");
            DropColumn("dbo.HealthQuestionnaire", "RedEyes");
            DropColumn("dbo.HealthQuestionnaire", "ImpatientIdealistic");
            DropColumn("dbo.HealthQuestionnaire", "AngerFrustration");
            DropColumn("dbo.HealthQuestionnaire", "ExcessBodyHeat");
            DropColumn("dbo.HealthQuestionnaire", "HyperMetabolism");
            DropColumn("dbo.HealthQuestionnaire", "Acne");
            DropColumn("dbo.HealthQuestionnaire", "Nausea");
            DropColumn("dbo.HealthQuestionnaire", "UlcersAcidReflux");
            DropColumn("dbo.HealthQuestionnaire", "Fretting");
            DropColumn("dbo.HealthQuestionnaire", "TroubleFallingAsleep");
            DropColumn("dbo.HealthQuestionnaire", "IrregularDigestion");
            DropColumn("dbo.HealthQuestionnaire", "DryFlakySkin");
            DropColumn("dbo.HealthQuestionnaire", "NeuropathicPain");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeQHColumnTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "Hiccups", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Asthma", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "CarpelTunnel", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "BackPain", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "BurningMouth", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "WhiteSpotsOnNails", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "Anaemia", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "PostExertionalMalaise", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "CrampsTremorsTwitches", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "Migraines", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "POTS", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "DepressionAnxiety", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "MemoryProblems", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "ConcentrationProblems", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "BrainFog", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "Insomnia", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "NightSweats", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "LowMorningEnergy", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "RacingThoughts", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "InnerTension", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "SugarCrashes", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "ChronicViralInfections", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "SpiderVeins", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "StretchMarks", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "FrequentNighttimeUrination", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "Herpes", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "Irritability", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "HighBloodPressure", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "ChestPain", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "Palpitations", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "EasilyOutOfBreath", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "Bloating", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "IBS", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "Gas", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "LooseStool", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "Constipation", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "AbdominalPainOrCramping", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "SkinIssues", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "CoatedTongue", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "UTI", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "ColdExtremities", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "ColdIntolerant", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "CandidaAndFungus", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "JointInflammation", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "Autoimmunity", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "AmalgamFillings", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "AmalgamFillingsHistory", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "ToothPain", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "TMJ", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "CrackedTeeth", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "Cavities", c => c.Boolean(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "RootCanals", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HealthQuestionnaire", "RootCanals", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "Cavities", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "CrackedTeeth", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "TMJ", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "ToothPain", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "AmalgamFillingsHistory", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "AmalgamFillings", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "Autoimmunity", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "JointInflammation", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "CandidaAndFungus", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "ColdIntolerant", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "ColdExtremities", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "UTI", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "CoatedTongue", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "SkinIssues", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "AbdominalPainOrCramping", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "Constipation", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "LooseStool", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "Gas", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "IBS", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "Bloating", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "EasilyOutOfBreath", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "Palpitations", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "ChestPain", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "HighBloodPressure", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "Irritability", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "Herpes", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "FrequentNighttimeUrination", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "StretchMarks", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "SpiderVeins", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "ChronicViralInfections", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "SugarCrashes", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "InnerTension", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "RacingThoughts", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "LowMorningEnergy", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "NightSweats", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "Insomnia", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "BrainFog", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "ConcentrationProblems", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "MemoryProblems", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "DepressionAnxiety", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "POTS", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "Migraines", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "CrampsTremorsTwitches", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "PostExertionalMalaise", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "Anaemia", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "WhiteSpotsOnNails", c => c.Int());
            DropColumn("dbo.HealthQuestionnaire", "BurningMouth");
            DropColumn("dbo.HealthQuestionnaire", "BackPain");
            DropColumn("dbo.HealthQuestionnaire", "CarpelTunnel");
            DropColumn("dbo.HealthQuestionnaire", "Asthma");
            DropColumn("dbo.HealthQuestionnaire", "Hiccups");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System.Data.Entity.Migrations;

    public partial class MultipleAdditionsToHQ : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "WhiteSpotsOnNails", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Anaemia", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "PostExertionalMalaise", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "CrampsTremorsTwitches", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "HistoryOfchronicFatigueOrFibromyalgia", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Migraines", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "POTS", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "DepressionAnxiety", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "BrainFog", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Insomnia", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "NightSweats", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "LowMorningEnergy", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "RacingThoughts", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "InnerTension", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "LoudNoisesBrightLights", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "CoarseThinEyebrows", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "AmmoniaSmell", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "SugarCrashes", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "OCD", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Bloating", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Gas", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "LooseStool", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Constipation", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "AbdominalPainOrCramping", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "SkinIssues", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "SkinIssuesDetails", c => c.String());
            AddColumn("dbo.HealthQuestionnaire", "CoatedTongue", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "InfectionSeverity", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "AllergiesAndSensitivities", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "HighBloodPressure", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "UTI", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "AllergiesAndSensitivitiesDetails", c => c.String());
            AddColumn("dbo.HealthQuestionnaire", "ColdExtremities", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "ColdIntolerant", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "CandidaAndFungus", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "JointInflammation", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "SurgeryDetails", c => c.String());
            AddColumn("dbo.HealthQuestionnaire", "PrescriptionMedication", c => c.String());
            AddColumn("dbo.HealthQuestionnaire", "Supplements", c => c.String());
            AddColumn("dbo.HealthQuestionnaire", "Smoke", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "DrinksAlcohol", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "PhysicalTraumaDetails", c => c.String());
            AddColumn("dbo.HealthQuestionnaire", "EmotionalTrauma", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "AmalgamFillings", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "AmalgamFillingsHistory", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "RootCanals", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "DentalIssuesLabel", c => c.String());
            AddColumn("dbo.HealthQuestionnaire", "Exercise", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "Exercise");
            DropColumn("dbo.HealthQuestionnaire", "DentalIssuesLabel");
            DropColumn("dbo.HealthQuestionnaire", "RootCanals");
            DropColumn("dbo.HealthQuestionnaire", "AmalgamFillingsHistory");
            DropColumn("dbo.HealthQuestionnaire", "AmalgamFillings");
            DropColumn("dbo.HealthQuestionnaire", "EmotionalTrauma");
            DropColumn("dbo.HealthQuestionnaire", "PhysicalTraumaDetails");
            DropColumn("dbo.HealthQuestionnaire", "DrinksAlcohol");
            DropColumn("dbo.HealthQuestionnaire", "Smoke");
            DropColumn("dbo.HealthQuestionnaire", "Supplements");
            DropColumn("dbo.HealthQuestionnaire", "PrescriptionMedication");
            DropColumn("dbo.HealthQuestionnaire", "SurgeryDetails");
            DropColumn("dbo.HealthQuestionnaire", "JointInflammation");
            DropColumn("dbo.HealthQuestionnaire", "CandidaAndFungus");
            DropColumn("dbo.HealthQuestionnaire", "ColdIntolerant");
            DropColumn("dbo.HealthQuestionnaire", "ColdExtremities");
            DropColumn("dbo.HealthQuestionnaire", "AllergiesAndSensitivitiesDetails");
            DropColumn("dbo.HealthQuestionnaire", "UTI");
            DropColumn("dbo.HealthQuestionnaire", "HighBloodPressure");
            DropColumn("dbo.HealthQuestionnaire", "AllergiesAndSensitivities");
            DropColumn("dbo.HealthQuestionnaire", "InfectionSeverity");
            DropColumn("dbo.HealthQuestionnaire", "CoatedTongue");
            DropColumn("dbo.HealthQuestionnaire", "SkinIssuesDetails");
            DropColumn("dbo.HealthQuestionnaire", "SkinIssues");
            DropColumn("dbo.HealthQuestionnaire", "AbdominalPainOrCramping");
            DropColumn("dbo.HealthQuestionnaire", "Constipation");
            DropColumn("dbo.HealthQuestionnaire", "LooseStool");
            DropColumn("dbo.HealthQuestionnaire", "Gas");
            DropColumn("dbo.HealthQuestionnaire", "Bloating");
            DropColumn("dbo.HealthQuestionnaire", "OCD");
            DropColumn("dbo.HealthQuestionnaire", "SugarCrashes");
            DropColumn("dbo.HealthQuestionnaire", "AmmoniaSmell");
            DropColumn("dbo.HealthQuestionnaire", "CoarseThinEyebrows");
            DropColumn("dbo.HealthQuestionnaire", "LoudNoisesBrightLights");
            DropColumn("dbo.HealthQuestionnaire", "InnerTension");
            DropColumn("dbo.HealthQuestionnaire", "RacingThoughts");
            DropColumn("dbo.HealthQuestionnaire", "LowMorningEnergy");
            DropColumn("dbo.HealthQuestionnaire", "NightSweats");
            DropColumn("dbo.HealthQuestionnaire", "Insomnia");
            DropColumn("dbo.HealthQuestionnaire", "BrainFog");
            DropColumn("dbo.HealthQuestionnaire", "DepressionAnxiety");
            DropColumn("dbo.HealthQuestionnaire", "POTS");
            DropColumn("dbo.HealthQuestionnaire", "Migraines");
            DropColumn("dbo.HealthQuestionnaire", "HistoryOfchronicFatigueOrFibromyalgia");
            DropColumn("dbo.HealthQuestionnaire", "CrampsTremorsTwitches");
            DropColumn("dbo.HealthQuestionnaire", "PostExertionalMalaise");
            DropColumn("dbo.HealthQuestionnaire", "Anaemia");
            DropColumn("dbo.HealthQuestionnaire", "WhiteSpotsOnNails");
        }
    }
}

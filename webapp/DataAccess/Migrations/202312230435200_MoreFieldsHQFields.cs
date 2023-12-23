namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MoreFieldsHQFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "WhiteSpotsOnSkin", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "PMS", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "LowBloodPressure", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Stroke", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "HeartAttack", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Diverticulitis", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "LeakyGut", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "LossOfAppetite", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Vomiting", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Nauseas", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Bronchitis", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "PulmonaryFibrosis", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "COPD", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "CysticFibrosis", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Pneumonia", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Tuberculosis", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "LungCancer", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "MusclePain", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Cataracts", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "KidneyStones", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "DifficultyUrinating", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "InterstitialCystitis", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Endometriosis", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "EyePain", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "MastCellActivation", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Type2Diabetes", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "MetabolicSyndroms", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Tinnitus", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "MetalicTasteInMouth", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "IncreasedThirst", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "FrequentlySick", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Numbness", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "MuscleWeakness", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "HormoneImbalance", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Rashes", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Impotency", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Fatigue", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Allergies", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "LackOfCoordination", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "VisionProblems", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Dizziness", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "AbdominalDistention", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Edema", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Hives", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "ADHD", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "IrregularMenstruation", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "NasalCongestion", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "FlushingAndRedness", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Itching", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "HypoThyroidism", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "SandyEye", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "PCOS", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "GrainyStools", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Osteoporosis", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "Hemorrhoids", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "HairLoss", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "HairLoss");
            DropColumn("dbo.HealthQuestionnaire", "Hemorrhoids");
            DropColumn("dbo.HealthQuestionnaire", "Osteoporosis");
            DropColumn("dbo.HealthQuestionnaire", "GrainyStools");
            DropColumn("dbo.HealthQuestionnaire", "PCOS");
            DropColumn("dbo.HealthQuestionnaire", "SandyEye");
            DropColumn("dbo.HealthQuestionnaire", "HypoThyroidism");
            DropColumn("dbo.HealthQuestionnaire", "Itching");
            DropColumn("dbo.HealthQuestionnaire", "FlushingAndRedness");
            DropColumn("dbo.HealthQuestionnaire", "NasalCongestion");
            DropColumn("dbo.HealthQuestionnaire", "IrregularMenstruation");
            DropColumn("dbo.HealthQuestionnaire", "ADHD");
            DropColumn("dbo.HealthQuestionnaire", "Hives");
            DropColumn("dbo.HealthQuestionnaire", "Edema");
            DropColumn("dbo.HealthQuestionnaire", "AbdominalDistention");
            DropColumn("dbo.HealthQuestionnaire", "Dizziness");
            DropColumn("dbo.HealthQuestionnaire", "VisionProblems");
            DropColumn("dbo.HealthQuestionnaire", "LackOfCoordination");
            DropColumn("dbo.HealthQuestionnaire", "Allergies");
            DropColumn("dbo.HealthQuestionnaire", "Fatigue");
            DropColumn("dbo.HealthQuestionnaire", "Impotency");
            DropColumn("dbo.HealthQuestionnaire", "Rashes");
            DropColumn("dbo.HealthQuestionnaire", "HormoneImbalance");
            DropColumn("dbo.HealthQuestionnaire", "MuscleWeakness");
            DropColumn("dbo.HealthQuestionnaire", "Numbness");
            DropColumn("dbo.HealthQuestionnaire", "FrequentlySick");
            DropColumn("dbo.HealthQuestionnaire", "IncreasedThirst");
            DropColumn("dbo.HealthQuestionnaire", "MetalicTasteInMouth");
            DropColumn("dbo.HealthQuestionnaire", "Tinnitus");
            DropColumn("dbo.HealthQuestionnaire", "MetabolicSyndroms");
            DropColumn("dbo.HealthQuestionnaire", "Type2Diabetes");
            DropColumn("dbo.HealthQuestionnaire", "MastCellActivation");
            DropColumn("dbo.HealthQuestionnaire", "EyePain");
            DropColumn("dbo.HealthQuestionnaire", "Endometriosis");
            DropColumn("dbo.HealthQuestionnaire", "InterstitialCystitis");
            DropColumn("dbo.HealthQuestionnaire", "DifficultyUrinating");
            DropColumn("dbo.HealthQuestionnaire", "KidneyStones");
            DropColumn("dbo.HealthQuestionnaire", "Cataracts");
            DropColumn("dbo.HealthQuestionnaire", "MusclePain");
            DropColumn("dbo.HealthQuestionnaire", "LungCancer");
            DropColumn("dbo.HealthQuestionnaire", "Tuberculosis");
            DropColumn("dbo.HealthQuestionnaire", "Pneumonia");
            DropColumn("dbo.HealthQuestionnaire", "CysticFibrosis");
            DropColumn("dbo.HealthQuestionnaire", "COPD");
            DropColumn("dbo.HealthQuestionnaire", "PulmonaryFibrosis");
            DropColumn("dbo.HealthQuestionnaire", "Bronchitis");
            DropColumn("dbo.HealthQuestionnaire", "Nauseas");
            DropColumn("dbo.HealthQuestionnaire", "Vomiting");
            DropColumn("dbo.HealthQuestionnaire", "LossOfAppetite");
            DropColumn("dbo.HealthQuestionnaire", "LeakyGut");
            DropColumn("dbo.HealthQuestionnaire", "Diverticulitis");
            DropColumn("dbo.HealthQuestionnaire", "HeartAttack");
            DropColumn("dbo.HealthQuestionnaire", "Stroke");
            DropColumn("dbo.HealthQuestionnaire", "LowBloodPressure");
            DropColumn("dbo.HealthQuestionnaire", "PMS");
            DropColumn("dbo.HealthQuestionnaire", "WhiteSpotsOnSkin");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HQMoreOptionalFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HealthQuestionnaire", "SensitivityToMedications", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "SensitiveToCaffeine", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "CaffeineAffectsSleep", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "SensitiveToMold", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "SensitiveToEnvironmentalChemicals", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "SpaceBetweenThighs", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "GonialAngle", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "TendonsAndSinewsVisible", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "WristsAndAnklesLookPadded", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "GainsMuscleEasily", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "SomatoType", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "IndexFingerprintsMatch", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "LeftThumprintType", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "LeftIndexFingerprintType", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "LeftMiddleFingerprintType", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "LeftRingFingerprintType", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "LeftLittleFingerprintType", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "RightThumprintType", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "RightIndexFingerprintType", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "RightMiddleFingerprintType", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "RightRingFingerprintType", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "RightLittleFingerprintType", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "LeftHanded", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "Ambidextrous", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "IncisorShovelling", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "CruciferousVegetablesTasteVeryBitter", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "FamilyHistoryOfNeurologicalDisease", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "FamilyHistoryOfHeartDiseaseStrokeOrDiabetes", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "FamilyHistoryOfCancer", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "FamilyHistoryOfAutoimmuneDisease", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "FamilyHistoryOfSubstanceDependency", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "EnjoysCooking", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "CookingFrequency", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HealthQuestionnaire", "CookingFrequency", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "EnjoysCooking", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "FamilyHistoryOfSubstanceDependency", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "FamilyHistoryOfAutoimmuneDisease", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "FamilyHistoryOfCancer", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "FamilyHistoryOfHeartDiseaseStrokeOrDiabetes", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "FamilyHistoryOfNeurologicalDisease", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "CruciferousVegetablesTasteVeryBitter", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "IncisorShovelling", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "Ambidextrous", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "LeftHanded", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "RightLittleFingerprintType", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "RightRingFingerprintType", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "RightMiddleFingerprintType", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "RightIndexFingerprintType", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "RightThumprintType", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "LeftLittleFingerprintType", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "LeftRingFingerprintType", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "LeftMiddleFingerprintType", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "LeftIndexFingerprintType", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "LeftThumprintType", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "IndexFingerprintsMatch", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "SomatoType", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "GainsMuscleEasily", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "WristsAndAnklesLookPadded", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "TendonsAndSinewsVisible", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "GonialAngle", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "SpaceBetweenThighs", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "SensitiveToEnvironmentalChemicals", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "SensitiveToMold", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "CaffeineAffectsSleep", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "SensitiveToCaffeine", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "SensitivityToMedications", c => c.Int(nullable: false));
        }
    }
}

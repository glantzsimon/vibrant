namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHealthQuestionnaire : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HealthQuestionnaire",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BloodGroup = c.Int(nullable: false),
                        RhesusFactor = c.Int(nullable: false),
                        SensitivityToMedications = c.Int(nullable: false),
                        SensitiveToCaffeine = c.Int(nullable: false),
                        CaffeineAffectsSleep = c.Int(nullable: false),
                        SensitiveToMold = c.Int(nullable: false),
                        SensitiveToEnvironmentalChemicals = c.Int(nullable: false),
                        StandingHeight = c.Double(nullable: false),
                        SittingHeight = c.Double(nullable: false),
                        ChairHeight = c.Double(nullable: false),
                        LowerLegLength = c.Double(nullable: false),
                        UpperLegLength = c.Double(nullable: false),
                        IndexFingerLengthLeft = c.Double(nullable: false),
                        IndexFingerLengthRight = c.Double(nullable: false),
                        RingFingerLengthLeft = c.Int(nullable: false),
                        RingFingerLengthRight = c.Double(nullable: false),
                        SpaceBetweenThighs = c.Int(nullable: false),
                        WaistSize = c.Double(nullable: false),
                        HipSize = c.Double(nullable: false),
                        GonialAngle = c.Int(nullable: false),
                        HeadWidth = c.Double(nullable: false),
                        HeadLength = c.Double(nullable: false),
                        TendonsAndSinewsVisible = c.Int(nullable: false),
                        WristsAndAnklesLookPadded = c.Int(nullable: false),
                        GainsMuscleEasily = c.Int(nullable: false),
                        SomatoType = c.Int(nullable: false),
                        IndexFingerprintsMatch = c.Int(nullable: false),
                        LeftThumprintType = c.Int(nullable: false),
                        LeftIndexFingerprintType = c.Int(nullable: false),
                        LeftMiddleFingerprintType = c.Int(nullable: false),
                        LeftRingFingerprintType = c.Int(nullable: false),
                        LeftLittleFingerprintType = c.Int(nullable: false),
                        RightThumprintType = c.Int(nullable: false),
                        RightIndexFingerprintType = c.Int(nullable: false),
                        RightMiddleFingerprintType = c.Int(nullable: false),
                        RightRingFingerprintType = c.Int(nullable: false),
                        RightLittleFingerprintType = c.Int(nullable: false),
                        LeftHanded = c.Int(nullable: false),
                        Ambidextrous = c.Int(nullable: false),
                        IncisorShovelling = c.Int(nullable: false),
                        CruciferousVegetablesTasteVeryBitter = c.Int(nullable: false),
                        FamilyHistoryOfNeurologicalDisease = c.Int(nullable: false),
                        FamilyHistoryOfHeartDiseaseStrokeOrDiabetes = c.Int(nullable: false),
                        FamilyHistoryOfCancer = c.Int(nullable: false),
                        FamilyHistoryOfAutoimmuneDisease = c.Int(nullable: false),
                        FamilyHistoryOfSubstanceDependency = c.Int(nullable: false),
                        ExternalId = c.Guid(nullable: false),
                        ClientId = c.Int(nullable: false),
                        Gender = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        CurrentHealthIssues = c.String(),
                        HealthGoals = c.String(),
                        CurrentHealthLevel = c.Int(nullable: false),
                        NutritionExpertiseLevel = c.Int(nullable: false),
                        EnjoysCooking = c.Int(nullable: false),
                        CookingFrequency = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Client", t => t.ClientId)
                .Index(t => t.ClientId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HealthQuestionnaire", "ClientId", "dbo.Client");
            DropIndex("dbo.HealthQuestionnaire", new[] { "Name" });
            DropIndex("dbo.HealthQuestionnaire", new[] { "ClientId" });
            DropTable("dbo.HealthQuestionnaire");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HQUpdateAgain : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HealthQuestionnaire", "CurrentHealthIssues", c => c.String(maxLength: 1111));
            AlterColumn("dbo.HealthQuestionnaire", "HealthGoals", c => c.String(maxLength: 1111));
            AlterColumn("dbo.HealthQuestionnaire", "SkinIssuesDetails", c => c.String(maxLength: 1111));
            AlterColumn("dbo.HealthQuestionnaire", "AllergiesAndSensitivitiesDetails", c => c.String(maxLength: 1111));
            AlterColumn("dbo.HealthQuestionnaire", "SurgeryDetails", c => c.String(maxLength: 1111));
            AlterColumn("dbo.HealthQuestionnaire", "PrescriptionMedication", c => c.String(maxLength: 1111));
            AlterColumn("dbo.HealthQuestionnaire", "Supplements", c => c.String(maxLength: 1111));
            AlterColumn("dbo.HealthQuestionnaire", "PhysicalTraumaDetails", c => c.String(maxLength: 1111));
            AlterColumn("dbo.HealthQuestionnaire", "DentalIssuesLabel", c => c.String(maxLength: 1111));
            AlterColumn("dbo.HealthQuestionnaire", "Exercise", c => c.String(maxLength: 1111));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HealthQuestionnaire", "Exercise", c => c.String());
            AlterColumn("dbo.HealthQuestionnaire", "DentalIssuesLabel", c => c.String());
            AlterColumn("dbo.HealthQuestionnaire", "PhysicalTraumaDetails", c => c.String());
            AlterColumn("dbo.HealthQuestionnaire", "Supplements", c => c.String());
            AlterColumn("dbo.HealthQuestionnaire", "PrescriptionMedication", c => c.String());
            AlterColumn("dbo.HealthQuestionnaire", "SurgeryDetails", c => c.String());
            AlterColumn("dbo.HealthQuestionnaire", "AllergiesAndSensitivitiesDetails", c => c.String());
            AlterColumn("dbo.HealthQuestionnaire", "SkinIssuesDetails", c => c.String());
            AlterColumn("dbo.HealthQuestionnaire", "HealthGoals", c => c.String());
            AlterColumn("dbo.HealthQuestionnaire", "CurrentHealthIssues", c => c.String());
        }
    }
}

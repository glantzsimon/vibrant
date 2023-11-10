namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HQNullableValues : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HealthQuestionnaire", "RingFingerLengthLeft", c => c.Double(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "CurrentHealthLevel", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "NutritionExpertiseLevel", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HealthQuestionnaire", "NutritionExpertiseLevel", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "CurrentHealthLevel", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "RingFingerLengthLeft", c => c.Int(nullable: false));
        }
    }
}

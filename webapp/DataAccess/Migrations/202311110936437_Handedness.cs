namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Handedness : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "Handedness", c => c.Int());
            DropColumn("dbo.HealthQuestionnaire", "IndexFingerprintsMatch");
            DropColumn("dbo.HealthQuestionnaire", "LeftHanded");
            DropColumn("dbo.HealthQuestionnaire", "Ambidextrous");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HealthQuestionnaire", "Ambidextrous", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "LeftHanded", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "IndexFingerprintsMatch", c => c.Int());
            DropColumn("dbo.HealthQuestionnaire", "Handedness");
        }
    }
}

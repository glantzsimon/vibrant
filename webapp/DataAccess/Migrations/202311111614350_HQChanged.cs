namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HQChanged : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.HealthQuestionnaire", "AllergiesAndSensitivities");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HealthQuestionnaire", "AllergiesAndSensitivities", c => c.Int());
        }
    }
}

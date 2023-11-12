namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SulfphitesAndMsg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "SulfiteSensitivity", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "MsgSensitivity", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "MsgSensitivity");
            DropColumn("dbo.HealthQuestionnaire", "SulfiteSensitivity");
        }
    }
}

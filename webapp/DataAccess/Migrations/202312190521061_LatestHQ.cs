namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LatestHQ : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "IsBulletProof", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "IsBulletProof");
        }
    }
}

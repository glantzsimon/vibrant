namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SattvicColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "IsSattvic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "IsSattvic");
        }
    }
}

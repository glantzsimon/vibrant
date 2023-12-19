namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EatsGrains : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "EatsGrains", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "EatsGrains");
        }
    }
}

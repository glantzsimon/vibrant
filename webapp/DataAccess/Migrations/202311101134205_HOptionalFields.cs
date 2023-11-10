namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HOptionalFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HealthQuestionnaire", "Gender", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HealthQuestionnaire", "Gender", c => c.Int(nullable: false));
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColumnChange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "DentalIssues", c => c.String(maxLength: 1111));
            DropColumn("dbo.HealthQuestionnaire", "DentalIssuesLabel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HealthQuestionnaire", "DentalIssuesLabel", c => c.String(maxLength: 1111));
            DropColumn("dbo.HealthQuestionnaire", "DentalIssues");
        }
    }
}

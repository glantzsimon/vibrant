namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivityRenamefield : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "Recommendations", c => c.String(nullable: false));
            DropColumn("dbo.Activity", "Reommendations");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Activity", "Reommendations", c => c.String(nullable: false));
            DropColumn("dbo.Activity", "Recommendations");
        }
    }
}

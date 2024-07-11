namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScheduleDisplay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Protocol", "IsScheduleDisplayed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "IsScheduleDisplayed");
        }
    }
}

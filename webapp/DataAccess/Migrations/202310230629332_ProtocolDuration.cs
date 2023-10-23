namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProtocolDuration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Protocol", "Duration", c => c.Int(nullable: false));
            AddColumn("dbo.Protocol", "CustomDaysDuration", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "CustomDaysDuration");
            DropColumn("dbo.Protocol", "Duration");
        }
    }
}

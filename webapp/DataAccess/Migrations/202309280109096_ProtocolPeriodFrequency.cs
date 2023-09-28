namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProtocolPeriodFrequency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Protocol", "Frequency", c => c.Int(nullable: false));
            AddColumn("dbo.Protocol", "Period", c => c.Int(nullable: false));
            AddColumn("dbo.Protocol", "PeriodValue", c => c.Int(nullable: false));
            AddColumn("dbo.Protocol", "NumberOfDaysOff", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "NumberOfDaysOff");
            DropColumn("dbo.Protocol", "PeriodValue");
            DropColumn("dbo.Protocol", "Period");
            DropColumn("dbo.Protocol", "Frequency");
        }
    }
}

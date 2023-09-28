namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePeriodOffName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Protocol", "NumberOfPeriodsOff", c => c.Int(nullable: false));
            DropColumn("dbo.Protocol", "NumberOfDaysOff");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Protocol", "NumberOfDaysOff", c => c.Int(nullable: false));
            DropColumn("dbo.Protocol", "NumberOfPeriodsOff");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProtocolFeatures : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Protocol", "NumberOfPeriodsOn", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "NumberOfPeriodsOn");
        }
    }
}

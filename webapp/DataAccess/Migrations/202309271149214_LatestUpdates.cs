namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LatestUpdates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProtocolSectionProduct", "Amount", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProtocolSectionProduct", "Amount");
        }
    }
}

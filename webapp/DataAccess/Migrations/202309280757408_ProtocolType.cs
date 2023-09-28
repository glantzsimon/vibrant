namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProtocolType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Protocol", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "Type");
        }
    }
}

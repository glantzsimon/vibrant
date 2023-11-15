namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredFromProtocolBody : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Protocol", "Body", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Protocol", "Body", c => c.String(nullable: false));
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScoreToProtocol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Protocol", "GenoType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Protocol", "GenoType");
        }
    }
}

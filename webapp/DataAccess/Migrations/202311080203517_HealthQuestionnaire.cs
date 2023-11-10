namespace K9.DataAccessLayer.Database
{
    using System.Data.Entity.Migrations;

    public partial class HealthQuestionnaire : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Protocol", "ProtocolFrequency", c => c.Int(nullable: false));
            DropColumn("dbo.Protocol", "Frequency");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Protocol", "Frequency", c => c.Int(nullable: false));
            DropColumn("dbo.Protocol", "ProtocolFrequency");
        }
    }
}

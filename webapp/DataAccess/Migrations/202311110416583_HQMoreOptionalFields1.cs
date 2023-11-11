namespace K9.DataAccessLayer.Database
{
    using System.Data.Entity.Migrations;

    public partial class HQMoreOptionalFields1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HealthQuestionnaire", "BloodGroup", c => c.Int());
            AlterColumn("dbo.HealthQuestionnaire", "RhesusFactor", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HealthQuestionnaire", "RhesusFactor", c => c.Int(nullable: false));
            AlterColumn("dbo.HealthQuestionnaire", "BloodGroup", c => c.Int(nullable: false));
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System.Data.Entity.Migrations;

    public partial class WristCircumference : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "WristCircumference", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "WristCircumference");
        }
    }
}

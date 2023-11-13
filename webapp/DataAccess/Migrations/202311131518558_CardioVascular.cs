namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CardioVascular : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "ChestPain", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Palpitations", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "EasilyOutOfBreath", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "EasilyOutOfBreath");
            DropColumn("dbo.HealthQuestionnaire", "Palpitations");
            DropColumn("dbo.HealthQuestionnaire", "ChestPain");
        }
    }
}

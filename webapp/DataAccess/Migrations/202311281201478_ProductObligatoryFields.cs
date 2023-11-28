namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductObligatoryFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "Nervousness", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "Spasms", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "LowVigour", c => c.Int());
            AddColumn("dbo.HealthQuestionnaire", "WeightLoss", c => c.Int());
            AlterColumn("dbo.Product", "Body", c => c.String());
            AlterColumn("dbo.Product", "Benefits", c => c.String());
            AlterColumn("dbo.Product", "Dosage", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "Dosage", c => c.String(nullable: false));
            AlterColumn("dbo.Product", "Benefits", c => c.String(nullable: false));
            AlterColumn("dbo.Product", "Body", c => c.String(nullable: false));
            DropColumn("dbo.HealthQuestionnaire", "WeightLoss");
            DropColumn("dbo.HealthQuestionnaire", "LowVigour");
            DropColumn("dbo.HealthQuestionnaire", "Spasms");
            DropColumn("dbo.HealthQuestionnaire", "Nervousness");
        }
    }
}

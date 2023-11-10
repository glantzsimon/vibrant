namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HealthQuestionnaireClientIdUnique : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.HealthQuestionnaire", new[] { "ClientId" });
            CreateIndex("dbo.HealthQuestionnaire", "ClientId", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.HealthQuestionnaire", new[] { "ClientId" });
            CreateIndex("dbo.HealthQuestionnaire", "ClientId");
        }
    }
}

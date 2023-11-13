namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DentalHealth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "DentalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "DentalHealth", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DietaryRecommendation", "DentalHealth");
            DropColumn("dbo.Activity", "DentalHealth");
        }
    }
}

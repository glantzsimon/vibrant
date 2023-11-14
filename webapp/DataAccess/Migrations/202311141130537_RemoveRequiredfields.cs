namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredfields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activity", "Body", c => c.String());
            AlterColumn("dbo.Activity", "Benefits", c => c.String());
            AlterColumn("dbo.Activity", "Recommendations", c => c.String());
            AlterColumn("dbo.DietaryRecommendation", "Body", c => c.String());
            AlterColumn("dbo.DietaryRecommendation", "Benefits", c => c.String());
            AlterColumn("dbo.DietaryRecommendation", "Recommendations", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DietaryRecommendation", "Recommendations", c => c.String(nullable: false));
            AlterColumn("dbo.DietaryRecommendation", "Benefits", c => c.String(nullable: false));
            AlterColumn("dbo.DietaryRecommendation", "Body", c => c.String(nullable: false));
            AlterColumn("dbo.Activity", "Recommendations", c => c.String(nullable: false));
            AlterColumn("dbo.Activity", "Benefits", c => c.String(nullable: false));
            AlterColumn("dbo.Activity", "Body", c => c.String(nullable: false));
        }
    }
}

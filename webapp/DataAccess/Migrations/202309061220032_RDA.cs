namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RDA : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ingredient", "RecommendedDailyAllownace", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ingredient", "RecommendedDailyAllownace");
        }
    }
}

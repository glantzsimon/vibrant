namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductRecommendations : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Recommendations", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Recommendations");
        }
    }
}

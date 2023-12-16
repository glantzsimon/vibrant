namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PromoteDemoteFoodItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientForbiddenFood", "IsPromotion", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientForbiddenFood", "IsPromotion");
        }
    }
}

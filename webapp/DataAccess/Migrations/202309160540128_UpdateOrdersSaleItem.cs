namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrdersSaleItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "OrderType", c => c.Int(nullable: false));
            DropColumn("dbo.Order", "SaleType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "SaleType", c => c.Int(nullable: false));
            DropColumn("dbo.Order", "OrderType");
        }
    }
}

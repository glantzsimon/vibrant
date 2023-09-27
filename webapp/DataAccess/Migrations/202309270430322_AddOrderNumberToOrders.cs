namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderNumberToOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "OrderNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "OrderNumber");
        }
    }
}

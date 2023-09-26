namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrderItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItemProductPack", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.OrderItem", "OrderItemType", c => c.Int(nullable: false));
            AddColumn("dbo.OrderItemProduct", "Price", c => c.Double(nullable: false));
            DropColumn("dbo.OrderItem", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItem", "Price", c => c.Double(nullable: false));
            DropColumn("dbo.OrderItemProduct", "Price");
            DropColumn("dbo.OrderItem", "OrderItemType");
            DropColumn("dbo.OrderItemProductPack", "Price");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAmountCompleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderProductPack", "AmountCompleted", c => c.Int(nullable: false));
            AddColumn("dbo.OrderProduct", "AmountCompleted", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderProductPack", "Amount", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderProduct", "Amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderProduct", "Amount", c => c.Single(nullable: false));
            AlterColumn("dbo.OrderProductPack", "Amount", c => c.Single(nullable: false));
            DropColumn("dbo.OrderProduct", "AmountCompleted");
            DropColumn("dbo.OrderProductPack", "AmountCompleted");
        }
    }
}

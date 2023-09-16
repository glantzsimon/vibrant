namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpandOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "RequestedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Order", "StartedOn", c => c.DateTime());
            AddColumn("dbo.Order", "DueBy", c => c.DateTime());
            AddColumn("dbo.Order", "MadeOn", c => c.DateTime());
            AddColumn("dbo.Order", "CompletedOn", c => c.DateTime());
            AddColumn("dbo.Order", "PaidOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "PaidOn");
            DropColumn("dbo.Order", "CompletedOn");
            DropColumn("dbo.Order", "MadeOn");
            DropColumn("dbo.Order", "DueBy");
            DropColumn("dbo.Order", "StartedOn");
            DropColumn("dbo.Order", "RequestedOn");
        }
    }
}

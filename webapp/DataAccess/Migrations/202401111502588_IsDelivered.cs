namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsDelivered : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "DeliveredOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "DeliveredOn");
        }
    }
}

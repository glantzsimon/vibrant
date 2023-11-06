namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsLocalDelivery : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "IsLocalDelivery", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "IsLocalDelivery");
        }
    }
}

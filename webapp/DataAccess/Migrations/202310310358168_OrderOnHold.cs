namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderOnHold : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "IsOnHold", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "IsOnHold");
        }
    }
}

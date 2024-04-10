namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomDiscount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "CustomDiscount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "CustomDiscount");
        }
    }
}

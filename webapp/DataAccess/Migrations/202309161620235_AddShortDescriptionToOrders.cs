namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShortDescriptionToOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "ShortDescription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "ShortDescription");
        }
    }
}

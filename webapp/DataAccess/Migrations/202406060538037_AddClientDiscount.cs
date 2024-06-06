namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClientDiscount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Client", "ClientDiscount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Client", "ClientDiscount");
        }
    }
}

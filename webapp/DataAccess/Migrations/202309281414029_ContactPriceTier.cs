namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactPriceTier : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contact", "PriceTier", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contact", "PriceTier");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAddressToContact : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Contact", new[] { "CountryId" });
            AlterColumn("dbo.Contact", "CountryId", c => c.Int());
            CreateIndex("dbo.Contact", "CountryId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Contact", new[] { "CountryId" });
            AlterColumn("dbo.Contact", "CountryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Contact", "CountryId");
        }
    }
}

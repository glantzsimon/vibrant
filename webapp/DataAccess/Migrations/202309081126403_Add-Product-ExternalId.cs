namespace K9.DataAccessLayer.Database
{
    using System.Data.Entity.Migrations;

    public partial class AddProductExternalId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ExternalId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "ExternalId");
        }
    }
}

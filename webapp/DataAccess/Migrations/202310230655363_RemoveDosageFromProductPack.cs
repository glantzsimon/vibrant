namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDosageFromProductPack : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductPack", "Dosage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductPack", "Dosage", c => c.String(nullable: false));
        }
    }
}

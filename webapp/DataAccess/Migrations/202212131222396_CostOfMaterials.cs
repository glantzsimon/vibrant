namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CostOfMaterials : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "CostOfMaterials", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "CostOfMaterials");
        }
    }
}

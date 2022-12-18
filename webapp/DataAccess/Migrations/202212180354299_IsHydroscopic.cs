namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsHydroscopic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "IsHydroscopic", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "IsHydroscopic");
        }
    }
}

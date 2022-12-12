namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AmountPerServing : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "AmountPerServing", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "AmountPerServing");
        }
    }
}

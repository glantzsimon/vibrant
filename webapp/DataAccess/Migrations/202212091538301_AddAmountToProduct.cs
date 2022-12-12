namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAmountToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Amount", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Amount");
        }
    }
}

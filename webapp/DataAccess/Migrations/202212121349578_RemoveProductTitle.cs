namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProductTitle : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Product", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Title", c => c.String(nullable: false, maxLength: 256));
        }
    }
}

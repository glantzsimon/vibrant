namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsMain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "IsMain", c => c.Boolean(nullable: false));
            DropColumn("dbo.Product", "IsLiveOn");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "IsLiveOn", c => c.DateTime(nullable: false));
            DropColumn("dbo.Product", "IsMain");
        }
    }
}

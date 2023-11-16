namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRequiredFromActivityShortDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activity", "ShortDescription", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Activity", "ShortDescription", c => c.String(nullable: false));
        }
    }
}

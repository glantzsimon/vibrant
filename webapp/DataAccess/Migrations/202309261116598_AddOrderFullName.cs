namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderFullName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "FullName");
        }
    }
}

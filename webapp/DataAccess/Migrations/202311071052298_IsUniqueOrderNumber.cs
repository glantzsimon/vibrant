namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsUniqueOrderNumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "OrderNumber", c => c.String(maxLength: 9));
            CreateIndex("dbo.Order", "OrderNumber", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Order", new[] { "OrderNumber" });
            AlterColumn("dbo.Order", "OrderNumber", c => c.String());
        }
    }
}

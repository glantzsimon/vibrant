namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderRep : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "RepId", c => c.Int());
            CreateIndex("dbo.Order", "RepId");
            AddForeignKey("dbo.Order", "RepId", "dbo.Contact", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "RepId", "dbo.Contact");
            DropIndex("dbo.Order", new[] { "RepId" });
            DropColumn("dbo.Order", "RepId");
        }
    }
}

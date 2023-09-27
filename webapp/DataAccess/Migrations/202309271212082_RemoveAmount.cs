namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAmount : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProtocolSectionProduct", "Amount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProtocolSectionProduct", "Amount", c => c.Single(nullable: false));
        }
    }
}

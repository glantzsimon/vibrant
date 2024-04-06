namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomProtocolSection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProtocolSection", "IsCustom", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProtocolSection", "IsCustom");
        }
    }
}

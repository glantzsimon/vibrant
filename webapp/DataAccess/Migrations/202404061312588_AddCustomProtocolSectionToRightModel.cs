namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomProtocolSectionToRightModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Section", "IsCustom", c => c.Boolean(nullable: false));
            DropColumn("dbo.ProtocolSection", "IsCustom");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProtocolSection", "IsCustom", c => c.Boolean(nullable: false));
            DropColumn("dbo.Section", "IsCustom");
        }
    }
}

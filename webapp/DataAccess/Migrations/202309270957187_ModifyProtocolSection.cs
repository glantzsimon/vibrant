namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyProtocolSection : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProtocolProtocolSection", newName: "ProtoProtocolSection");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ProtoProtocolSection", newName: "ProtocolProtocolSection");
        }
    }
}

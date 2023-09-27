namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNameBack : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProtoProtocolSection", newName: "ProtocolProtocolSection");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ProtocolProtocolSection", newName: "ProtoProtocolSection");
        }
    }
}

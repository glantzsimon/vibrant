namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserProtocols : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProtocol",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProtocolId = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Protocol", t => t.ProtocolId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ProtocolId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProtocol", "UserId", "dbo.User");
            DropForeignKey("dbo.UserProtocol", "ProtocolId", "dbo.Protocol");
            DropIndex("dbo.UserProtocol", new[] { "Name" });
            DropIndex("dbo.UserProtocol", new[] { "ProtocolId" });
            DropIndex("dbo.UserProtocol", new[] { "UserId" });
            DropTable("dbo.UserProtocol");
        }
    }
}

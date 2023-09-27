namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActivities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExternalId = c.Guid(nullable: false),
                        ShortDescription = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        Benefits = c.String(nullable: false),
                        Reommendations = c.String(nullable: false),
                        ImageUrl = c.String(maxLength: 512),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.ProtocolActivity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProtocolId = c.Int(nullable: false),
                        ActivityId = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activity", t => t.ActivityId)
                .ForeignKey("dbo.Protocol", t => t.ProtocolId)
                .Index(t => t.ProtocolId)
                .Index(t => t.ActivityId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProtocolActivity", "ProtocolId", "dbo.Protocol");
            DropForeignKey("dbo.ProtocolActivity", "ActivityId", "dbo.Activity");
            DropIndex("dbo.ProtocolActivity", new[] { "Name" });
            DropIndex("dbo.ProtocolActivity", new[] { "ActivityId" });
            DropIndex("dbo.ProtocolActivity", new[] { "ProtocolId" });
            DropIndex("dbo.Activity", new[] { "Name" });
            DropTable("dbo.ProtocolActivity");
            DropTable("dbo.Activity");
        }
    }
}

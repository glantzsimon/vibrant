namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProtocols : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProtocolProductPack",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProtocolId = c.Int(nullable: false),
                        ProductPackId = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductPack", t => t.ProductPackId)
                .ForeignKey("dbo.Protocol", t => t.ProtocolId)
                .Index(t => t.ProtocolId)
                .Index(t => t.ProductPackId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Protocol",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExternalId = c.Guid(nullable: false),
                        ClientId = c.Int(),
                        ShortDescription = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        SeoFriendlyId = c.String(),
                        VideoUrl = c.String(maxLength: 512),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contact", t => t.ClientId)
                .Index(t => t.ClientId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.ProtocolProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProtocolId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.Protocol", t => t.ProtocolId)
                .Index(t => t.ProtocolId)
                .Index(t => t.ProductId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.ProtocolProtocolSectionProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProtocolProtocolSectionId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.ProtocolProtocolSection", t => t.ProtocolProtocolSectionId)
                .Index(t => t.ProtocolProtocolSectionId)
                .Index(t => t.ProductId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.ProtocolProtocolSection",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProtocolId = c.Int(nullable: false),
                        ProtocolSectionId = c.Int(nullable: false),
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
                .ForeignKey("dbo.ProtocolSection", t => t.ProtocolSectionId)
                .Index(t => t.ProtocolId)
                .Index(t => t.ProtocolSectionId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.ProtocolSection",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Recommendations = c.Int(nullable: false),
                        ShortDescription = c.String(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.DisplayOrder, unique: true, name: "IX_ProtocolSection_DisplayOrder")
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProtocolProtocolSectionProduct", "ProtocolProtocolSectionId", "dbo.ProtocolProtocolSection");
            DropForeignKey("dbo.ProtocolProtocolSection", "ProtocolSectionId", "dbo.ProtocolSection");
            DropForeignKey("dbo.ProtocolProtocolSection", "ProtocolId", "dbo.Protocol");
            DropForeignKey("dbo.ProtocolProtocolSectionProduct", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProtocolProduct", "ProtocolId", "dbo.Protocol");
            DropForeignKey("dbo.ProtocolProduct", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProtocolProductPack", "ProtocolId", "dbo.Protocol");
            DropForeignKey("dbo.Protocol", "ClientId", "dbo.Contact");
            DropForeignKey("dbo.ProtocolProductPack", "ProductPackId", "dbo.ProductPack");
            DropIndex("dbo.ProtocolSection", new[] { "Name" });
            DropIndex("dbo.ProtocolSection", "IX_ProtocolSection_DisplayOrder");
            DropIndex("dbo.ProtocolProtocolSection", new[] { "Name" });
            DropIndex("dbo.ProtocolProtocolSection", new[] { "ProtocolSectionId" });
            DropIndex("dbo.ProtocolProtocolSection", new[] { "ProtocolId" });
            DropIndex("dbo.ProtocolProtocolSectionProduct", new[] { "Name" });
            DropIndex("dbo.ProtocolProtocolSectionProduct", new[] { "ProductId" });
            DropIndex("dbo.ProtocolProtocolSectionProduct", new[] { "ProtocolProtocolSectionId" });
            DropIndex("dbo.ProtocolProduct", new[] { "Name" });
            DropIndex("dbo.ProtocolProduct", new[] { "ProductId" });
            DropIndex("dbo.ProtocolProduct", new[] { "ProtocolId" });
            DropIndex("dbo.Protocol", new[] { "Name" });
            DropIndex("dbo.Protocol", new[] { "ClientId" });
            DropIndex("dbo.ProtocolProductPack", new[] { "Name" });
            DropIndex("dbo.ProtocolProductPack", new[] { "ProductPackId" });
            DropIndex("dbo.ProtocolProductPack", new[] { "ProtocolId" });
            DropTable("dbo.ProtocolSection");
            DropTable("dbo.ProtocolProtocolSection");
            DropTable("dbo.ProtocolProtocolSectionProduct");
            DropTable("dbo.ProtocolProduct");
            DropTable("dbo.Protocol");
            DropTable("dbo.ProtocolProductPack");
        }
    }
}

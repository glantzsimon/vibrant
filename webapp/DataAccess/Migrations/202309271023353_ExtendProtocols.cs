namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExtendProtocols : DbMigration
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
                "dbo.ProtocolSectionProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProtocolSectionId = c.Int(nullable: false),
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
                .ForeignKey("dbo.ProtocolSection", t => t.ProtocolSectionId)
                .Index(t => t.ProtocolSectionId)
                .Index(t => t.ProductId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.ProtocolSection",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProtocolId = c.Int(nullable: false),
                        SectionId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Section", t => t.SectionId)
                .Index(t => t.ProtocolId)
                .Index(t => t.SectionId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProtocolSectionProduct", "ProtocolSectionId", "dbo.ProtocolSection");
            DropForeignKey("dbo.ProtocolSection", "SectionId", "dbo.Section");
            DropForeignKey("dbo.ProtocolSection", "ProtocolId", "dbo.Protocol");
            DropForeignKey("dbo.ProtocolSectionProduct", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProtocolProduct", "ProtocolId", "dbo.Protocol");
            DropForeignKey("dbo.ProtocolProduct", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProtocolProductPack", "ProtocolId", "dbo.Protocol");
            DropForeignKey("dbo.ProtocolProductPack", "ProductPackId", "dbo.ProductPack");
            DropIndex("dbo.ProtocolSection", new[] { "Name" });
            DropIndex("dbo.ProtocolSection", new[] { "SectionId" });
            DropIndex("dbo.ProtocolSection", new[] { "ProtocolId" });
            DropIndex("dbo.ProtocolSectionProduct", new[] { "Name" });
            DropIndex("dbo.ProtocolSectionProduct", new[] { "ProductId" });
            DropIndex("dbo.ProtocolSectionProduct", new[] { "ProtocolSectionId" });
            DropIndex("dbo.ProtocolProduct", new[] { "Name" });
            DropIndex("dbo.ProtocolProduct", new[] { "ProductId" });
            DropIndex("dbo.ProtocolProduct", new[] { "ProtocolId" });
            DropIndex("dbo.ProtocolProductPack", new[] { "Name" });
            DropIndex("dbo.ProtocolProductPack", new[] { "ProductPackId" });
            DropIndex("dbo.ProtocolProductPack", new[] { "ProtocolId" });
            DropTable("dbo.ProtocolSection");
            DropTable("dbo.ProtocolSectionProduct");
            DropTable("dbo.ProtocolProduct");
            DropTable("dbo.ProtocolProductPack");
        }
    }
}

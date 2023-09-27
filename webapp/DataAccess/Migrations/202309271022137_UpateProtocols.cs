namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpateProtocols : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProtocolSection", newName: "Section");
            DropForeignKey("dbo.ProtocolProductPack", "ProductPackId", "dbo.ProductPack");
            DropForeignKey("dbo.ProtocolProductPack", "ProtocolId", "dbo.Protocol");
            DropForeignKey("dbo.ProtocolProduct", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProtocolProduct", "ProtocolId", "dbo.Protocol");
            DropForeignKey("dbo.ProtocolProtocolSectionProduct", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProtocolProtocolSection", "ProtocolId", "dbo.Protocol");
            DropForeignKey("dbo.ProtocolProtocolSection", "ProtocolSectionId", "dbo.ProtocolSection");
            DropForeignKey("dbo.ProtocolProtocolSectionProduct", "ProtocolProtocolSectionId", "dbo.ProtocolProtocolSection");
            DropIndex("dbo.ProtocolProductPack", new[] { "ProtocolId" });
            DropIndex("dbo.ProtocolProductPack", new[] { "ProductPackId" });
            DropIndex("dbo.ProtocolProductPack", new[] { "Name" });
            DropIndex("dbo.ProtocolProduct", new[] { "ProtocolId" });
            DropIndex("dbo.ProtocolProduct", new[] { "ProductId" });
            DropIndex("dbo.ProtocolProduct", new[] { "Name" });
            DropIndex("dbo.ProtocolProtocolSectionProduct", new[] { "ProtocolProtocolSectionId" });
            DropIndex("dbo.ProtocolProtocolSectionProduct", new[] { "ProductId" });
            DropIndex("dbo.ProtocolProtocolSectionProduct", new[] { "Name" });
            DropIndex("dbo.ProtocolProtocolSection", new[] { "ProtocolId" });
            DropIndex("dbo.ProtocolProtocolSection", new[] { "ProtocolSectionId" });
            DropIndex("dbo.ProtocolProtocolSection", new[] { "Name" });
            DropTable("dbo.ProtocolProductPack");
            DropTable("dbo.ProtocolProduct");
            DropTable("dbo.ProtocolProtocolSectionProduct");
            DropTable("dbo.ProtocolProtocolSection");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.ProtocolProtocolSection", "Name", unique: true);
            CreateIndex("dbo.ProtocolProtocolSection", "ProtocolSectionId");
            CreateIndex("dbo.ProtocolProtocolSection", "ProtocolId");
            CreateIndex("dbo.ProtocolProtocolSectionProduct", "Name", unique: true);
            CreateIndex("dbo.ProtocolProtocolSectionProduct", "ProductId");
            CreateIndex("dbo.ProtocolProtocolSectionProduct", "ProtocolProtocolSectionId");
            CreateIndex("dbo.ProtocolProduct", "Name", unique: true);
            CreateIndex("dbo.ProtocolProduct", "ProductId");
            CreateIndex("dbo.ProtocolProduct", "ProtocolId");
            CreateIndex("dbo.ProtocolProductPack", "Name", unique: true);
            CreateIndex("dbo.ProtocolProductPack", "ProductPackId");
            CreateIndex("dbo.ProtocolProductPack", "ProtocolId");
            AddForeignKey("dbo.ProtocolProtocolSectionProduct", "ProtocolProtocolSectionId", "dbo.ProtocolProtocolSection", "Id");
            AddForeignKey("dbo.ProtocolProtocolSection", "ProtocolSectionId", "dbo.ProtocolSection", "Id");
            AddForeignKey("dbo.ProtocolProtocolSection", "ProtocolId", "dbo.Protocol", "Id");
            AddForeignKey("dbo.ProtocolProtocolSectionProduct", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.ProtocolProduct", "ProtocolId", "dbo.Protocol", "Id");
            AddForeignKey("dbo.ProtocolProduct", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.ProtocolProductPack", "ProtocolId", "dbo.Protocol", "Id");
            AddForeignKey("dbo.ProtocolProductPack", "ProductPackId", "dbo.ProductPack", "Id");
            RenameTable(name: "dbo.Section", newName: "ProtocolSection");
        }
    }
}

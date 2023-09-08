namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductPacks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductPackProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProductPackId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
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
                .ForeignKey("dbo.ProductPack", t => t.ProductPackId)
                .Index(t => t.ProductId)
                .Index(t => t.ProductPackId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.ProductPack",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExternalId = c.Guid(nullable: false),
                        ContactId = c.Int(),
                        ShortDescription = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        Benefits = c.String(nullable: false),
                        Dosage = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        SeoFriendlyId = c.String(),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contact", t => t.ContactId)
                .Index(t => t.ContactId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductPackProduct", "ProductPackId", "dbo.ProductPack");
            DropForeignKey("dbo.ProductPack", "ContactId", "dbo.Contact");
            DropForeignKey("dbo.ProductPackProduct", "ProductId", "dbo.Product");
            DropIndex("dbo.ProductPack", new[] { "Name" });
            DropIndex("dbo.ProductPack", new[] { "ContactId" });
            DropIndex("dbo.ProductPackProduct", new[] { "Name" });
            DropIndex("dbo.ProductPackProduct", new[] { "ProductPackId" });
            DropIndex("dbo.ProductPackProduct", new[] { "ProductId" });
            DropTable("dbo.ProductPack");
            DropTable("dbo.ProductPackProduct");
        }
    }
}

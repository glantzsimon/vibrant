namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Products : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IngredientInventory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngredientId = c.Int(nullable: false),
                        QuantityInStock = c.Int(nullable: false),
                        StockLowWarningLevel = c.Int(nullable: false),
                        Notes = c.String(nullable: false),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingredient", t => t.IngredientId)
                .Index(t => t.IngredientId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Ingredient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngredientType = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 256),
                        ShortDescription = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        Benefits = c.String(nullable: false),
                        Research = c.String(nullable: false),
                        ImageUrl = c.String(maxLength: 512),
                        AdditionalCssClasses = c.String(),
                        SeoFriendlyId = c.String(),
                        VideoUrl = c.String(maxLength: 512),
                        PurchaseUrls = c.String(maxLength: 512),
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
                "dbo.ProductIngredient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        IngredientId = c.Int(nullable: false),
                        Amount = c.Single(nullable: false),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingredient", t => t.IngredientId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.IngredientId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductType = c.Int(nullable: false),
                        ContactId = c.Int(),
                        IsLiveOn = c.DateTime(nullable: false),
                        Title = c.String(nullable: false, maxLength: 256),
                        ShortDescription = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        Benefits = c.String(nullable: false),
                        Dosage = c.String(nullable: false),
                        ImageUrl = c.String(maxLength: 512),
                        AdditionalCssClasses = c.String(),
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
                .ForeignKey("dbo.Contact", t => t.ContactId)
                .Index(t => t.ContactId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.ProductInventory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(),
                        QuantityInStock = c.Int(nullable: false),
                        AmountPerProduct = c.Int(nullable: false),
                        Notes = c.String(nullable: false),
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
                .Index(t => t.ProductId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductInventory", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductIngredient", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "ContactId", "dbo.Contact");
            DropForeignKey("dbo.ProductIngredient", "IngredientId", "dbo.Ingredient");
            DropForeignKey("dbo.IngredientInventory", "IngredientId", "dbo.Ingredient");
            DropIndex("dbo.ProductInventory", new[] { "Name" });
            DropIndex("dbo.ProductInventory", new[] { "ProductId" });
            DropIndex("dbo.Product", new[] { "Name" });
            DropIndex("dbo.Product", new[] { "ContactId" });
            DropIndex("dbo.ProductIngredient", new[] { "Name" });
            DropIndex("dbo.ProductIngredient", new[] { "IngredientId" });
            DropIndex("dbo.ProductIngredient", new[] { "ProductId" });
            DropIndex("dbo.Ingredient", new[] { "Name" });
            DropIndex("dbo.IngredientInventory", new[] { "Name" });
            DropIndex("dbo.IngredientInventory", new[] { "IngredientId" });
            DropTable("dbo.ProductInventory");
            DropTable("dbo.Product");
            DropTable("dbo.ProductIngredient");
            DropTable("dbo.Ingredient");
            DropTable("dbo.IngredientInventory");
        }
    }
}

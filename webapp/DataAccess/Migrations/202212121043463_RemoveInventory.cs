namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveInventory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IngredientInventory", "IngredientId", "dbo.Ingredient");
            DropForeignKey("dbo.ProductInventory", "ProductId", "dbo.Product");
            DropIndex("dbo.IngredientInventory", new[] { "IngredientId" });
            DropIndex("dbo.IngredientInventory", new[] { "Name" });
            DropIndex("dbo.ProductInventory", new[] { "ProductId" });
            DropIndex("dbo.ProductInventory", new[] { "Name" });
            AddColumn("dbo.Product", "QuantityInStock", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "StockLowWarningLevel", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredient", "QuantityInStock", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredient", "StockLowWarningLevel", c => c.Int(nullable: false));
            DropTable("dbo.IngredientInventory");
            DropTable("dbo.ProductInventory");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Ingredient", "StockLowWarningLevel");
            DropColumn("dbo.Ingredient", "QuantityInStock");
            DropColumn("dbo.Product", "StockLowWarningLevel");
            DropColumn("dbo.Product", "QuantityInStock");
            CreateIndex("dbo.ProductInventory", "Name", unique: true);
            CreateIndex("dbo.ProductInventory", "ProductId");
            CreateIndex("dbo.IngredientInventory", "Name", unique: true);
            CreateIndex("dbo.IngredientInventory", "IngredientId");
            AddForeignKey("dbo.ProductInventory", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.IngredientInventory", "IngredientId", "dbo.Ingredient", "Id");
        }
    }
}

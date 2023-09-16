namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrders : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Sale", newName: "Order");
            DropForeignKey("dbo.SaleItem", "ProductId", "dbo.Product");
            DropForeignKey("dbo.SaleItem", "SaleId", "dbo.Sale");
            DropForeignKey("dbo.SaleItem", "UserId", "dbo.User");
            DropIndex("dbo.SaleItem", new[] { "SaleId" });
            DropIndex("dbo.SaleItem", new[] { "UserId" });
            DropIndex("dbo.SaleItem", new[] { "ProductId" });
            DropIndex("dbo.SaleItem", new[] { "Name" });
            DropIndex("dbo.Order", new[] { "ContactId" });
            CreateTable(
                "dbo.OrderItemProductPack",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductPackId = c.Int(nullable: false),
                        OrderItemId = c.Int(nullable: false),
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
                .ForeignKey("dbo.OrderItem", t => t.OrderItemId)
                .ForeignKey("dbo.ProductPack", t => t.ProductPackId)
                .Index(t => t.ProductPackId)
                .Index(t => t.OrderItemId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.OrderId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.OrderId)
                .Index(t => t.UserId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.OrderItemProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        OrderItemId = c.Int(nullable: false),
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
                .ForeignKey("dbo.OrderItem", t => t.OrderItemId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.OrderItemId)
                .Index(t => t.Name, unique: true);
            
            AddColumn("dbo.Order", "ExternalId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Order", "ContactId", c => c.Int());
            CreateIndex("dbo.Order", "ContactId");
            DropTable("dbo.SaleItem");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SaleItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SaleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.OrderItemProduct", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderItemProduct", "OrderItemId", "dbo.OrderItem");
            DropForeignKey("dbo.OrderItemProductPack", "ProductPackId", "dbo.ProductPack");
            DropForeignKey("dbo.OrderItemProductPack", "OrderItemId", "dbo.OrderItem");
            DropForeignKey("dbo.OrderItem", "UserId", "dbo.User");
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropIndex("dbo.OrderItemProduct", new[] { "Name" });
            DropIndex("dbo.OrderItemProduct", new[] { "OrderItemId" });
            DropIndex("dbo.OrderItemProduct", new[] { "ProductId" });
            DropIndex("dbo.Order", new[] { "ContactId" });
            DropIndex("dbo.OrderItem", new[] { "Name" });
            DropIndex("dbo.OrderItem", new[] { "UserId" });
            DropIndex("dbo.OrderItem", new[] { "OrderId" });
            DropIndex("dbo.OrderItemProductPack", new[] { "Name" });
            DropIndex("dbo.OrderItemProductPack", new[] { "OrderItemId" });
            DropIndex("dbo.OrderItemProductPack", new[] { "ProductPackId" });
            AlterColumn("dbo.Order", "ContactId", c => c.Int(nullable: false));
            DropColumn("dbo.Order", "ExternalId");
            DropTable("dbo.OrderItemProduct");
            DropTable("dbo.OrderItem");
            DropTable("dbo.OrderItemProductPack");
            CreateIndex("dbo.Order", "ContactId");
            CreateIndex("dbo.SaleItem", "Name", unique: true);
            CreateIndex("dbo.SaleItem", "ProductId");
            CreateIndex("dbo.SaleItem", "UserId");
            CreateIndex("dbo.SaleItem", "SaleId");
            AddForeignKey("dbo.SaleItem", "UserId", "dbo.User", "Id");
            AddForeignKey("dbo.SaleItem", "SaleId", "dbo.Sale", "Id");
            AddForeignKey("dbo.SaleItem", "ProductId", "dbo.Product", "Id");
            RenameTable(name: "dbo.Order", newName: "Sale");
        }
    }
}

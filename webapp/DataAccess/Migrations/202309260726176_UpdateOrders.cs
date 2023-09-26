namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateOrders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderItem", "OrderId", "dbo.Order");
            DropForeignKey("dbo.OrderItem", "UserId", "dbo.User");
            DropForeignKey("dbo.OrderItemProductPack", "OrderItemId", "dbo.OrderItem");
            DropForeignKey("dbo.OrderItemProductPack", "ProductPackId", "dbo.ProductPack");
            DropForeignKey("dbo.OrderItemProduct", "OrderItemId", "dbo.OrderItem");
            DropForeignKey("dbo.OrderItemProduct", "ProductId", "dbo.Product");
            DropIndex("dbo.OrderItemProductPack", new[] { "ProductPackId" });
            DropIndex("dbo.OrderItemProductPack", new[] { "OrderItemId" });
            DropIndex("dbo.OrderItemProductPack", new[] { "Name" });
            DropIndex("dbo.OrderItem", new[] { "OrderId" });
            DropIndex("dbo.OrderItem", new[] { "UserId" });
            DropIndex("dbo.OrderItem", new[] { "Name" });
            DropIndex("dbo.OrderItemProduct", new[] { "ProductId" });
            DropIndex("dbo.OrderItemProduct", new[] { "OrderItemId" });
            DropIndex("dbo.OrderItemProduct", new[] { "Name" });
            CreateTable(
                "dbo.OrderProductPack",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriceTier = c.Int(nullable: false),
                        ProductPackId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Order", t => t.OrderId)
                .ForeignKey("dbo.ProductPack", t => t.ProductPackId)
                .Index(t => t.ProductPackId)
                .Index(t => t.OrderId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.OrderProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PriceTier = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Order", t => t.OrderId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId)
                .Index(t => t.OrderId)
                .Index(t => t.Name, unique: true);
            
            AddColumn("dbo.Order", "Discount", c => c.Double());
            DropTable("dbo.OrderItemProductPack");
            DropTable("dbo.OrderItem");
            DropTable("dbo.OrderItemProduct");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrderItemProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        OrderItemId = c.Int(nullable: false),
                        Amount = c.Single(nullable: false),
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
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderItemType = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
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
                "dbo.OrderItemProductPack",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductPackId = c.Int(nullable: false),
                        OrderItemId = c.Int(nullable: false),
                        Amount = c.Single(nullable: false),
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
            
            DropForeignKey("dbo.OrderProduct", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderProduct", "OrderId", "dbo.Order");
            DropForeignKey("dbo.OrderProductPack", "ProductPackId", "dbo.ProductPack");
            DropForeignKey("dbo.OrderProductPack", "OrderId", "dbo.Order");
            DropIndex("dbo.OrderProduct", new[] { "Name" });
            DropIndex("dbo.OrderProduct", new[] { "OrderId" });
            DropIndex("dbo.OrderProduct", new[] { "ProductId" });
            DropIndex("dbo.OrderProductPack", new[] { "Name" });
            DropIndex("dbo.OrderProductPack", new[] { "OrderId" });
            DropIndex("dbo.OrderProductPack", new[] { "ProductPackId" });
            DropColumn("dbo.Order", "Discount");
            DropTable("dbo.OrderProduct");
            DropTable("dbo.OrderProductPack");
            CreateIndex("dbo.OrderItemProduct", "Name", unique: true);
            CreateIndex("dbo.OrderItemProduct", "OrderItemId");
            CreateIndex("dbo.OrderItemProduct", "ProductId");
            CreateIndex("dbo.OrderItem", "Name", unique: true);
            CreateIndex("dbo.OrderItem", "UserId");
            CreateIndex("dbo.OrderItem", "OrderId");
            CreateIndex("dbo.OrderItemProductPack", "Name", unique: true);
            CreateIndex("dbo.OrderItemProductPack", "OrderItemId");
            CreateIndex("dbo.OrderItemProductPack", "ProductPackId");
            AddForeignKey("dbo.OrderItemProduct", "ProductId", "dbo.Product", "Id");
            AddForeignKey("dbo.OrderItemProduct", "OrderItemId", "dbo.OrderItem", "Id");
            AddForeignKey("dbo.OrderItemProductPack", "ProductPackId", "dbo.ProductPack", "Id");
            AddForeignKey("dbo.OrderItemProductPack", "OrderItemId", "dbo.OrderItem", "Id");
            AddForeignKey("dbo.OrderItem", "UserId", "dbo.User", "Id");
            AddForeignKey("dbo.OrderItem", "OrderId", "dbo.Order", "Id");
        }
    }
}

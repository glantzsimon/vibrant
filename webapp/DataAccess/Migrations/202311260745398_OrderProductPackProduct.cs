namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderProductPackProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderProductPackProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderProductPackId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        AmountCompleted = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderProductPack", t => t.OrderProductPackId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.OrderProductPackId)
                .Index(t => t.ProductId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderProductPackProduct", "ProductId", "dbo.Product");
            DropForeignKey("dbo.OrderProductPackProduct", "OrderProductPackId", "dbo.OrderProductPack");
            DropIndex("dbo.OrderProductPackProduct", new[] { "Name" });
            DropIndex("dbo.OrderProductPackProduct", new[] { "ProductId" });
            DropIndex("dbo.OrderProductPackProduct", new[] { "OrderProductPackId" });
            DropTable("dbo.OrderProductPackProduct");
        }
    }
}

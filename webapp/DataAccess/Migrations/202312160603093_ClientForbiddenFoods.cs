namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientForbiddenFoods : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientForbiddenFood",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        FoodItemId = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Client", t => t.ClientId)
                .ForeignKey("dbo.FoodItem", t => t.FoodItemId)
                .Index(t => t.ClientId)
                .Index(t => t.FoodItemId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientForbiddenFood", "FoodItemId", "dbo.FoodItem");
            DropForeignKey("dbo.ClientForbiddenFood", "ClientId", "dbo.Client");
            DropIndex("dbo.ClientForbiddenFood", new[] { "Name" });
            DropIndex("dbo.ClientForbiddenFood", new[] { "FoodItemId" });
            DropIndex("dbo.ClientForbiddenFood", new[] { "ClientId" });
            DropTable("dbo.ClientForbiddenFood");
        }
    }
}

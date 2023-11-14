namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LatestUpdates1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProtocolFoodItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProtocolId = c.Int(nullable: false),
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
                .ForeignKey("dbo.FoodItem", t => t.FoodItemId)
                .ForeignKey("dbo.Protocol", t => t.ProtocolId)
                .Index(t => t.ProtocolId)
                .Index(t => t.FoodItemId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProtocolFoodItem", "ProtocolId", "dbo.Protocol");
            DropForeignKey("dbo.ProtocolFoodItem", "FoodItemId", "dbo.FoodItem");
            DropIndex("dbo.ProtocolFoodItem", new[] { "Name" });
            DropIndex("dbo.ProtocolFoodItem", new[] { "FoodItemId" });
            DropIndex("dbo.ProtocolFoodItem", new[] { "ProtocolId" });
            DropTable("dbo.ProtocolFoodItem");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Latest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductIngredientSubstitute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductIngredientId = c.Int(nullable: false),
                        SubstituteIngredientId = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductIngredient", t => t.ProductIngredientId)
                .ForeignKey("dbo.Ingredient", t => t.SubstituteIngredientId)
                .Index(t => t.ProductIngredientId)
                .Index(t => t.SubstituteIngredientId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductIngredientSubstitute", "SubstituteIngredientId", "dbo.Ingredient");
            DropForeignKey("dbo.ProductIngredientSubstitute", "ProductIngredientId", "dbo.ProductIngredient");
            DropIndex("dbo.ProductIngredientSubstitute", new[] { "Name" });
            DropIndex("dbo.ProductIngredientSubstitute", new[] { "SubstituteIngredientId" });
            DropIndex("dbo.ProductIngredientSubstitute", new[] { "ProductIngredientId" });
            DropTable("dbo.ProductIngredientSubstitute");
        }
    }
}

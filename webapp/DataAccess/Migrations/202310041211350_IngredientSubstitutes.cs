namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IngredientSubstitutes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IngredientSubstitute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngredientId = c.Int(nullable: false),
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
                .ForeignKey("dbo.Ingredient", t => t.IngredientId)
                .ForeignKey("dbo.Ingredient", t => t.SubstituteIngredientId)
                .Index(t => t.IngredientId)
                .Index(t => t.SubstituteIngredientId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IngredientSubstitute", "SubstituteIngredientId", "dbo.Ingredient");
            DropForeignKey("dbo.IngredientSubstitute", "IngredientId", "dbo.Ingredient");
            DropIndex("dbo.IngredientSubstitute", new[] { "Name" });
            DropIndex("dbo.IngredientSubstitute", new[] { "SubstituteIngredientId" });
            DropIndex("dbo.IngredientSubstitute", new[] { "IngredientId" });
            DropTable("dbo.IngredientSubstitute");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IngredientIsHygroscopic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ingredient", "IsHydroscopic", c => c.Boolean(nullable: false));
            DropColumn("dbo.Product", "IsHydroscopic");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "IsHydroscopic", c => c.Boolean(nullable: false));
            DropColumn("dbo.Ingredient", "IsHydroscopic");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IngredientConcentration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ingredient", "Concentration", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ingredient", "Concentration");
        }
    }
}

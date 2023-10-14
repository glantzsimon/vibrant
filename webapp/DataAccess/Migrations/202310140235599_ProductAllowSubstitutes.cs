namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductAllowSubstitutes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "AllowIngredientSubstitutes", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "AllowIngredientSubstitutes");
        }
    }
}

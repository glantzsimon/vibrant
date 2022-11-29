namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveIngredientTitle : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ingredient", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ingredient", "Title", c => c.String(nullable: false, maxLength: 256));
        }
    }
}

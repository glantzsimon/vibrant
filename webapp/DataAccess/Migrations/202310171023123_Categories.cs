namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Categories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Category", c => c.Int(nullable: false));
            AddColumn("dbo.Product", "ItemCode", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredient", "Category", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredient", "ItemCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ingredient", "ItemCode");
            DropColumn("dbo.Ingredient", "Category");
            DropColumn("dbo.Product", "ItemCode");
            DropColumn("dbo.Product", "Category");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FoodItemRemoveRequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FoodItem", "ShortDescription", c => c.String());
            AlterColumn("dbo.FoodItem", "Benefits", c => c.String());
            AlterColumn("dbo.FoodItem", "Recommendations", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FoodItem", "Recommendations", c => c.String(nullable: false));
            AlterColumn("dbo.FoodItem", "Benefits", c => c.String(nullable: false));
            AlterColumn("dbo.FoodItem", "ShortDescription", c => c.String(nullable: false));
        }
    }
}

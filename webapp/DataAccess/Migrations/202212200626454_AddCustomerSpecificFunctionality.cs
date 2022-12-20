namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomerSpecificFunctionality : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ingredient", "IsHidden", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ingredient", "IsHidden");
        }
    }
}

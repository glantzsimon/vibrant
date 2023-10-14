namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductIngredientSubstitutesFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductIngredient", "NumberOfSubstitutesToUse", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductIngredient", "NumberOfSubstitutesToUse");
        }
    }
}

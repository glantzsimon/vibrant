namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIngredients : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ingredient", "SubjectiveEffects", c => c.String());
            AddColumn("dbo.Ingredient", "SideEffects", c => c.String());
            AddColumn("dbo.Ingredient", "DeficiencySymptoms", c => c.String());
            AlterColumn("dbo.Ingredient", "Research", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ingredient", "Research", c => c.String(nullable: false));
            DropColumn("dbo.Ingredient", "DeficiencySymptoms");
            DropColumn("dbo.Ingredient", "SideEffects");
            DropColumn("dbo.Ingredient", "SubjectiveEffects");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMorerequiredFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ingredient", "Body", c => c.String());
            AlterColumn("dbo.Ingredient", "Benefits", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ingredient", "Benefits", c => c.String(nullable: false));
            AlterColumn("dbo.Ingredient", "Body", c => c.String(nullable: false));
        }
    }
}

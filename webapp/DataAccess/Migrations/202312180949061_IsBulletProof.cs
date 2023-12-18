namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsBulletProof : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItem", "IsBulletProof", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodItem", "IsBulletProof");
        }
    }
}

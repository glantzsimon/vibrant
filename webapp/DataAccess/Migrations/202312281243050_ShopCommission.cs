namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShopCommission : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "ShopCommission", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "ShopCommission");
        }
    }
}

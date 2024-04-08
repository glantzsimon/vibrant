namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditConsultantOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Client", "ShopCommission", c => c.Double());
            DropColumn("dbo.Order", "TotalPaid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "TotalPaid", c => c.Double(nullable: false));
            DropColumn("dbo.Client", "ShopCommission");
        }
    }
}

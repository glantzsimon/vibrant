namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewProductFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "SubTitleLabelText", c => c.String(nullable: true));
            AddColumn("dbo.Product", "MaxDosage", c => c.Int(nullable: false, defaultValue: 1));
            AddColumn("dbo.Product", "MinDosage", c => c.Int(nullable: false, defaultValue: 1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "MinDosage");
            DropColumn("dbo.Product", "MaxDosage");
            DropColumn("dbo.Product", "SubTitleLabelText");
        }
    }
}

namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLGBTQPlus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HealthQuestionnaire", "IsIsLGBTQPlus", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "IsIsLGBTQPlus");
        }
    }
}

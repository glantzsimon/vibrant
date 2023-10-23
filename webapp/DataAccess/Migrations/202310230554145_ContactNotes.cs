namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContactNotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contact", "Notes", c => c.String(maxLength: 243));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contact", "Notes");
        }
    }
}

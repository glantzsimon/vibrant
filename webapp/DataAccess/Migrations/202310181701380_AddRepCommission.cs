namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRepCommission : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RepCommission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RepId = c.Int(nullable: false),
                        AmountRedeemed = c.Double(nullable: false),
                        RedeemedOn = c.DateTime(nullable: false),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contact", t => t.RepId)
                .Index(t => t.RepId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RepCommission", "RepId", "dbo.Contact");
            DropIndex("dbo.RepCommission", new[] { "Name" });
            DropIndex("dbo.RepCommission", new[] { "RepId" });
            DropTable("dbo.RepCommission");
        }
    }
}

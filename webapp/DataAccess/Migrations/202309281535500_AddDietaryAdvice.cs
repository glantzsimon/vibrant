namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDietaryAdvice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DietaryRecommendation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExternalId = c.Guid(nullable: false),
                        ShortDescription = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        Benefits = c.String(nullable: false),
                        Recommendations = c.String(nullable: false),
                        ImageUrl = c.String(maxLength: 512),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.ProtocolDietaryRecommendation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProtocolId = c.Int(nullable: false),
                        DietaryRecommendationId = c.Int(nullable: false),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DietaryRecommendation", t => t.DietaryRecommendationId)
                .ForeignKey("dbo.Protocol", t => t.ProtocolId)
                .Index(t => t.ProtocolId)
                .Index(t => t.DietaryRecommendationId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProtocolDietaryRecommendation", "ProtocolId", "dbo.Protocol");
            DropForeignKey("dbo.ProtocolDietaryRecommendation", "DietaryRecommendationId", "dbo.DietaryRecommendation");
            DropIndex("dbo.ProtocolDietaryRecommendation", new[] { "Name" });
            DropIndex("dbo.ProtocolDietaryRecommendation", new[] { "DietaryRecommendationId" });
            DropIndex("dbo.ProtocolDietaryRecommendation", new[] { "ProtocolId" });
            DropIndex("dbo.DietaryRecommendation", new[] { "Name" });
            DropTable("dbo.ProtocolDietaryRecommendation");
            DropTable("dbo.DietaryRecommendation");
        }
    }
}

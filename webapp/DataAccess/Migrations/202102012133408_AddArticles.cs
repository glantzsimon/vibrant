namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArticles : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArticleCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                "dbo.Article",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ArticleCategoryId = c.Int(nullable: false),
                        PublishedOn = c.DateTime(nullable: false),
                        PublishedBy = c.String(),
                        Subject = c.String(nullable: false, maxLength: 256),
                        Body = c.String(nullable: false),
                        ImageUrl = c.String(maxLength: 512),
                        AdditionalCssClasses = c.String(),
                        SeoFriendlyId = c.String(),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArticleCategory", t => t.ArticleCategoryId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ArticleCategoryId)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.ArticleSection",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticleId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 256),
                        Body = c.String(nullable: false),
                        Order = c.Int(),
                        ImageUrl = c.String(maxLength: 512),
                        AdditionalCssClasses = c.String(),
                        Name = c.String(maxLength: 128),
                        IsSystemStandard = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.String(maxLength: 255),
                        CreatedOn = c.DateTime(),
                        LastUpdatedBy = c.String(maxLength: 255),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Article", t => t.ArticleId)
                .Index(t => t.ArticleId)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArticleSection", "ArticleId", "dbo.Article");
            DropForeignKey("dbo.Article", "UserId", "dbo.User");
            DropForeignKey("dbo.Article", "ArticleCategoryId", "dbo.ArticleCategory");
            DropIndex("dbo.ArticleSection", new[] { "Name" });
            DropIndex("dbo.ArticleSection", new[] { "ArticleId" });
            DropIndex("dbo.Article", new[] { "Name" });
            DropIndex("dbo.Article", new[] { "ArticleCategoryId" });
            DropIndex("dbo.Article", new[] { "UserId" });
            DropIndex("dbo.ArticleCategory", new[] { "Name" });
            DropTable("dbo.ArticleSection");
            DropTable("dbo.Article");
            DropTable("dbo.ArticleCategory");
        }
    }
}

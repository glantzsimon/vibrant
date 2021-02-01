namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVideoUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Article", "VideoUrl", c => c.String(maxLength: 512));
            AddColumn("dbo.ArticleSection", "VideoUrl", c => c.String(maxLength: 512));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ArticleSection", "VideoUrl");
            DropColumn("dbo.Article", "VideoUrl");
        }
    }
}

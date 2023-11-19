namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FoodItemClassificationColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItem", "IsWhite", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsBeige", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsBlue", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsGreen", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsYellow", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsOrange", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsRed", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsPurple", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsBrown", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsBlack", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "CanBeEatenRaw", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsCitrusFruit", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsLowCarb", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsNightshade", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "PlantPart", c => c.Int(nullable: false));
            AddColumn("dbo.FoodItem", "IsBitter", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsSweet", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsPungent", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsSalty", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsSour", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsAstringent", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsSpring", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsSummer", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsLateSummer", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsAutumn", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsWinter", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsVataDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsPittaDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsKaphaDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsWaterElement", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsTreeElement", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsFireElement", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsEarthlElement", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsMetalElement", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodItem", "IsMetalElement");
            DropColumn("dbo.FoodItem", "IsEarthlElement");
            DropColumn("dbo.FoodItem", "IsFireElement");
            DropColumn("dbo.FoodItem", "IsTreeElement");
            DropColumn("dbo.FoodItem", "IsWaterElement");
            DropColumn("dbo.FoodItem", "IsKaphaDosha");
            DropColumn("dbo.FoodItem", "IsPittaDosha");
            DropColumn("dbo.FoodItem", "IsVataDosha");
            DropColumn("dbo.FoodItem", "IsWinter");
            DropColumn("dbo.FoodItem", "IsAutumn");
            DropColumn("dbo.FoodItem", "IsLateSummer");
            DropColumn("dbo.FoodItem", "IsSummer");
            DropColumn("dbo.FoodItem", "IsSpring");
            DropColumn("dbo.FoodItem", "IsAstringent");
            DropColumn("dbo.FoodItem", "IsSour");
            DropColumn("dbo.FoodItem", "IsSalty");
            DropColumn("dbo.FoodItem", "IsPungent");
            DropColumn("dbo.FoodItem", "IsSweet");
            DropColumn("dbo.FoodItem", "IsBitter");
            DropColumn("dbo.FoodItem", "PlantPart");
            DropColumn("dbo.FoodItem", "IsNightshade");
            DropColumn("dbo.FoodItem", "IsLowCarb");
            DropColumn("dbo.FoodItem", "IsCitrusFruit");
            DropColumn("dbo.FoodItem", "CanBeEatenRaw");
            DropColumn("dbo.FoodItem", "IsBlack");
            DropColumn("dbo.FoodItem", "IsBrown");
            DropColumn("dbo.FoodItem", "IsPurple");
            DropColumn("dbo.FoodItem", "IsRed");
            DropColumn("dbo.FoodItem", "IsOrange");
            DropColumn("dbo.FoodItem", "IsYellow");
            DropColumn("dbo.FoodItem", "IsGreen");
            DropColumn("dbo.FoodItem", "IsBlue");
            DropColumn("dbo.FoodItem", "IsBeige");
            DropColumn("dbo.FoodItem", "IsWhite");
        }
    }
}

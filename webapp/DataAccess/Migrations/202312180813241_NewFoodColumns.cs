namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFoodColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodItem", "IsHighOxalate", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsHighLectin", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsHighPhytate", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsHighHistamine", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsHighMycotoxin", c => c.Boolean(nullable: false));
            AddColumn("dbo.FoodItem", "IsHighOmega6", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "EatsRedMeat", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "EatsPoultry", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "EatsFishAndSeafood", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "EatsEggsAndRoes", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "EatsDairy", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "EatsVegetables", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "EatsVegetableProtein", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "EatsFungi", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "EatsFruit", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "IsLowOxalate", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "IsLowLectin", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "IsLowPhytate", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "IsLowHistamine", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "IsLowMycotoxin", c => c.Boolean(nullable: false));
            AddColumn("dbo.HealthQuestionnaire", "IsLowOmega6", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HealthQuestionnaire", "IsLowOmega6");
            DropColumn("dbo.HealthQuestionnaire", "IsLowMycotoxin");
            DropColumn("dbo.HealthQuestionnaire", "IsLowHistamine");
            DropColumn("dbo.HealthQuestionnaire", "IsLowPhytate");
            DropColumn("dbo.HealthQuestionnaire", "IsLowLectin");
            DropColumn("dbo.HealthQuestionnaire", "IsLowOxalate");
            DropColumn("dbo.HealthQuestionnaire", "EatsFruit");
            DropColumn("dbo.HealthQuestionnaire", "EatsFungi");
            DropColumn("dbo.HealthQuestionnaire", "EatsVegetableProtein");
            DropColumn("dbo.HealthQuestionnaire", "EatsVegetables");
            DropColumn("dbo.HealthQuestionnaire", "EatsDairy");
            DropColumn("dbo.HealthQuestionnaire", "EatsEggsAndRoes");
            DropColumn("dbo.HealthQuestionnaire", "EatsFishAndSeafood");
            DropColumn("dbo.HealthQuestionnaire", "EatsPoultry");
            DropColumn("dbo.HealthQuestionnaire", "EatsRedMeat");
            DropColumn("dbo.FoodItem", "IsHighOmega6");
            DropColumn("dbo.FoodItem", "IsHighMycotoxin");
            DropColumn("dbo.FoodItem", "IsHighHistamine");
            DropColumn("dbo.FoodItem", "IsHighPhytate");
            DropColumn("dbo.FoodItem", "IsHighLectin");
            DropColumn("dbo.FoodItem", "IsHighOxalate");
        }
    }
}

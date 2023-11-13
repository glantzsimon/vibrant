namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Magic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activity", "VataDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "PittaDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "KaphaDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Hunter", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Gatherer", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Teacher", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Explorer", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Warrior", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Nomad", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Water", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Earth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Tree", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Metal", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Fire", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Cbs", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Immunity", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "NeurologicalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "DigestiveHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "CardioVascularHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "UrologicalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "HormoneBalance", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "StressRelief", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "Detoxification", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "NourishesYin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Activity", "NourishesYang", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "VataDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "PittaDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "KaphaDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Hunter", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Gatherer", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Teacher", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Explorer", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Warrior", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Nomad", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Water", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Earth", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Tree", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Metal", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Fire", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Cbs", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Immunity", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "NeurologicalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "DigestiveHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "CardioVascularHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "UrologicalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "HormoneBalance", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "StressRelief", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "Detoxification", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "NourishesYin", c => c.Boolean(nullable: false));
            AddColumn("dbo.DietaryRecommendation", "NourishesYang", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DietaryRecommendation", "NourishesYang");
            DropColumn("dbo.DietaryRecommendation", "NourishesYin");
            DropColumn("dbo.DietaryRecommendation", "Detoxification");
            DropColumn("dbo.DietaryRecommendation", "StressRelief");
            DropColumn("dbo.DietaryRecommendation", "HormoneBalance");
            DropColumn("dbo.DietaryRecommendation", "UrologicalHealth");
            DropColumn("dbo.DietaryRecommendation", "CardioVascularHealth");
            DropColumn("dbo.DietaryRecommendation", "DigestiveHealth");
            DropColumn("dbo.DietaryRecommendation", "NeurologicalHealth");
            DropColumn("dbo.DietaryRecommendation", "Immunity");
            DropColumn("dbo.DietaryRecommendation", "Cbs");
            DropColumn("dbo.DietaryRecommendation", "Fire");
            DropColumn("dbo.DietaryRecommendation", "Metal");
            DropColumn("dbo.DietaryRecommendation", "Tree");
            DropColumn("dbo.DietaryRecommendation", "Earth");
            DropColumn("dbo.DietaryRecommendation", "Water");
            DropColumn("dbo.DietaryRecommendation", "Nomad");
            DropColumn("dbo.DietaryRecommendation", "Warrior");
            DropColumn("dbo.DietaryRecommendation", "Explorer");
            DropColumn("dbo.DietaryRecommendation", "Teacher");
            DropColumn("dbo.DietaryRecommendation", "Gatherer");
            DropColumn("dbo.DietaryRecommendation", "Hunter");
            DropColumn("dbo.DietaryRecommendation", "KaphaDosha");
            DropColumn("dbo.DietaryRecommendation", "PittaDosha");
            DropColumn("dbo.DietaryRecommendation", "VataDosha");
            DropColumn("dbo.Activity", "NourishesYang");
            DropColumn("dbo.Activity", "NourishesYin");
            DropColumn("dbo.Activity", "Detoxification");
            DropColumn("dbo.Activity", "StressRelief");
            DropColumn("dbo.Activity", "HormoneBalance");
            DropColumn("dbo.Activity", "UrologicalHealth");
            DropColumn("dbo.Activity", "CardioVascularHealth");
            DropColumn("dbo.Activity", "DigestiveHealth");
            DropColumn("dbo.Activity", "NeurologicalHealth");
            DropColumn("dbo.Activity", "Immunity");
            DropColumn("dbo.Activity", "Cbs");
            DropColumn("dbo.Activity", "Fire");
            DropColumn("dbo.Activity", "Metal");
            DropColumn("dbo.Activity", "Tree");
            DropColumn("dbo.Activity", "Earth");
            DropColumn("dbo.Activity", "Water");
            DropColumn("dbo.Activity", "Nomad");
            DropColumn("dbo.Activity", "Warrior");
            DropColumn("dbo.Activity", "Explorer");
            DropColumn("dbo.Activity", "Teacher");
            DropColumn("dbo.Activity", "Gatherer");
            DropColumn("dbo.Activity", "Hunter");
            DropColumn("dbo.Activity", "KaphaDosha");
            DropColumn("dbo.Activity", "PittaDosha");
            DropColumn("dbo.Activity", "VataDosha");
        }
    }
}

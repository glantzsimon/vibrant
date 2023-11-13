namespace K9.DataAccessLayer.Database
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenoTypeColumns : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FoodItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExternalId = c.Guid(nullable: false),
                        Category = c.Int(nullable: false),
                        ShortDescription = c.String(nullable: false),
                        Benefits = c.String(nullable: false),
                        Recommendations = c.String(nullable: false),
                        VataDosha = c.Boolean(nullable: false),
                        PittaDosha = c.Boolean(nullable: false),
                        KaphaDosha = c.Boolean(nullable: false),
                        Hunter = c.Boolean(nullable: false),
                        Gatherer = c.Boolean(nullable: false),
                        Teacher = c.Boolean(nullable: false),
                        Explorer = c.Boolean(nullable: false),
                        Warrior = c.Boolean(nullable: false),
                        Nomad = c.Boolean(nullable: false),
                        Water = c.Boolean(nullable: false),
                        Earth = c.Boolean(nullable: false),
                        Tree = c.Boolean(nullable: false),
                        Metal = c.Boolean(nullable: false),
                        Fire = c.Boolean(nullable: false),
                        Cbs = c.Boolean(nullable: false),
                        Immunity = c.Boolean(nullable: false),
                        NeurologicalHealth = c.Boolean(nullable: false),
                        DigestiveHealth = c.Boolean(nullable: false),
                        CardioVascularHealth = c.Boolean(nullable: false),
                        UrologicalHealth = c.Boolean(nullable: false),
                        HormoneBalance = c.Boolean(nullable: false),
                        StressRelief = c.Boolean(nullable: false),
                        Detoxification = c.Boolean(nullable: false),
                        DentalHealth = c.Boolean(nullable: false),
                        NourishesYin = c.Boolean(nullable: false),
                        NourishesYang = c.Boolean(nullable: false),
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
            
            AddColumn("dbo.Product", "VataDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "PittaDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "KaphaDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Hunter", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Gatherer", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Teacher", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Explorer", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Warrior", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Nomad", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Water", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Earth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Tree", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Metal", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Fire", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Cbs", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Immunity", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "NeurologicalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "DigestiveHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "CardioVascularHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "UrologicalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "HormoneBalance", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "StressRelief", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "Detoxification", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "DentalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "NourishesYin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "NourishesYang", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "VataDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "PittaDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "KaphaDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Hunter", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Gatherer", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Teacher", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Explorer", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Warrior", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Nomad", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Water", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Earth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Tree", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Metal", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Fire", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Cbs", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Immunity", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "NeurologicalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "DigestiveHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "CardioVascularHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "UrologicalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "HormoneBalance", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "StressRelief", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "Detoxification", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "DentalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "NourishesYin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Ingredient", "NourishesYang", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "VataDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "PittaDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "KaphaDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Hunter", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Gatherer", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Teacher", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Explorer", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Warrior", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Nomad", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Water", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Earth", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Tree", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Metal", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Fire", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Cbs", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Immunity", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "NeurologicalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "DigestiveHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "CardioVascularHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "UrologicalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "HormoneBalance", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "StressRelief", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "Detoxification", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "DentalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "NourishesYin", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductPack", "NourishesYang", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "VataDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "PittaDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "KaphaDosha", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Hunter", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Gatherer", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Teacher", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Explorer", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Warrior", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Nomad", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Water", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Earth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Tree", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Metal", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Fire", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Cbs", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Immunity", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "NeurologicalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "DigestiveHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "CardioVascularHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "UrologicalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "HormoneBalance", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "StressRelief", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "Detoxification", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "DentalHealth", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "NourishesYin", c => c.Boolean(nullable: false));
            AddColumn("dbo.Protocol", "NourishesYang", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropIndex("dbo.FoodItem", new[] { "Name" });
            DropColumn("dbo.Protocol", "NourishesYang");
            DropColumn("dbo.Protocol", "NourishesYin");
            DropColumn("dbo.Protocol", "DentalHealth");
            DropColumn("dbo.Protocol", "Detoxification");
            DropColumn("dbo.Protocol", "StressRelief");
            DropColumn("dbo.Protocol", "HormoneBalance");
            DropColumn("dbo.Protocol", "UrologicalHealth");
            DropColumn("dbo.Protocol", "CardioVascularHealth");
            DropColumn("dbo.Protocol", "DigestiveHealth");
            DropColumn("dbo.Protocol", "NeurologicalHealth");
            DropColumn("dbo.Protocol", "Immunity");
            DropColumn("dbo.Protocol", "Cbs");
            DropColumn("dbo.Protocol", "Fire");
            DropColumn("dbo.Protocol", "Metal");
            DropColumn("dbo.Protocol", "Tree");
            DropColumn("dbo.Protocol", "Earth");
            DropColumn("dbo.Protocol", "Water");
            DropColumn("dbo.Protocol", "Nomad");
            DropColumn("dbo.Protocol", "Warrior");
            DropColumn("dbo.Protocol", "Explorer");
            DropColumn("dbo.Protocol", "Teacher");
            DropColumn("dbo.Protocol", "Gatherer");
            DropColumn("dbo.Protocol", "Hunter");
            DropColumn("dbo.Protocol", "KaphaDosha");
            DropColumn("dbo.Protocol", "PittaDosha");
            DropColumn("dbo.Protocol", "VataDosha");
            DropColumn("dbo.ProductPack", "NourishesYang");
            DropColumn("dbo.ProductPack", "NourishesYin");
            DropColumn("dbo.ProductPack", "DentalHealth");
            DropColumn("dbo.ProductPack", "Detoxification");
            DropColumn("dbo.ProductPack", "StressRelief");
            DropColumn("dbo.ProductPack", "HormoneBalance");
            DropColumn("dbo.ProductPack", "UrologicalHealth");
            DropColumn("dbo.ProductPack", "CardioVascularHealth");
            DropColumn("dbo.ProductPack", "DigestiveHealth");
            DropColumn("dbo.ProductPack", "NeurologicalHealth");
            DropColumn("dbo.ProductPack", "Immunity");
            DropColumn("dbo.ProductPack", "Cbs");
            DropColumn("dbo.ProductPack", "Fire");
            DropColumn("dbo.ProductPack", "Metal");
            DropColumn("dbo.ProductPack", "Tree");
            DropColumn("dbo.ProductPack", "Earth");
            DropColumn("dbo.ProductPack", "Water");
            DropColumn("dbo.ProductPack", "Nomad");
            DropColumn("dbo.ProductPack", "Warrior");
            DropColumn("dbo.ProductPack", "Explorer");
            DropColumn("dbo.ProductPack", "Teacher");
            DropColumn("dbo.ProductPack", "Gatherer");
            DropColumn("dbo.ProductPack", "Hunter");
            DropColumn("dbo.ProductPack", "KaphaDosha");
            DropColumn("dbo.ProductPack", "PittaDosha");
            DropColumn("dbo.ProductPack", "VataDosha");
            DropColumn("dbo.Ingredient", "NourishesYang");
            DropColumn("dbo.Ingredient", "NourishesYin");
            DropColumn("dbo.Ingredient", "DentalHealth");
            DropColumn("dbo.Ingredient", "Detoxification");
            DropColumn("dbo.Ingredient", "StressRelief");
            DropColumn("dbo.Ingredient", "HormoneBalance");
            DropColumn("dbo.Ingredient", "UrologicalHealth");
            DropColumn("dbo.Ingredient", "CardioVascularHealth");
            DropColumn("dbo.Ingredient", "DigestiveHealth");
            DropColumn("dbo.Ingredient", "NeurologicalHealth");
            DropColumn("dbo.Ingredient", "Immunity");
            DropColumn("dbo.Ingredient", "Cbs");
            DropColumn("dbo.Ingredient", "Fire");
            DropColumn("dbo.Ingredient", "Metal");
            DropColumn("dbo.Ingredient", "Tree");
            DropColumn("dbo.Ingredient", "Earth");
            DropColumn("dbo.Ingredient", "Water");
            DropColumn("dbo.Ingredient", "Nomad");
            DropColumn("dbo.Ingredient", "Warrior");
            DropColumn("dbo.Ingredient", "Explorer");
            DropColumn("dbo.Ingredient", "Teacher");
            DropColumn("dbo.Ingredient", "Gatherer");
            DropColumn("dbo.Ingredient", "Hunter");
            DropColumn("dbo.Ingredient", "KaphaDosha");
            DropColumn("dbo.Ingredient", "PittaDosha");
            DropColumn("dbo.Ingredient", "VataDosha");
            DropColumn("dbo.Product", "NourishesYang");
            DropColumn("dbo.Product", "NourishesYin");
            DropColumn("dbo.Product", "DentalHealth");
            DropColumn("dbo.Product", "Detoxification");
            DropColumn("dbo.Product", "StressRelief");
            DropColumn("dbo.Product", "HormoneBalance");
            DropColumn("dbo.Product", "UrologicalHealth");
            DropColumn("dbo.Product", "CardioVascularHealth");
            DropColumn("dbo.Product", "DigestiveHealth");
            DropColumn("dbo.Product", "NeurologicalHealth");
            DropColumn("dbo.Product", "Immunity");
            DropColumn("dbo.Product", "Cbs");
            DropColumn("dbo.Product", "Fire");
            DropColumn("dbo.Product", "Metal");
            DropColumn("dbo.Product", "Tree");
            DropColumn("dbo.Product", "Earth");
            DropColumn("dbo.Product", "Water");
            DropColumn("dbo.Product", "Nomad");
            DropColumn("dbo.Product", "Warrior");
            DropColumn("dbo.Product", "Explorer");
            DropColumn("dbo.Product", "Teacher");
            DropColumn("dbo.Product", "Gatherer");
            DropColumn("dbo.Product", "Hunter");
            DropColumn("dbo.Product", "KaphaDosha");
            DropColumn("dbo.Product", "PittaDosha");
            DropColumn("dbo.Product", "VataDosha");
            DropTable("dbo.FoodItem");
        }
    }
}

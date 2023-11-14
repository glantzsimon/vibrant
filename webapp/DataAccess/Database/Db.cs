using K9.DataAccessLayer.Models;
using System.Data.Entity;

namespace K9.DataAccessLayer.Database
{
    public class LocalDb : Base.DataAccessLayer.Database.Db
    {
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<ArticleSection> ArticleSections { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<DietaryRecommendation> DietaryRecommendations { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<MembershipOption> MembershipOptions { get; set; }
        public DbSet<PromoCode> PromoCodes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductIngredient> ProductIngredients { get; set; }
        public DbSet<ProductIngredientSubstitute> ProductIngredientSubstitutes { get; set; }
        public DbSet<IngredientSubstitute> IngredientSubstitutes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderProductPack> OrderProductPacks { get; set; }
        public DbSet<ClientProduct> ClientProducts { get; set; }
        public DbSet<ProductPack> ProductPacks { get; set; }
        public DbSet<ProductPackProduct> ProductPackProducts { get; set; }

        public DbSet<Protocol> Protocols { get; set; }
        public DbSet<ProtocolProduct> ProtocolProducts { get; set; }
        public DbSet<ProtocolProductPack> ProtocolProductPacks { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<ProtocolSection> ProtocolSections { get; set; }
        public DbSet<ProtocolSectionProduct> ProtocolSectionProducts { get; set; }
        public DbSet<ProtocolActivity> ProtocolActivities { get; set; }
        public DbSet<ProtocolDietaryRecommendation> ProtocolDietaryRecommendations { get; set; }
        public DbSet<ProtocolFoodItem> ProtocolFoodItems { get; set; }
        public DbSet<RepCommission> RepCommissions { get; set; }

        public DbSet<Models.HealthQuestionnaire> HealthQuestionnaires { get; set; }

        public DbSet<UserConsultation> UserConsultations { get; set; }
        public DbSet<UserCreditPack> UserCreditPack { get; set; }
        public DbSet<UserMembership> UserMemberships { get; set; }
        public DbSet<UserPromoCode> UserPromoCodes { get; set; }
        public DbSet<UserProtocol> UserProtocols { get; set; }
    }
}

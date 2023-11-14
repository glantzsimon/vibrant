using K9.DataAccessLayer.Models;
using System.Collections.Generic;

namespace K9.WebApplication.ViewModels
{
    public class GenoTypeBaseItemsViewModel
    {
        public List<Protocol> Protocols { get; set; }
        public List<ProductPack> ProductPacks { get; set; }
        public List<Product> Products { get; set; }
        public List<Activity> Activities { get; set; }
        public List<DietaryRecommendation> DietaryRecommendations { get; set; }
        public List<FoodItem> Foods { get; set; }
    }
}
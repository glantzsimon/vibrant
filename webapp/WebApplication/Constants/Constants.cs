using System.Collections.Generic;
using K9.DataAccessLayer.Enums;

namespace K9.WebApplication.Constants
{
    public static class Constants
    {
        public const string UnicornUser = "Unicorn User";
        public const string ClientUser = "Client User";
        public const string ProductionUser = "Production User";

        public const int CategoryGap = 100000;
        public const int ItemCodeGap = 222;

        public static List<ECategory> IngredientCategories = new List<ECategory>
        {
            ECategory.AminoAcid,
            ECategory.Vitamin,
            ECategory.Mineral,
            ECategory.Phytonutrient,
            ECategory.Herb,
            ECategory.Superfood,
            ECategory.Other
        };

        public static List<ECategory> ProductCategories = new List<ECategory>
        {
            ECategory.DietarySupplement,
            ECategory.PersonalCare
        };
    }
}
namespace K9.WebApplication.Helpers
{
    public static class RolesHelper
    {
        public static bool CurrentUserIsAdmin { get; set; }
        public static bool CurrentUserIsClientUser { get; set; }
        public static bool CurrentUserIsProductionUser { get; set; }
        public static bool CurrentUserIsUnicornUser { get; set; }
    }
}
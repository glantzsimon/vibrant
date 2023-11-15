using K9.WebApplication.Constants;
using System;

namespace K9.WebApplication.Helpers
{
    public static class SessionHelper
    {
        public static int GetIntValue(string key)
        {
            var value = Base.WebApplication.Helpers.SessionHelper.GetValue(key);
            var stringValue = value?.ToString() ?? string.Empty;
            int.TryParse(stringValue, out var intValue);
            return intValue;
        }

        public static DateTime? GetDateTimeValue(string key)
        {
            var value = Base.WebApplication.Helpers.SessionHelper.GetValue(key);
            var stringValue = value?.ToString() ?? string.Empty;
            if (DateTime.TryParse(stringValue, out var dateTimeValue))
            {
                return dateTimeValue;
            }
            return null;
        }

        public static bool GetBooleanValue(string key)
        {
            var value = Base.WebApplication.Helpers.SessionHelper.GetValue(key);
            var stringValue = value?.ToString() ?? string.Empty;
            if (bool.TryParse(stringValue, out var boolValue))
            {
                return boolValue;
            }
            return false;
        }

        public static void SetCurrentUserRoles(bool isAdmin = false, bool isClient = false, bool isProduction = false, bool isUnicorn = false)
        {
            Base.WebApplication.Helpers.SessionHelper.SetValue(SessionConstants.CurrentUserIsAdmin, isAdmin);
            Base.WebApplication.Helpers.SessionHelper.SetValue(SessionConstants.CurrentUserIsClientUser, isClient);
            Base.WebApplication.Helpers.SessionHelper.SetValue(SessionConstants.CurrentUserIsProductionUser, isProduction);
            Base.WebApplication.Helpers.SessionHelper.SetValue(SessionConstants.CurrentUserIsUnicornUser, isUnicorn);
        }

        public static bool CurrentUserIsAdmin() => GetBooleanValue(SessionConstants.CurrentUserIsAdmin);
        public static bool CurrentUserIsClientUser() => GetBooleanValue(SessionConstants.CurrentUserIsClientUser);
        public static bool CurrentUserIsProductionUser() => GetBooleanValue(SessionConstants.CurrentUserIsProductionUser);
        public static bool CurrentUserIsUnicornUser() => GetBooleanValue(SessionConstants.CurrentUserIsUnicornUser);
    }
}
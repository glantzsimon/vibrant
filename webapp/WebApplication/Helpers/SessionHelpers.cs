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

        public static void SetCurrentUserRoles(bool isAdmin = false, bool isPower = false, bool isClient = false, bool isPractitioner = false, bool isUnicorn = false)
        {
            Base.WebApplication.Helpers.SessionHelper.SetValue(Constants.Constants.Administrator, isAdmin);
            Base.WebApplication.Helpers.SessionHelper.SetValue(Constants.Constants.PowerUser, isPower);
            Base.WebApplication.Helpers.SessionHelper.SetValue(Constants.Constants.ClientUser, isClient);
            Base.WebApplication.Helpers.SessionHelper.SetValue(Constants.Constants.PractitionerUser, isPractitioner);
            Base.WebApplication.Helpers.SessionHelper.SetValue(Constants.Constants.UnicornUser, isUnicorn);
        }

        public static bool CurrentUserIsAdmin() => GetBooleanValue(Constants.Constants.Administrator);
        public static bool CurrentUserIsPowertUser() => GetBooleanValue(Constants.Constants.PowerUser);
        public static bool CurrentUserIsClientUser() => GetBooleanValue(Constants.Constants.ClientUser);
        public static bool CurrentUserIsPractitionerUser() => GetBooleanValue(Constants.Constants.PractitionerUser);
        public static bool CurrentUserIsUnicornUser() => GetBooleanValue(Constants.Constants.PractitionerUser);
    }
}
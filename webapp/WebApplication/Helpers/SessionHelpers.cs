using K9.WebApplication.Models;
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

        public static void SetLastSomething(MembershipModel model)
        {
            //Base.WebApplication.Helpers.SessionHelper.SetValue(Constants.SessionConstants.LastProfileDateOfBirth, model.PersonModel.DateOfBirth.ToString(Constants.FormatConstants.SessionDateTimeFormat));
        }

        public static void ClearLastSomething()
        {
            Base.WebApplication.Helpers.SessionHelper.SetValue(Constants.SessionConstants.IsRetrieveSomething, false);
        }

        public static PersonModel GetLastSomething(bool todayOnly = false, bool remove = true)
        {
            if (Base.WebApplication.Helpers.SessionHelper.GetBoolValue(Constants.SessionConstants.IsRetrieveSomething) && (!todayOnly || GetDateTimeValue(Constants.SessionConstants.SomethingStoredOn) == DateTime.Today))
            {
                //DateTime.TryParse(Base.WebApplication.Helpers.SessionHelper.GetStringValue(Constants.SessionConstants.LastProfileDateOfBirth), out var dateOfBirth);
                
                if (remove)
                    ClearLastSomething();

                return new PersonModel
                {
                    //DateOfBirth = dateOfBirth,
                    //Gender = gender,
                    //Name = Base.WebApplication.Helpers.SessionHelper.GetStringValue(Constants.SessionConstants.LastProfileName)
                };
            }
            return null;
        }

    }
}
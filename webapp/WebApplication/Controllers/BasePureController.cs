using K9.Base.WebApplication.Constants;
using K9.Base.WebApplication.Controllers;
using K9.Base.WebApplication.Helpers;
using K9.DataAccessLayer.Models;
using K9.Globalisation;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Enums;
using K9.WebApplication.Models;
using K9.WebApplication.Services;
using NLog;
using System.Collections.Generic;
using System.Web.Mvc;
using K9.WebApplication.Packages;
using WebMatrix.WebData;

namespace K9.WebApplication.Controllers
{
    public class BasePureController : BaseController, IShoppingCartController
    {
        private readonly IRoles _roles;
        private readonly IMembershipService _membershipService;
        private readonly IPureControllerPackage _pureControllerPackage;

        public Order ShoppingCart => WebSecurity.IsAuthenticated
            ? _pureControllerPackage.ShoppingCartService.GetShoppingCart(WebSecurity.CurrentUserId)
            : null;

        public BasePureController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles,
            IAuthentication authentication, IFileSourceHelper fileSourceHelper, IPureControllerPackage pureControllerPackage)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper)
        {
            _roles = roles;
            _membershipService = pureControllerPackage.MembershipService;
            SetBetaWarningSessionVariable();

            var acceptableCultureCodes = new List<string>
            {
                Strings.CultureCodes.English,
                Strings.CultureCodes.Thai
            };

            if (!acceptableCultureCodes.Contains(SessionHelper.GetStringValue(SessionConstants.CultureCode)))
            {
                SetCultureCode(Strings.LanguageCodes.English, Strings.CultureCodes.English);
            }
        }

        public ActionResult SetLanguage(string languageCode, string cultureCode)
        {
            SetCultureCode(languageCode, cultureCode);
            return Redirect(Request.UrlReferrer?.ToString());
        }

        public UserMembership GetActiveUserMembership()
        {
            if (Authentication.IsAuthenticated)
            {
                return _membershipService.GetActiveUserMembership(Authentication.CurrentUserId);
            }

            return null;
        }

        public override string GetObjectName()
        {
            return string.Empty;
        }

        public EDeviceType GetDeviceType()
        {
            return new BrowserInfo(Request.Headers["User-Agent"]).DeviceType;
        }

        private void SetCultureCode(string languageCode, string cultureCode)
        {
            SessionHelper.SetValue(SessionConstants.LanguageCode, languageCode);
            SessionHelper.SetValue(SessionConstants.CultureCode, cultureCode);
        }

        private static void SetBetaWarningSessionVariable()
        {
            var numberOfDisplays = Helpers.SessionHelper.GetIntValue(Constants.SessionConstants.BetaWarningDisplay);
            if (numberOfDisplays < 1)
            {
                numberOfDisplays++;
                SessionHelper.SetValue(Constants.SessionConstants.BetaWarningDisplay, numberOfDisplays);
            }
            else
            {
                SessionHelper.SetValue(Constants.SessionConstants.BetaWarningHide, true);
            }
        }
    }
}

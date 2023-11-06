using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using NLog;
using System.Web.Mvc;
using K9.Base.WebApplication.Constants;

namespace K9.WebApplication.Controllers
{
    public class HomeController : BasePureController
    {
        private readonly IAuthentication _authentication;
        
        public HomeController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, membershipService)
        {
            _authentication = authentication;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult DownloadKickStartGuide()
        {
            return View();
        }
        
        [Route("privacy-policy")]
        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public override string GetObjectName()
        {
            return string.Empty;
        }
    }
}

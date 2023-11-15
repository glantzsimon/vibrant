using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using NLog;
using System.Web.Mvc;
using K9.WebApplication.Packages;

namespace K9.WebApplication.Controllers
{
    public class SupplementationController : BasePureController
    {
        private readonly IAuthentication _authentication;
        
        public SupplementationController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IPureControllerPackage pureControllerPackage)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, pureControllerPackage)
        {
            _authentication = authentication;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AntiAging()
        {
            return View();
        }

        public ActionResult Nootropics()
        {
            return View();
        }

        public ActionResult NutritionalSupplements()
        {
            return View();
        }

        public ActionResult SuperFoods()
        {
            return View();
        }

        public ActionResult SuperHerbs()
        {
            return View();
        }

        public override string GetObjectName()
        {
            return string.Empty;
        }
    }
}

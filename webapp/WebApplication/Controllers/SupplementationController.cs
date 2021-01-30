using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using NLog;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    public class SupplementationController : BaseVibrantController
    {
        private readonly IAuthentication _authentication;
        
        public SupplementationController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, membershipService)
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

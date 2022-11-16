using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using NLog;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    public class PersonalisedNutritionController : BasePureController
    {
        private readonly IAuthentication _authentication;
        
        public PersonalisedNutritionController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, membershipService)
        {
            _authentication = authentication;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Route("acid-alkaline")]
        public ActionResult AcidAlkaline()
        {
            return View();
        }

        public override string GetObjectName()
        {
            return string.Empty;
        }
    }
}

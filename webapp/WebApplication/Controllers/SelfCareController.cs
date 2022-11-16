using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using NLog;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    public class SelfCareController : BasePureController
    {
        private readonly IAuthentication _authentication;
        
        public SelfCareController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, membershipService)
        {
            _authentication = authentication;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BinauralBeats()
        {
            return View();
        }

        public ActionResult Breath()
        {
            return View();
        }

        public ActionResult Detoxification()
        {
            return View();
        }

        public ActionResult EMFs()
        {
            return View();
        }

        public override string GetObjectName()
        {
            return string.Empty;
        }
    }
}

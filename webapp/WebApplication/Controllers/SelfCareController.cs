using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using NLog;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Route("self-care")]
    [Route("wellness")]
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

        [Route("protocols")]
        public ActionResult Protocols()
        {
            return View();
        }

        [Route("acid-alkaline")]
        public ActionResult AcidAlkaline()
        {
            return View();
        }

        public ActionResult Doshas()
        {
            return View();
        }

        public ActionResult Fats()
        {
            return View();
        }

        [Route("fermented-foods")]
        public ActionResult FermentedFoods()
        {
            return View();
        }

        [Route("metabolic-types")]
        public ActionResult MetabolicTypes()
        {
            return View();
        }

        [Route("meditation-techniques")]
        public ActionResult MeditationTechniques()
        {
            return View();
        }

        public ActionResult Minerals()
        {
            return View();
        }

        [Route("raw-or-cooked")]
        public ActionResult RawOrCooked()
        {
            return View();
        }

        [Route("vegan-or-omni")]
        public ActionResult VeganOrOmni()
        {
            return View();
        }

        [Route("breathwork")]
        public ActionResult Breathwork()
        {
            return View();
        }

        [Route("cold-exposure")]
        public ActionResult ColdExposure()
        {
            return View();
        }

        public ActionResult Detoxification()
        {
            return View();
        }

        public ActionResult ElectroMedicine()
        {
            return View();
        }

        public ActionResult EMFs()
        {
            return View();
        }

        public ActionResult Grounding()
        {
            return View();
        }

        [Route("immune-support")]
        public ActionResult ImmuneSupport()
        {
            return View();
        }

        public ActionResult Infections()
        {
            return View();
        }

        public ActionResult SaunaTherapy()
        {
            return View();
        }

        [Route("oral-care")]
        public ActionResult OralCare()
        {
            return View();
        }

        public ActionResult Rebounding()
        {
            return View();
        }

        public ActionResult Sleep()
        {
            return View();
        }

        [Route("water-magic")]
        public ActionResult WaterMagic()
        {
            return View();
        }
        
        [Route("sun-gazing")]
        public ActionResult Sungazing()
        {
            return View();
        }

        public override string GetObjectName()
        {
            return string.Empty;
        }
    }
}

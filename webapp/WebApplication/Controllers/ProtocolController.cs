using K9.Base.WebApplication.Filters;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using NLog;
using System;
using System.Web.Mvc;
using K9.WebApplication.Packages;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = Constants.Constants.ClientUser)]
    public partial class ProtocolController : BasePureController
    {
        private readonly IProtocolService _protocolService;
        private readonly IHealthQuestionnaireService _healthQuestionnaireService;

        public ProtocolController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IRepository<Product> productsRepository, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService, IProtocolService protocolService, IHealthQuestionnaireService healthQuestionnaireService, IPureControllerPackage pureControllerPackage) : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, pureControllerPackage)
        {
            _protocolService = protocolService;
            _healthQuestionnaireService = healthQuestionnaireService;
        }

        public ActionResult Summary(Guid id)
        {
            var protocol = _protocolService.Find(id);
            protocol = _protocolService.GetProtocolWithProtocolSections(protocol.Id);
            ViewBag.ProtocolView = true;
            return View("../Protocols/Summary", protocol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Summary(Protocol model)
        {
            var hq = _healthQuestionnaireService.GetHealthQuestionnaireForClient(model.ClientId.Value);

            hq.EatsRedMeat = model.EatsRedMeat;
            hq.EatsPoultry = model.EatsPoultry;
            hq.EatsFishAndSeafood = model.EatsFishAndSeafood;
            hq.EatsEggsAndRoes = model.EatsEggsAndRoes;
            hq.EatsDairy = model.EatsDairy;
            hq.EatsVegetables = model.EatsVegetables;
            hq.EatsVegetableProtein = model.EatsVegetableProtein;
            hq.EatsFungi = model.EatsFungi;
            hq.EatsFruit = model.EatsFruit;
            hq.EatsGrains = model.EatsGrains;
            hq.IsLowOxalate = model.IsLowOxalate;
            hq.IsLowLectin = model.IsLowLectin;
            hq.IsLowPhytate = model.IsLowPhytate;
            hq.IsLowHistamine = model.IsLowHistamine;
            hq.IsLowMycotoxin = model.IsLowMycotoxin;
            hq.IsLowOmega6 = model.IsLowOmega6;
            hq.IsBulletProof = model.IsBulletProof;
            hq.IsSattvic = model.IsSattvic;
            hq.IsLowSulphur = model.IsLowSulphur;
            hq.IsKeto = model.IsKeto;
            hq.CurrentHealthLevel = model.CurrentHealthLevel;
            hq.AutomaticallyFilterFoods = model.AutomaticallyFilterFoods;

            if (model.AutomaticallyFilterFoods)
            {
                var threshold = hq.GetScoreThreshold();

                hq.IsLowOxalate = hq.GetOxalateScore() > threshold;
                hq.IsLowLectin = hq.GetLectinsScore() > threshold;
                hq.IsLowPhytate = hq.GetPhytateScore() > threshold;
                hq.IsLowHistamine = hq.GetHistamineScore() > threshold;
                hq.IsLowMycotoxin = hq.GetMycotoxinScore() > threshold;
                hq.IsLowOmega6 = hq.GetOmega6Score() > threshold;
                hq.IsLowSulphur = hq.GetCbsScore() > threshold;
                hq.IsKeto= hq.GetCbsScore() > threshold || hq.GetInflammationScore() > threshold;
            }

            _healthQuestionnaireService.Save(hq);

            return RedirectToAction("Summary", new { id = model.ExternalId });
        }

        public ActionResult PrintableSummary(Guid id)
        {
            var protocol = _protocolService.Find(id);
            protocol = _protocolService.GetProtocolWithProtocolSections(id);
            return View("../Protocols/PrintableSummary", protocol);
        }
        
        public ActionResult PrintableFoodList(Guid id)
        {
            var protocol = _protocolService.Find(id);
            protocol = _protocolService.GetProtocolWithProtocolSections(id);
            return View("../Protocols/PrintableFoodsList", protocol);
        }
    }
}

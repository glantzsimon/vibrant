using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using NLog;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    public class ProtocolArticlesController : BasePureController
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IProductService _productService;
        private readonly IProtocolService _protocolService;

        public ProtocolArticlesController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IRepository<Product> productsRepository, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService, IProductService productService, IProtocolService protocolService)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, membershipService)
        {
            _productsRepository = productsRepository;
            _productService = productService;
            _protocolService = protocolService;
        }

        public ActionResult Index()
        {
            return View(_protocolService.List());
        }

        [Route("health-questionnaire")]
        public ActionResult Questionnaire()
        {
            return View(); ;
        }

        [Route("cbs-upregulation")]
        public ActionResult CbsUpregulation()
        {
            return View(); ;
        }

        [Route("sulfur-detox")]
        public ActionResult SulfurDetox()
        {
            return View(); ;
        }

        [Route("methylation-support")]
        public ActionResult MethylationSupport()
        {
            return View(); ;
        }

        [Route("heavy-metal-detox")]
        public ActionResult HeavyMetalDetox()
        {
            return View(); ;
        }

        [Route("general-wellbeing")]
        public ActionResult GeneralWellbeing()
        {
            return View(); ;
        }
    }
}

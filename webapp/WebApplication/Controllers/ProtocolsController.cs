using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using NLog;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    public class ProtocolsController : BasePureController
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IProductService _productService;

        public ProtocolsController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IRepository<Product> productsRepository, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService, IProductService productService)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, membershipService)
        {
            _productsRepository = productsRepository;
            _productService = productService;
        }
        
        public ActionResult Index()
        {
            return View(); ;
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

        [Route("methylation-support")]
        public ActionResult MethylationSupport()
        {
            return View(); ;
        }
    }
}

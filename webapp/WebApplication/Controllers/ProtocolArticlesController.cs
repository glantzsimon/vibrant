using System.Linq;
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
            return View(_protocolService.List().Where(e => !e.ClientId.HasValue).ToList());
        }
        
    }
}

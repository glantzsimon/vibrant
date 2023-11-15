using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using K9.WebApplication.ViewModels;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using K9.WebApplication.Packages;
using WebMatrix.WebData;

namespace K9.WebApplication.Controllers
{
    public class ProtocolArticlesController : BasePureController
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IProductService _productService;
        private readonly IProtocolService _protocolService;
        private readonly IHealthQuestionnaireService _healthQuestionnaireService;
        private readonly IClientService _clientService;

        public ProtocolArticlesController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IRepository<Product> productsRepository, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService, IProductService productService, IProtocolService protocolService, IHealthQuestionnaireService healthQuestionnaireService, IClientService clientService, IPureControllerPackage pureControllerPackage)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, pureControllerPackage)
        {
            _productsRepository = productsRepository;
            _productService = productService;
            _protocolService = protocolService;
            _healthQuestionnaireService = healthQuestionnaireService;
            _clientService = clientService;
        }

        [Route("protocols/viewall")]
        public ActionResult Index()
        {
            var protocols = _protocolService.List().Where(e => !e.ClientId.HasValue).ToList();
            var recommended = new List<Protocol>();

            if (WebSecurity.IsAuthenticated)
            {
                var clientRecord = _clientService.FindFromUser(WebSecurity.CurrentUserId);
                if (clientRecord != null)
                {
                    recommended = _healthQuestionnaireService.GetGeneticProfileMatchedProtocols(clientRecord.Id);
                    var recommendedIds = recommended.Select(e => e.Id).ToList();
                    
                    protocols = protocols.Where(e => !recommendedIds.Contains(e.Id)).OrderBy(e => e.Name).ToList();
                }
            }
            
            return View(new ProtocolsViewModel
            {
                Protocols = protocols,
                Recommended = recommended
            });
        }
        
    }
}

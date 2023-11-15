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
    public class ProtocolController : BasePureController
    {
        private readonly IProtocolService _protocolService;

        public ProtocolController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IRepository<Product> productsRepository, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService, IProtocolService protocolService, IPureControllerPackage pureControllerPackage) : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, pureControllerPackage)
        {
            _protocolService = protocolService;
        }

        public ActionResult Summary(Guid id)
        {
            var protocol = _protocolService.Find(id);
            protocol = _protocolService.GetProtocolWithProtocolSections(protocol.Id);
            return View("../Protocols/Summary", protocol);
        }

        public ActionResult PrintableSummary(Guid id)
        {
            var protocol = _protocolService.Find(id);
            protocol = _protocolService.GetProtocolWithProtocolSections(id);
            return View("../Protocols/PrintableSummary", protocol);
        }
    }
}

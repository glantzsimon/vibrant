using K9.Base.WebApplication.Filters;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using NLog;
using System;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = Constants.Constants.UnicornUser)]
    public class ProtocolController : BasePureController
    {
        private readonly IProtocolService _protocolService;

        public ProtocolController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IRepository<Product> productsRepository, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService, IProtocolService protocolService) : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, membershipService)
        {
            _protocolService = protocolService;
        }

        public ActionResult Summary(Guid id)
        {
            var protocol = _protocolService.Find(id);
            protocol = _protocolService.GetProtocolWithProtocolSections(protocol.Id);
            return View("../Protocols/Summary", protocol);
        }
    }
}

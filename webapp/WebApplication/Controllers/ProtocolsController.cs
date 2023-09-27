using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.WebApplication.Services;
using System;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class ProtocolsController : HtmlControllerBase<Protocol>
    {
        private readonly IProtocolService _protocolService;

        public ProtocolsController(IControllerPackage<Protocol> controllerPackage, IProtocolService protocolService) : base(controllerPackage)
        {
            _protocolService = protocolService;
            RecordCreated += ProtocolsController_RecordCreated;
            RecordBeforeCreated += ProtocolsController_RecordBeforeCreated;
            RecordBeforeDetails += ProtocolsController_RecordBeforeDetails;
            RecordBeforeUpdate += ProtocolsController_RecordBeforeUpdate;
            RecordBeforeDelete += ProtocolsController_RecordBeforeDelete;
            RecordBeforeDeleted += ProtocolsController_RecordBeforeDeleted;
        }

        public ActionResult View(int protocolId)
        {
            return RedirectToAction("Details", null, new { id = protocolId });
        }

        public ActionResult DuplicateProtocol(int id)
        {
            var duplicate = _protocolService.Duplicate(id);
            return RedirectToAction("Edit", new { id = duplicate.Id });
        }

        public ActionResult EditSections(int id = 0)
        {
            return RedirectToAction("EditProtocolProtocolSectionsForProtocol", "ProtocolProtocolSections", new { id });
        }

        public ActionResult EditProducts(int id = 0)
        {
            return RedirectToAction("EditProductsForProtocol", "ProtocolProducts", new { id });
        }

        public ActionResult EditProductPacks(int id = 0)
        {
            return RedirectToAction("EditProductPacksForProtocol", "ProtocolProductPacks", new { id });
        }

        private void ProtocolsController_RecordBeforeDeleted(object sender, CrudEventArgs e)
        {
            _protocolService.DeleteChildRecords(e.Item.Id);
        }

        private void ProtocolsController_RecordBeforeDelete(object sender, CrudEventArgs e)
        {
            var protocol = e.Item as Protocol;
            _protocolService.GetFullProtocol(protocol);
        }

        private void ProtocolsController_RecordBeforeUpdate(object sender, CrudEventArgs e)
        {
            var protocol = e.Item as Protocol;
            _protocolService.GetFullProtocol(protocol);
        }

        private void ProtocolsController_RecordBeforeDetails(object sender, CrudEventArgs e)
        {
            var protocol = e.Item as Protocol;
            _protocolService.GetFullProtocol(protocol);
        }

        private void ProtocolsController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var protocol = e.Item as Protocol;
            protocol.ExternalId = Guid.NewGuid();
        }

        private void ProtocolsController_RecordCreated(object sender, CrudEventArgs e)
        {
            _protocolService.AddDefaultSections(e.Item.Id);
        }
    }
}

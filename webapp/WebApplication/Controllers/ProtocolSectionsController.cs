using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using System.Linq;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class ProtocolSectionsController : HtmlControllerBase<ProtocolSection>
    {
        public ProtocolSectionsController(IControllerPackage<ProtocolSection> controllerPackage) : base(controllerPackage)
        {
            RecordBeforeCreate += ProtocolSectionsController_RecordBeforeCreate;
        }

        public ActionResult View(int protocolSectionId)
        {
            return RedirectToAction("Details", null, new { id = protocolSectionId });
        }

        private void ProtocolSectionsController_RecordBeforeCreate(object sender, CrudEventArgs e)
        {
            var protocolSection = e.Item as ProtocolSection;
            UpdateDisplayOrder(protocolSection);
        }

        private void UpdateDisplayOrder(ProtocolSection protocolSection)
        {
            var lastSection = Repository.CustomQuery<ProtocolSection>(
                $"SELECT TOP 1 * FROM {nameof(ProtocolSection)} ORDER BY {nameof(ProtocolSection.DisplayOrder)} DESC").FirstOrDefault();
            protocolSection.DisplayOrder = lastSection?.DisplayOrder + 1 ?? 0;
        }
    }
}

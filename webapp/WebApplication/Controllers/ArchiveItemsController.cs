using K9.Base.DataAccessLayer.Models;
using K9.Base.WebApplication.Controllers;
using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.SharedLibrary.Authentication;
using System;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class ArchiveItemsController : BaseController<ArchiveItem>
    {

        public ArchiveItemsController(IControllerPackage<ArchiveItem> controllerPackage)
            : base(controllerPackage)
        {
            RecordBeforeCreate += ArchiveItemsController_RecordBeforeCreate;
        }

        void ArchiveItemsController_RecordBeforeCreate(object sender, CrudEventArgs e)
        {
            var archiveItem = e.Item as ArchiveItem;
            archiveItem.PublishedBy = WebSecurity.IsAuthenticated ? WebSecurity.CurrentUserName : string.Empty;
            archiveItem.PublishedOn = DateTime.Now;
        }

    }
}
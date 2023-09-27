using System;
using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.WebApplication.Extensions;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class ActivitiesController : HtmlControllerBase<Activity>
    {
        public ActivitiesController(IControllerPackage<Activity> controllerPackage) : base(controllerPackage)
        {
            RecordBeforeCreated += ActivitiesController_RecordBeforeCreated;
        }

        private void ActivitiesController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var activity = e.Item as Activity;
            activity.ExternalId = Guid.NewGuid();
        }
    }
}

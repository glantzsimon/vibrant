using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.WebApplication.Packages;
using System;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class ActivitiesController : HtmlControllerBase<Activity>
    {
        public ActivitiesController(IControllerPackage<Activity> controllerPackage, IPureControllerPackage pureControllerPackage) : base(controllerPackage, pureControllerPackage)
        {
            RecordBeforeCreated += ActivitiesController_RecordBeforeCreated;
            RecordBeforeUpdated += ActivitiesController_RecordBeforeUpdated;
        }

        private void ActivitiesController_RecordBeforeUpdated(object sender, CrudEventArgs e)
        {
            var activity = e.Item as Activity;
        }

        private void ActivitiesController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var activity = e.Item as Activity;
            activity.ExternalId = Guid.NewGuid();
        }
    }
}

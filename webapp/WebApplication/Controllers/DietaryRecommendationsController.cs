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
    public class DietaryRecommendationsController : HtmlControllerBase<DietaryRecommendation>
    {
        public DietaryRecommendationsController(IControllerPackage<DietaryRecommendation> controllerPackage, IPureControllerPackage pureControllerPackage) : base(controllerPackage, pureControllerPackage)
        {
            RecordBeforeCreated += DietaryRecommendationsController_RecordBeforeCreated;
            RecordBeforeUpdated += DietaryRecommendationsController_RecordBeforeUpdated;
        }

        private void DietaryRecommendationsController_RecordBeforeUpdated(object sender, CrudEventArgs e)
        {
            var item = e.Item as DietaryRecommendation;
        }

        private void DietaryRecommendationsController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var item = e.Item as DietaryRecommendation;
            item.ExternalId = Guid.NewGuid();
        }
    }
}

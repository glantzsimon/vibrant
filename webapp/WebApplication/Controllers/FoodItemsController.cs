using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using System;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class FoodItemsController : HtmlControllerBase<FoodItem>
    {
        public FoodItemsController(IControllerPackage<FoodItem> controllerPackage) : base(controllerPackage)
        {
            RecordBeforeCreated += FoodItemsController_RecordBeforeCreated;
            RecordBeforeUpdated += FoodItemsController_RecordBeforeUpdated;
        }

        private void FoodItemsController_RecordBeforeUpdated(object sender, CrudEventArgs e)
        {
            var FoodItem = e.Item as FoodItem;
        }

        private void FoodItemsController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var FoodItem = e.Item as FoodItem;
            FoodItem.ExternalId = Guid.NewGuid();
        }
    }
}

using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using NLog;
using System.Web.Mvc;
using K9.Base.WebApplication.Filters;
using K9.SharedLibrary.Authentication;

namespace K9.WebApplication.Controllers
{
    public class SharedController : BasePureController
    {
        private readonly IAuthentication _authentication;
        private readonly IProductService _productService;
        private readonly IIngredientService _ingredientService;
        private readonly IOrderService _orderService;

        public SharedController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService, IProductService productService, IIngredientService ingredientService, IOrderService orderService)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, membershipService)
        {
            _authentication = authentication;
            _productService = productService;
            _ingredientService = ingredientService;
            _orderService = orderService;
        }

        public override string GetObjectName()
        {
            return string.Empty;
        }

        [Authorize]
        [RequirePermissions(Role = RoleNames.Administrators)]
        public ActionResult RunMaintenanceScript()
        {
            //_productService.UpdateProductCategories();
            //_ingredientService.UpdateIngredientCategories();
            //_orderService.UpdateFullName();
            _ingredientService.UpdateIngredientCategories();

            return RedirectToAction("MaintenanceComplete");
        }

        public ActionResult MaintenanceComplete()
        {
            return View();
        }
    }
}

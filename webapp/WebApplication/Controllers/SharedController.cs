using K9.Base.WebApplication.Filters;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Extensions;
using K9.WebApplication.Packages;
using K9.WebApplication.Services;
using NLog;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    public class SharedController : BasePureController
    {
        private readonly IAuthentication _authentication;
        private readonly IProductService _productService;
        private readonly IIngredientService _ingredientService;
        private readonly IOrderService _orderService;
        private readonly IQrCodeService _qrCodeService;
        private readonly IMaintenanceService _maintenanceService;
        private readonly IHealthQuestionnaireService _healthQuestionnaireService;
        private readonly IRepository<FoodItem> _foodItemsRepository;

        public SharedController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService, IProductService productService, IIngredientService ingredientService, IOrderService orderService, IQrCodeService qrCodeService, IMaintenanceService maintenanceService, IPureControllerPackage pureControllerPackage, IHealthQuestionnaireService healthQuestionnaireService, IRepository<FoodItem> foodItemsRepository)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, pureControllerPackage)
        {
            _authentication = authentication;
            _productService = productService;
            _ingredientService = ingredientService;
            _orderService = orderService;
            _qrCodeService = qrCodeService;
            _maintenanceService = maintenanceService;
            _healthQuestionnaireService = healthQuestionnaireService;
            _foodItemsRepository = foodItemsRepository;
        }

        public override string GetObjectName()
        {
            return string.Empty;
        }

        [Authorize]
        [RequirePermissions(Role = RoleNames.Administrators)]
        public ActionResult ClearCache()
        {
            _productService.ClearCache();
            return RedirectToAction("MaintenanceComplete");
        }

        [Authorize]
        [RequirePermissions(Role = RoleNames.Administrators)]
        public ActionResult RunMaintenanceScript()
        {
            //_productService.UpdateProductCategories();
            //_maintenanceService.AddFoodItemsAndActivities();

            return RedirectToAction("MaintenanceComplete");
        }

        public ActionResult MaintenanceComplete()
        {
            return View();
        }

        [OutputCache(VaryByParam = "*", Duration = int.MaxValue)]
        public ActionResult GetQrCode(string code, int size = 111)
        {
            var image = _qrCodeService.GetQrCode(code, size).ToByteArray();

            Response.Clear();
            Response.ContentType = "image/gif";
            Response.BinaryWrite(image);
            Response.End();

            return new EmptyResult();
        }

        
    }
}

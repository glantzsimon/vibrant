using K9.Base.WebApplication.Filters;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Extensions;
using K9.WebApplication.Services;
using NLog;
using System.Web.Mvc;
using K9.WebApplication.Packages;

namespace K9.WebApplication.Controllers
{
    public class SharedController : BasePureController
    {
        private readonly IAuthentication _authentication;
        private readonly IProductService _productService;
        private readonly IIngredientService _ingredientService;
        private readonly IOrderService _orderService;
        private readonly IQrCodeService _qrCodeService;

        public SharedController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService, IProductService productService, IIngredientService ingredientService, IOrderService orderService, IQrCodeService qrCodeService, IPureControllerPackage pureControllerPackage)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, pureControllerPackage)
        {
            _authentication = authentication;
            _productService = productService;
            _ingredientService = ingredientService;
            _orderService = orderService;
            _qrCodeService = qrCodeService;
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

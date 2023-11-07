using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using K9.WebApplication.Services;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    public class OrderController : BasePureController
    {
        private readonly IRepository<OrderProduct> _orderProductsRepository;
        private readonly IRepository<OrderProductPack> _orderProductPackRepository;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly DefaultValuesConfiguration _defaultValues;

        public OrderController(IControllerPackage<Order> controllerPackage, IOptions<DefaultValuesConfiguration> defaultValues, IRepository<OrderProduct> orderProductsRepository, IRepository<OrderProductPack> orderProductPackRepository, IOrderService orderService, IProductService productService, IMembershipService membershipService) : base(controllerPackage.Logger, controllerPackage.DataSetsHelper, controllerPackage.Roles, controllerPackage.Authentication, controllerPackage.FileSourceHelper, membershipService)
        {
            _orderProductsRepository = orderProductsRepository;
            _orderProductPackRepository = orderProductPackRepository;
            _orderService = orderService;
            _productService = productService;
            _defaultValues = defaultValues.Value;
        }

        [Route("orders/view")]
        public ActionResult ViewOrder(string ordernumber)
        {
            var order = _orderService.Find(ordernumber);
            if (order == null)
            {
                return HttpNotFound();
            }
            if (order.UserId != WebSecurity.CurrentUserId)
            {
                return HttpForbidden();
            }

            ViewBag.DeviceType = GetDeviceType();

            return View(order);
        }
    }
}


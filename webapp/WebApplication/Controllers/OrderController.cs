using K9.Base.DataAccessLayer.Models;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using K9.WebApplication.Services;
using System.Linq;
using System.Web.Mvc;
using K9.WebApplication.Packages;
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
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<Client> _clientsRepository;
        private readonly DefaultValuesConfiguration _defaultValues;

        public OrderController(IControllerPackage<Order> controllerPackage, IOptions<DefaultValuesConfiguration> defaultValues, IRepository<OrderProduct> orderProductsRepository, IRepository<OrderProductPack> orderProductPackRepository, IOrderService orderService, IProductService productService, IMembershipService membershipService, IRepository<User> usersRepository, IRepository<Client> clientsRepository, IPureControllerPackage pureControllerPackage) : 
            base(controllerPackage.Logger, controllerPackage.DataSetsHelper, controllerPackage.Roles, controllerPackage.Authentication, controllerPackage.FileSourceHelper, pureControllerPackage)
        {
            _orderProductsRepository = orderProductsRepository;
            _orderProductPackRepository = orderProductPackRepository;
            _orderService = orderService;
            _productService = productService;
            _usersRepository = usersRepository;
            _clientsRepository = clientsRepository;
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
                var user = _usersRepository.Find(WebSecurity.CurrentUserId);
                var client = _clientsRepository.Find(e => e.UserId == user.Id).First();
                if (client.Id != order.ClientId)
                {
                    return HttpForbidden();
                }
            }

            ViewBag.DeviceType = GetDeviceType();

            return View(order);
        }
    }
}


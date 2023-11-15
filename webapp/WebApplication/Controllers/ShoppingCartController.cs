using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Extensions;
using K9.WebApplication.Models;
using K9.WebApplication.Packages;
using K9.WebApplication.Services;
using NLog;
using System;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    public partial class ShoppingCartController : HtmlControllerBase<Order>
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOrderService _orderService;
        private readonly IClientService _clientService;
        private readonly ILogger _logger;
        
        public ShoppingCartController(IControllerPackage<Order> controllerPackage, IShoppingCartService shoppingCartService, IOrderService orderService, IClientService clientService, ILogger logger, IPureControllerPackage pureControllerPackage) : base(controllerPackage, pureControllerPackage)
        {
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
            _clientService = clientService;
            _logger = logger;
        }

        public Order GetShoppingCart()
        {
            return WebSecurity.IsAuthenticated ? _shoppingCartService.GetShoppingCart(WebSecurity.CurrentUserId) : null;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCart(Order model)
        {
            if (model.Products != null && model.Products.Any())
            {
                foreach (var orderProduct in model.Products)
                {
                    _shoppingCartService.UpdateProductAmount(orderProduct.ProductId, orderProduct.Amount);
                }
            }

            if (model.ProductPacks != null && model.ProductPacks.Any())
            {
                foreach (var orderProductPack in model.ProductPacks)
                {
                    _shoppingCartService.UpdateProductPackAmount(orderProductPack.ProductPackId, orderProductPack.Amount);
                }
            }

            return RedirectToAction("Checkout");
        }

        public ActionResult ViewCart()
        {
            return View(GetShoppingCart());
        }
        
        [Route("shop/checkout")]
        public ActionResult Checkout()
        {
            return View(GetShoppingCart());
        }

        [HttpPost]
        public ActionResult ProcessPayment(PurchaseModel purchaseModel)
        {
            try
            {
                var order = _orderService.Find(purchaseModel.ItemId);

                order.OrderType = EOrderType.Sale;
                order.RequestedOn = DateTime.Today;
                order.PaidOn = DateTime.Today;
                Repository.Update(order);
                _orderService.ClearCache();

                return Json(new { success = true, itemId = order.OrderNumber });
            }
            catch (Exception ex)
            {
                _logger.Error($"ShoppingCartController => ProcessPayment => Error: {ex.GetFullErrorMessage()}");
                return Json(new { success = false, error = ex.Message });
            }
        }

        public ActionResult OrderCreateSuccess(string itemId)
        {
            var order = _orderService.Find(itemId);
            return View(order);
        }
    }
}


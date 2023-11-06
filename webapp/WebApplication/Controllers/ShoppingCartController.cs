using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.WebApplication.Services;
using System;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    public class ShoppingCartController : HtmlControllerBase<Order>
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOrderService _orderService;

        public ShoppingCartController(IControllerPackage<Order> controllerPackage, IShoppingCartService shoppingCartService, IOrderService orderService) : base(controllerPackage)
        {
            _shoppingCartService = shoppingCartService;
            _orderService = orderService;
        }

        public ActionResult ViewCart()
        {
            var cart = _shoppingCartService.GetShoppingCart(WebSecurity.CurrentUserId);
            return View(cart);
        }

        [ChildActionOnly]
        public ActionResult ShoppingCartMenuItem()
        {
            var cart = _shoppingCartService.GetShoppingCart(WebSecurity.CurrentUserId);
            if (cart == null)
            {
                return new EmptyResult();
            }

            return PartialView("_MenuItem");
        }

        public JsonResult AddProductToCart(int productId)
        {
            try
            {
                _shoppingCartService.AddProductToCart(productId, 1);
                _orderService.ClearCache();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public JsonResult AddProductPackToCart(int productPackId)
        {
            try
            {
                _shoppingCartService.AddProductPackToCart(productPackId, 1);
                _orderService.ClearCache();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }



    }
}


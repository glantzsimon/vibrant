using K9.DataAccessLayer.Models;
using System;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    public partial class ShoppingCartController
    {
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

        public JsonResult UpdateProductAmount(int productId, int amount)
        {
            try
            {
                _shoppingCartService.UpdateProductAmount(productId, amount);
                _orderService.ClearCache();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public JsonResult UpdateProductPackAmount(int productPackId, int amount)
        {
            try
            {
                _shoppingCartService.UpdateProductPackAmount(productPackId, amount);
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


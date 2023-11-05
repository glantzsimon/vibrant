using System;
using System.Collections.Generic;
using System.Web.Mvc;
using K9.SharedLibrary.Helpers;
using K9.WebApplication.Models;
using ServiceStack.Text;

namespace K9.WebApplication.Controllers
{
    public partial class ProductsController
    {
        public JsonResult MarkIngredientAsOutOfStock(int ingredientId)
        {
            try
            {
                _ingredientService.MarkIngredientAsOutOfStock(ingredientId);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [Route("products/json")]
        public JsonResult GetProductsJson()
        {
            var productItems = GetProductItems();

            return Json(productItems.ToJson(), JsonRequestBehavior.AllowGet);
        }
    }
}
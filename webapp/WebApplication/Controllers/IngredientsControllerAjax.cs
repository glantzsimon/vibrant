using System;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    public partial class IngredientsController
    {
        public JsonResult UpdateIngredientPriorities(int newId, int oldId, int newDisplayIndex, int oldDisplayIndex)
        {
            try
            {
                _ingredientService.UpdateIngredientPriorities(newId, oldId, newDisplayIndex, oldDisplayIndex);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
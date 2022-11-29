using K9.Base.WebApplication.Controllers;
using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.WebApplication.Extensions;
using System.Web.Mvc;
using K9.WebApplication.Helpers;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class IngredientsController : BaseController<Ingredient>
	{
		public IngredientsController(IControllerPackage<Ingredient> controllerPackage) : base(controllerPackage)
		{
            RecordBeforeCreated += IngredientsController_RecordBeforeCreated;
            RecordBeforeUpdated += IngredientsController_RecordBeforeUpdated;
            RecordBeforeUpdate += IngredientsController_RecordBeforeUpdate;
		}
        
        public ActionResult Main()
	    {
	        return View();
	    }

        private void IngredientsController_RecordBeforeUpdated(object sender, CrudEventArgs e)
        {
            var ingredient = e.Item as Ingredient;
            var original = Repository.Find(ingredient.Id);
            var titleHasChanged = original.Title != ingredient.Title;
            if (string.IsNullOrEmpty(ingredient.SeoFriendlyId) || titleHasChanged && original.SeoFriendlyId == original.Title.ToSeoFriendlyString())
            {
                ingredient.SeoFriendlyId = ingredient.Title.ToSeoFriendlyString();
            }
            HtmlParser.ParseHtml(ref ingredient);
        }

        private void IngredientsController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var ingredient = e.Item as Ingredient;
            if (string.IsNullOrEmpty(ingredient.SeoFriendlyId))
            {
                ingredient.SeoFriendlyId = ingredient.Title.ToSeoFriendlyString();
            }
            HtmlParser.ParseHtml(ref ingredient);
        }

	    private void IngredientsController_RecordBeforeUpdate(object sender, CrudEventArgs e)
	    {
	        var ingredient = e.Item as Ingredient;
            HtmlParser.ParseHtml(ref ingredient);
	    }
	}
}

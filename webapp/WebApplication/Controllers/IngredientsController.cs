using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.WebApplication.Extensions;
using System.Linq;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class IngredientsController : HtmlControllerBase<Ingredient>
	{
		public IngredientsController(IControllerPackage<Ingredient> controllerPackage) : base(controllerPackage)
		{
            RecordBeforeCreated += IngredientsController_RecordBeforeCreated;
            RecordBeforeUpdated += IngredientsController_RecordBeforeUpdated;
		}
        
	    [Route("ingredient/{seoFriendlyId}")]
        public ActionResult Info(string seoFriendlyId)
	    {
	        var ingredient = Repository.Find(e => e.SeoFriendlyId == seoFriendlyId).FirstOrDefault();
	        if (ingredient == null)
	        {
	            return HttpNotFound();
	        }
	        return View(ingredient);;
	    }

	    [Route("ingredient/full-list")]
	    public ActionResult FullList(string seoFriendlyId)
	    {
	        return View(Repository.List());;
	    }

        private void IngredientsController_RecordBeforeUpdated(object sender, CrudEventArgs e)
        {
            var ingredient = e.Item as Ingredient;
            var original = Repository.Find(ingredient.Id);
            var titleHasChanged = original.Name != ingredient.Name;
            if (string.IsNullOrEmpty(ingredient.SeoFriendlyId) || titleHasChanged && original.SeoFriendlyId == original.Name.ToSeoFriendlyString())
            {
                ingredient.SeoFriendlyId = ingredient.Name.ToSeoFriendlyString();
            }
        }

        private void IngredientsController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var ingredient = e.Item as Ingredient;
            if (string.IsNullOrEmpty(ingredient.SeoFriendlyId))
            {
                ingredient.SeoFriendlyId = ingredient.Name.ToSeoFriendlyString();
            }
        }
	}
}

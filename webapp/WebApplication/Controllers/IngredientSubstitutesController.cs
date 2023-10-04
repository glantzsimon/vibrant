using K9.Base.WebApplication.Controllers;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.Base.WebApplication.ViewModels;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Models;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
	[RequirePermissions(Role = RoleNames.Administrators)]
	public class IngredientSubstitutesController : BaseController<IngredientSubstitute>
	{
		private readonly IRepository<Ingredient> _ingredientsRepository;
		
		public IngredientSubstitutesController(IControllerPackage<IngredientSubstitute> controllerPackage, IRepository<Ingredient> ingredientsRepository)
			: base(controllerPackage)
		{
		    _ingredientsRepository = ingredientsRepository;
		}

	    public override ActionResult Index()
	    {
	        return RedirectToAction("Index", "Ingredients");
	    }

		[RequirePermissions(Permission = Permissions.Edit)]
		public ActionResult EditSubstitutesForIngredient(int id = 0)
		{
			return EditMultiple<Ingredient, Ingredient>(_ingredientsRepository.Find(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[RequirePermissions(Permission = Permissions.Edit)]
		public ActionResult EditSubstitutesForIngredient(MultiSelectViewModel model)
		{
			return EditMultiple<Ingredient, Ingredient>(model);
		}

	}
}

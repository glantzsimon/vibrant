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
	public class ProductPackProductsController : BaseController<ProductPackProduct>
	{
		private readonly IRepository<ProductPack> _productPackRepository;

	    public ProductPackProductsController(IControllerPackage<ProductPackProduct> controllerPackage, IRepository<Product> productsRepository, IRepository<ProductPack> productPackRepository)
			: base(controllerPackage)
	    {
	        _productPackRepository = productPackRepository;
	    }

	    public override ActionResult Index()
	    {
	        return RedirectToAction("Index", "ProductPacks");
	    }

		[RequirePermissions(Permission = Permissions.Edit)]
		public ActionResult EditProductsForProductPack(int id = 0)
		{
			return EditMultiple<ProductPack, Product>(_productPackRepository.Find(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[RequirePermissions(Permission = Permissions.Edit)]
		public ActionResult EditProductsForProductPack(MultiSelectViewModel model)
		{
			return EditMultiple<ProductPack, Product>(model);
		}

	}
}

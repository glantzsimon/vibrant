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
	public class OrderItemProductsController : BaseController<OrderItemProduct>
	{
	    private readonly IRepository<OrderItem> _orderItemsRepository;

	    public OrderItemProductsController(IControllerPackage<OrderItemProduct> controllerPackage, IRepository<OrderItem> orderItemsRepository)
			: base(controllerPackage)
	    {
	        _orderItemsRepository = orderItemsRepository;
	    }

	    public override ActionResult Index()
	    {
	        return RedirectToAction("Index", "Products");
	    }

		[RequirePermissions(Permission = Permissions.Edit)]
		public ActionResult EditProductsForOrderItem(int id = 0)
		{
			return EditMultiple<OrderItem, Product>(_orderItemsRepository.Find(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[RequirePermissions(Permission = Permissions.Edit)]
		public ActionResult EditProductsForProductPack(MultiSelectViewModel model)
		{
			return EditMultiple<OrderItem, Product>(model);
		}

	}
}

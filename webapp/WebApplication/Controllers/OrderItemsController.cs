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
	public class OrderItemsController : BaseController<OrderItem>
	{
	    private readonly IRepository<Order> _ordersRepository;
	    
		public OrderItemsController(IControllerPackage<OrderItem> controllerPackage, IRepository<Order> ordersRepository)
			: base(controllerPackage)
		{
		    _ordersRepository = ordersRepository;
		}

	    public override ActionResult Index()
	    {
	        return RedirectToAction("Index", "Orders");
	    }

	    public ActionResult EditProducts(int id = 0)
	    {
	        return RedirectToAction("EditProductsForOrderItem", "OrderItemProducts", new { id });
	    }

	    public ActionResult EditProductPacks(int id = 0)
	    {
	        return RedirectToAction("EditProductPacksForOrderItem", "OrderItemProductPacks", new { id });
	    }
	}
}

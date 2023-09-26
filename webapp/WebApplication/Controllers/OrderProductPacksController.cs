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
	public class OrderProductPacksController : BaseController<OrderProductPack>
	{
	    private readonly IRepository<Order> _ordersRepository;

	    public OrderProductPacksController(IControllerPackage<OrderProductPack> controllerPackage, IRepository<Order> ordersRepository)
			: base(controllerPackage)
	    {
	        _ordersRepository = ordersRepository;
	    }

	    public override ActionResult Index()
	    {
	        return RedirectToAction("Index", "Orders");
	    }

        
	    [RequirePermissions(Permission = Permissions.Edit)]
	    public ActionResult EditProductPacksForOrder(int id = 0)
	    {
	        return EditMultiple<Order, ProductPack>(_ordersRepository.Find(id));
	    }

	    [HttpPost]
	    [ValidateAntiForgeryToken]
	    [RequirePermissions(Permission = Permissions.Edit)]
	    public ActionResult EditProductPacksForOrder(MultiSelectViewModel model)
	    {
	        return EditMultiple<Order, ProductPack>(model);
	    }
	}
}

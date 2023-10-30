using K9.Base.WebApplication.Controllers;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.Base.WebApplication.ViewModels;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Models;
using System.Web.Mvc;
using K9.Base.WebApplication.Extensions;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class OrderProductsController : BaseController<OrderProduct>
    {
        private readonly IRepository<Order> _ordersRepository;

        public OrderProductsController(IControllerPackage<OrderProduct> controllerPackage, IRepository<Order> ordersRepository)
            : base(controllerPackage)
        {
            _ordersRepository = ordersRepository;

            RecordEditMultipleUpdated += OrderProductsController_RecordEditMultipleUpdated;
        }

        public override ActionResult Index()
        {
            return RedirectToAction("Index", "Orders");
        }

        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditProductsForOrder(int id = 0)
        {
            return EditMultiple<Order, Product>(_ordersRepository.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditProductsForOrder(MultiSelectViewModel model)
        {
            return EditMultiple<Order, Product>(model);
        }

        private void OrderProductsController_RecordEditMultipleUpdated(object sender, Base.WebApplication.EventArgs.CrudEventArgs e)
        {
            e.IsRedirect = true;
            e.Controller = typeof(OrdersController).GetControllerName();
            e.Action = nameof(OrdersController.EditProductQuantities);
            e.RouteValues = new { id = e.Item.Id };
        }
    }
}

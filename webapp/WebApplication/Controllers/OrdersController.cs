using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using K9.WebApplication.Services;
using System;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class OrdersController : HtmlControllerBase<Order>
    {
        private readonly IRepository<OrderProduct> _orderProductsRepository;
        private readonly IRepository<OrderProductPack> _orderProductPackRepository;
        private readonly DefaultValuesConfiguration _defaultValues;
        private readonly IOrderService _OrderService;

        public OrdersController(IControllerPackage<Order> controllerPackage, IOptions<DefaultValuesConfiguration> defaultValues, IRepository<OrderProduct> orderProductsRepository, IRepository<OrderProductPack> orderProductPackRepository) : base(controllerPackage)
        {
            _orderProductsRepository = orderProductsRepository;
            _orderProductPackRepository = orderProductPackRepository;
            _defaultValues = defaultValues.Value;
            RecordBeforeCreate += OrdersController_RecordBeforeCreate;
            RecordBeforeCreated += OrdersController_RecordBeforeCreated;
            RecordBeforeDetails += OrdersController_RecordBeforeDetails;
        }
        
        public ActionResult EditProducts(int id = 0)
        {
            return RedirectToAction("EditProductsForOrder", "OrderProducts", new { id });
        }

        public ActionResult EditProductQuantities(int id)
        {
            var order = _OrderService.Find(id);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditProductQuantities(Order model)
        {
            foreach (var product in model.Products)
            {
                var item = _orderProductsRepository.Find(product.Id);
                item.Amount = product.Amount;
                _orderProductsRepository.Update(item);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditProductPacks(int id = 0)
        {
            return RedirectToAction("EditProductPacksForOrder", "OrderProductPacks", new { id });
        }

        public ActionResult EditProductPackQuantities(int id)
        {
            var order = _OrderService.Find(id);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditProductPackQuantities(Order model)
        {
            foreach (var pack in model.ProductPacks)
            {
                var item = _orderProductPackRepository.Find(pack.Id);
                item.Amount = pack.Amount;
                _orderProductPackRepository.Update(item);
            }

            return RedirectToAction("Index");
        }

        public ActionResult View(int orderId)
        {
            return RedirectToAction("Details", null, new { id = orderId });
        }

        [Route("orders/export/csv")]
        public ActionResult DownloadOrdersCsv()
        {
            //var Orders = _OrderService.List(true);
            //var OrderItems = new List<OrderItem>();

            //foreach (var Order in Orders)
            //{
            //    var OrderItem = Order.MapTo<OrderItem>();
            //    OrderItems.Add(OrderItem);
            //}

            //var data = OrderItems.ToCsv();

            //Response.Clear();
            //Response.ContentType = "application/CSV";
            //Response.AddHeader("content-disposition", $"attachment; filename=\"Orders.csv\"");
            //Response.Write(data);
            //Response.End();

            return new EmptyResult();
        }

        private void OrdersController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var order = e.Item as Order;
            order.ExternalId = Guid.NewGuid();
        }

        private void OrdersController_RecordBeforeCreate(object sender, CrudEventArgs e)
        {
            var order = e.Item as Order;

            order.RequestedOn = DateTime.Now;
            order.DueBy = DateTime.Today.AddDays(11);

            int.TryParse(_defaultValues.DefaultUserId, out var userId);
            order.UserId = userId > 0 ? userId : 3;
        }

        private void OrdersController_RecordBeforeDetails(object sender, CrudEventArgs e)
        {
            var order = e.Item as Order;
            order = _OrderService.GetFullOrder(order);
        }
    }
}


using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.Base.WebApplication.ViewModels;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class OrdersController : HtmlControllerBase<Order>
    {
        private readonly IRepository<OrderItem> _orderItemsRepository;
        private readonly IOrderService _OrderService;
        
        public OrdersController(IControllerPackage<Order> controllerPackage, IRepository<OrderItem> orderItemsRepository): base(controllerPackage)
        {
            _orderItemsRepository = orderItemsRepository;
            RecordBeforeCreate += OrdersController_RecordBeforeCreate;
            RecordBeforeCreated += OrdersController_RecordBeforeCreated;
            RecordBeforeDetails += OrdersController_RecordBeforeDetails;
        }
        
        public ActionResult LabSheet(int OrderId = 0, int id = 0, int index = 0)
        {
            if (OrderId == 0)
            {
                OrderId = id;
            }

            var Order = index == 1 ? _OrderService.FindNext(OrderId) : index == -1 ? _OrderService.FindPrevious(OrderId) : _OrderService.Find(OrderId);

            return View(Order);
        }
      
        public ActionResult EditOrderItems(int id = 0)
        {
            return RedirectToAction("EditOrderItemsForOrder", "OrderItems", new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditOrderItemsForOrder(MultiSelectViewModel model)
        {
            return EditMultiple<Order, OrderItem>(model);
        }
        
        public ActionResult View(int orderId)
        {
            return RedirectToAction("Details", null, new { id = orderId });
        }

        [Route("export/csv")]
        public ActionResult DownloadOrdersCsv()
        {
            var Orders = _OrderService.List(true);
            var OrderItems = new List<OrderItem>();

            foreach (var Order in Orders)
            {
                var OrderItem = Order.MapTo<OrderItem>();
                OrderItems.Add(OrderItem);
            }

            var data = OrderItems.ToCsv();

            Response.Clear();
            Response.ContentType = "application/CSV";
            Response.AddHeader("content-disposition", $"attachment; filename=\"Orders.csv\"");
            Response.Write(data);
            Response.End();

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
        }

        private void OrdersController_RecordBeforeDetails(object sender, CrudEventArgs e)
        {
            var order = e.Item as Order;
            order = _OrderService.GetFullOrder(order);
        }
    }
}


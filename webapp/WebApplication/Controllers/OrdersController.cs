using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using K9.WebApplication.Models;
using K9.WebApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using K9.DataAccessLayer.Attributes;
using ServiceStack.Text;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class OrdersController : HtmlControllerBase<Order>
    {
        private readonly IRepository<OrderProduct> _orderProductsRepository;
        private readonly IRepository<OrderProductPack> _orderProductPackRepository;
        private readonly IOrderService _orderService;
        private readonly DefaultValuesConfiguration _defaultValues;

        public OrdersController(IControllerPackage<Order> controllerPackage, IOptions<DefaultValuesConfiguration> defaultValues, IRepository<OrderProduct> orderProductsRepository, IRepository<OrderProductPack> orderProductPackRepository, IOrderService orderService) : base(controllerPackage)
        {
            _orderProductsRepository = orderProductsRepository;
            _orderProductPackRepository = orderProductPackRepository;
            _orderService = orderService;
            _defaultValues = defaultValues.Value;
            RecordBeforeCreate += OrdersController_RecordBeforeCreate;
            RecordBeforeCreated += OrdersController_RecordBeforeCreated;
            RecordBeforeDetails += OrdersController_RecordBeforeDetails;
            RecordBeforeUpdate += OrdersController_RecordBeforeUpdate;
            RecordBeforeUpdated += OrdersController_RecordBeforeUpdated;
            RecordBeforeDelete += OrdersController_RecordBeforeDelete;
            RecordBeforeDeleted += OrdersController_RecordBeforeDeleted;
        }

        public ActionResult EditProducts(int id = 0)
        {
            return RedirectToAction("EditProductsForOrder", "OrderProducts", new { id });
        }

        public ActionResult EditProductQuantities(int id)
        {
            var order = _orderService.Find(id);
            order = _orderService.FillZeroQuantities(order);
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
                item.PriceTier = product.PriceTier;
                _orderProductsRepository.Update(item);
            }

            return RedirectToAction("Details", new { id = model.Id });
        }

        public ActionResult EditProductPacks(int id = 0)
        {
            return RedirectToAction("EditProductPacksForOrder", "OrderProductPacks", new { id });
        }

        public ActionResult EditProductPackQuantities(int id)
        {
            var order = _orderService.Find(id);
            order = _orderService.FillZeroQuantities(order);
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
                item.PriceTier = pack.PriceTier;
                _orderProductPackRepository.Update(item);
            }

            return RedirectToAction("Details", new { id = model.Id });
        }

        public ActionResult View(int orderId)
        {
            return RedirectToAction("Details", null, new { id = orderId });
        }

        public ActionResult OrderReview(int orderId = 0, int id = 0, int index = 0)
        {
            if (orderId == 0)
            {
                orderId = id;
            }

            var order = index == 1 ? _orderService.FindNext(orderId) : index == -1 ? _orderService.FindPrevious(orderId) : _orderService.Find(orderId);

            return View(order);
        }

        public ActionResult DuplicateOrder(int id)
        {
            var duplicate = _orderService.Duplicate(id);
            return RedirectToAction("Edit", new { id = duplicate.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult UpdateOrderProgress(Order order)
        {
            if (order.Products != null)
            {
                foreach (var orderProduct in order.Products)
                {
                    var item = _orderProductsRepository.Find(orderProduct.Id);
                    item.AmountCompleted = orderProduct.AmountCompleted;
                    _orderProductsRepository.Update(item);
                }
            }

            if (order.ProductPacks != null)
            {
                foreach (var pack in order.ProductPacks)
                {
                    var item = _orderProductPackRepository.Find(pack.Id);
                    item.AmountCompleted = pack.AmountCompleted;
                    _orderProductPackRepository.Update(item);
                }
            }

            if (order.AreAllItemsReady())
            {
                var item = Repository.Find(order.Id);
                item.MadeOn = DateTime.Today;
                Repository.Update(item);
            }

            return RedirectToAction("OrderReview", new { orderId = order.Id });
        }

        [NoCache]
        [Route("orders/exportsingle/csv")]
        public ActionResult DownloadOrderCsv(int id)
        {
            var order = _orderService.Find(id);
            var orderItems = new List<OrderItem>();
            orderItems.Add(GetOrderItem(order));

            var data = orderItems.ToCsv();
            return ExportToCsv(data, "Order.csv");
        }

        [NoCache]
        [Route("orders/export/csv")]
        public ActionResult DownloadOrdersCsv()
        {
            var orders = _orderService.List(true).Where(e => !e.IsPaid).ToList();
            var orderItems = new List<OrderItem>();

            foreach (var order in orders)
            {
                orderItems.Add(GetOrderItem(order));
            }

            var data = orderItems.ToCsv();
            return ExportToCsv(data, "Orders.csv");
        }

        private OrderItem GetOrderItem(Order order)
        {
            var orderItem = order.MapTo<OrderItem>();
            orderItem.InvoiceDate = DateTime.Today.ToShortDateString();
            return orderItem;
        }

        [NoCache]
        private ActionResult ExportToCsv(string data, string fileName)
        {
            Response.Clear();
            Response.ContentType = "application/CSV";
            Response.AddHeader("content-disposition", $"attachment; filename=\"{fileName}\"");
            Response.Write(data);
            Response.End();

            return new EmptyResult();
        }

        private void UpdateOrderNumberIfEmpty(Order order)
        {
            if (string.IsNullOrEmpty(order.OrderNumber))
            {
                var orderNumberCount = order.Id + Order.OrderNumberRoot;
                order.OrderNumber = $"PA-{orderNumberCount}";
            }
        }

        private void OrdersController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var order = e.Item as Order;
            order.FullName = order.GetFullName();
            order.ExternalId = Guid.NewGuid();
        }

        private void OrdersController_RecordBeforeCreate(object sender, CrudEventArgs e)
        {
            var order = e.Item as Order;
            order.RequestedOn = DateTime.Now;
            order.DueBy = DateTime.Today.AddDays(11);
            
            int.TryParse(_defaultValues.DefaultUserId, out var userId);
            order.UserId = userId > 0 ? userId : 3;

            UpdateOrderNumberIfEmpty(order);
        }

        private void OrdersController_RecordBeforeDetails(object sender, CrudEventArgs e)
        {
            var order = e.Item as Order;
            order = _orderService.GetFullOrder(order);
        }

        private void OrdersController_RecordBeforeUpdate(object sender, CrudEventArgs e)
        {
            var order = e.Item as Order;
            order = _orderService.GetFullOrder(order);
            UpdateOrderNumberIfEmpty(order);
        }

        private void OrdersController_RecordBeforeUpdated(object sender, CrudEventArgs e)
        {
            var order = e.Item as Order;
            order.FullName = order.GetFullName();
        }

        private void OrdersController_RecordBeforeDelete(object sender, CrudEventArgs e)
        {
            var order = e.Item as Order;
            order = _orderService.GetFullOrder(order);
        }

        private void OrdersController_RecordBeforeDeleted(object sender, CrudEventArgs e)
        {
            _orderService.DeleteChildRecords(e.Item.Id);
        }
    }
}


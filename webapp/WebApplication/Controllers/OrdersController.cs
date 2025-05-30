﻿using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Extensions;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Attributes;
using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using K9.WebApplication.Helpers;
using K9.WebApplication.Models;
using K9.WebApplication.Packages;
using K9.WebApplication.Services;
using K9.WebApplication.ViewModels;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public partial class OrdersController : HtmlControllerBase<Order>
    {
        private readonly IRepository<OrderProduct> _orderProductsRepository;
        private readonly IRepository<OrderProductPack> _orderProductPackRepository;
        private readonly IRepository<OrderProductPackProduct> _orderProductPackProductsRepository;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IPureControllerPackage _pureControllerPackage;
        private readonly DefaultValuesConfiguration _defaultValues;

        public OrdersController(IControllerPackage<Order> controllerPackage, IOptions<DefaultValuesConfiguration> defaultValues, IRepository<OrderProduct> orderProductsRepository, IRepository<OrderProductPack> orderProductPackRepository, IRepository<OrderProductPackProduct> orderProductPackProductsRepository, IOrderService orderService, IProductService productService, IPureControllerPackage pureControllerPackage) : base(controllerPackage, pureControllerPackage)
        {
            _orderProductsRepository = orderProductsRepository;
            _orderProductPackRepository = orderProductPackRepository;
            _orderProductPackProductsRepository = orderProductPackProductsRepository;
            _orderService = orderService;
            _productService = productService;
            _pureControllerPackage = pureControllerPackage;
            _defaultValues = defaultValues.Value;
            RecordBeforeCreate += OrdersController_RecordBeforeCreate;
            RecordBeforeCreated += OrdersController_RecordBeforeCreated;
            RecordBeforeDetails += OrdersController_RecordBeforeDetails;
            RecordBeforeUpdate += OrdersController_RecordBeforeUpdate;
            RecordBeforeUpdated += OrdersController_RecordBeforeUpdated;
            RecordBeforeDelete += OrdersController_RecordBeforeDelete;
            RecordBeforeDeleted += OrdersController_RecordBeforeDeleted;

            RecordUpdated += OrdersController_RecordUpdated;
            RecordCreated += OrdersController_RecordCreated;
            RecordDeleted += OrdersController_RecordDeleted;
        }

        public override ActionResult Index()
        {
            var allOrders = _orderService.List(false, true).Where(e => e.OrderType != EOrderType.ShoppingCart).ToList();
            var ordersViewModel = new OrdersReviewViewModel(allOrders);

            return View(ordersViewModel);
        }

        public ActionResult EditProducts(int id = 0)
        {
            return RedirectToAction("EditProductsForOrder", "OrderProducts", new { id });
        }

        public ActionResult EditProductQuantities(int id)
        {
            var order = _orderService.Find(id);
            order = _orderService.UpdatePricesForClient(order);
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

            _orderService.ClearCache();

            return RedirectToAction("Index");
        }

        public ActionResult EditProductPacks(int id = 0)
        {
            var order = _orderService.Find(id);
            if (order.OrderType == EOrderType.ShopProvision || order.OrderType == EOrderType.Restock)
            {
                return HttpForbidden();
            }

            return RedirectToAction("EditProductPacksForOrder", "OrderProductPacks", new { id });
        }

        public ActionResult EditProductPackQuantities(int id)
        {
            var order = _orderService.Find(id);
            order = _orderService.UpdatePricesForClient(order);
            order = _orderService.FillZeroQuantities(order);
            _orderService.ClearCache();
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

            return RedirectToAction("Index");
        }

        public ActionResult View(int orderId)
        {
            return RedirectToAction("Details", null, new { id = orderId });
        }

        public ActionResult OrderReview(int selectedOrderId = 0, int id = 0, int index = 0)
        {
            if (selectedOrderId == 0)
            {
                selectedOrderId = id;
            }

            var order = index == 1 ? _orderService.FindNext(selectedOrderId) : index == -1 ? _orderService.FindPrevious(selectedOrderId) : _orderService.Find(selectedOrderId);

            var allOrders = _orderService.List(true).Where(e => e.OrderType != EOrderType.ShoppingCart).ToList();
            var ordersViewModel = new OrdersReviewViewModel(allOrders);

            order = order ?? ordersViewModel.GetOrdersToMake().FirstOrDefault();
            ordersViewModel.SelectedOrder = order;

            return View(ordersViewModel);
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

                    // Order product pack products
                    foreach (var orderProductPackProduct in pack.ProductPackProducts)
                    {
                        var original = _orderProductPackProductsRepository.Find(orderProductPackProduct.Id);
                        if (original != null)
                        {
                            original.AmountCompleted = orderProductPackProduct.AmountCompleted;
                            _orderProductPackProductsRepository.Update(original);
                        }
                        else
                        {
                            var newItem = new OrderProductPackProduct
                            {
                                OrderProductPackId = pack.Id,
                                ProductId = orderProductPackProduct.ProductId,
                                AmountCompleted = orderProductPackProduct.AmountCompleted
                            };
                            _orderProductPackProductsRepository.Create(newItem);
                        }
                    }
                }
            }

            if (order.AreAllItemsReady())
            {
                var item = Repository.Find(order.Id);
                item.MadeOn = DateTime.Today;
                Repository.Update(item);
            }

            _orderService.ClearCache();

            return RedirectToAction("OrderReview", new { id = order.Id });
        }

        [NoCache]
        [Route("orders/exportsingle/csv")]
        public ActionResult DownloadOrderCsv(int id)
        {
            var order = _orderService.Find(id);
            var orderItems = new List<OrderItem>();
            orderItems.Add(GetOrderItem(order));

            var data = orderItems.ToCsv();
            return ExportToCsv(data, $"Order {order.OrderNumber} - {order.Name}.csv");
        }

        [NoCache]
        [Route("orders/exportsingle/gbp/csv")]
        public ActionResult DownloadOrderCsvInGbp(int id)
        {
            var order = _orderService.Find(id);
            var orderItems = new List<OrderItem>();
            orderItems.Add(GetOrderItem(order, true));

            var data = orderItems.ToCsv();
            return ExportToCsv(data, $"Order {order.OrderNumber} - {order.Name}.csv");
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
        
        private OrderItem GetOrderItem(Order order, bool isGbp = false)
        {
            var orderItem = order.MapTo<OrderItem>();
            if (isGbp)
            {
                orderItem.FormattedDiscount = order.GetFormattedTotalDiscountInBritishPounds();
                orderItem.FormattedCustomDiscount = order.GetFormattedCustomDiscountInBritishPounds();
                orderItem.FormattedGrandTotal = order.GetFormattedGrandTotalInBritishPounds();
                orderItem.FormattedSubTotal = order.GetFormattedSubTotalInBritishPounds();
                orderItem.PricesList = order.GetPricesListInBritishPounds();
                orderItem.TotalsList = order.GetTotalsListInBritishPounds();
            }
            else
            {
                orderItem.FormattedDiscount = order.GetFormattedTotalDiscount();
                orderItem.FormattedCustomDiscount = order.GetFormattedCustomDiscount();
                orderItem.FormattedGrandTotal = order.GetFormattedGrandTotal();
                orderItem.FormattedSubTotal = order.GetFormattedSubTotal();
                orderItem.PricesList = order.GetPricesList();
                orderItem.TotalsList = order.GetTotalsList();
            }
            
            orderItem.QuantitiesList = order.GetQuantitiesList();
            orderItem.ProductsList = order.GetProductsList();
            orderItem.InvoiceNumbersText = order.GetInvoiceNumbersText();
            orderItem.BulkDiscountText = order.ShopCommission.HasValue && order.ShopCommission.Value > 0 ? $"{Globalisation.Dictionary.ShopCommission} ({order.ShopCommissionAmount}%)" : order.DiscountAmount > 0 ? order.GetBulkDiscountText() : Globalisation.Dictionary.DiscountLabel;
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

        private void OrdersController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var order = e.Item as Order;
            order.FullName = order.GetFullName();
            order.ExternalId = Guid.NewGuid();

            if (order.ClientId.HasValue)
            {
                var client = _pureControllerPackage.ClientService.Find(order.ClientId.Value);

                if (client.ClientDiscount > 0)
                {
                    order.CustomDiscount = client.ClientDiscount;
                }

                if (order.ShopCommission == 0 && client.ShopCommission.HasValue)
                {
                    order.ShopCommission = client.ShopCommission;
                }
            }
        }

        private void OrdersController_RecordBeforeCreate(object sender, CrudEventArgs e)
        {
            var order = e.Item as Order;
            order.RequestedOn = DateTime.Today;
            order.DueBy = DateTime.Today.AddDays(11);
            order.UserId = Current.UserId;
            
            _orderService.UpdateOrderNumberIfEmpty(order);
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
            _orderService.UpdateOrderNumberIfEmpty(order);
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

        private void OrdersController_RecordDeleted(object sender, CrudEventArgs e)
        {
            _orderService.ClearCache();
        }

        private void OrdersController_RecordCreated(object sender, CrudEventArgs e)
        {
            Order order = e.Item as Order;

            e.IsRedirect = true;
            e.Controller = typeof(OrdersController).GetControllerName();
            e.Action = order.IsProductsOnly ? nameof(EditProducts) : nameof(EditProductPacks);
            e.RouteValues = new { id = e.Item.Id };

            _orderService.ClearCache();
        }

        private void OrdersController_RecordUpdated(object sender, CrudEventArgs e)
        {
            _orderService.ClearCache();
        }
    }
}


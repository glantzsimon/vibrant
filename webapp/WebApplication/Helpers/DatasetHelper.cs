using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;

namespace K9.WebApplication.Helpers
{
    public static class DatasetHelper
    {
        public static List<ListItem> OrdersDropdownData { get; set; }

        public static void LoadDatasets(IRepository<Order> ordersRepository)
        {
            var orders = ordersRepository.List()
                .Where(e => !e.IsComplete && e.OrderType != EOrderType.ShoppingCart);

            orders.ForEach(e => e.Name = e.GetFullName());

            OrdersDropdownData =
                GetListItems(
                    orders.Where(e => e.GetOrderStatus() == EOrderStatus.InPreparation).ToList(),
                    orders.Where(e => e.GetOrderStatus() == EOrderStatus.InProgress).ToList(),
                    orders.Where(e => e.GetOrderStatus() == EOrderStatus.AwaitingPayment).ToList(),
                    orders.Where(e => e.GetOrderStatus() == EOrderStatus.ReadyForDelivery).ToList());
        }

        private static List<ListItem> GetListItems<T>(List<T> items,
            List<T> items2 = null,
            List<T> items3 = null,
            List<T> items4 = null,
            List<T> items5 = null,
            List<T> items6 = null,
            List<T> items7 = null) where T : class, IObjectBase
        {
            var listItems = new List<ListItem>();

            listItems.AddRange(items.Select(e => new ListItem(e.Id, e.Name)));

            if (items2 != null && items2.Any())
            {
                AddListSeparator(listItems);
                listItems.AddRange(items2.Select(e => new ListItem(e.Id, e.Name)));
            };
            if (items3 != null && items3.Any())
            {
                AddListSeparator(listItems);
                listItems.AddRange(items3.Select(e => new ListItem(e.Id, e.Name)));
            };
            if (items4 != null && items4.Any())
            {
                AddListSeparator(listItems);
                listItems.AddRange(items4.Select(e => new ListItem(e.Id, e.Name)));
            };
            if (items5 != null && items5.Any())
            {
                AddListSeparator(listItems);
                listItems.AddRange(items5.Select(e => new ListItem(e.Id, e.Name)));
            };
            if (items6 != null && items6.Any())
            {
                AddListSeparator(listItems);
                listItems.AddRange(items6.Select(e => new ListItem(e.Id, e.Name)));
            };
            if (items7 != null && items7.Any())
            {
                AddListSeparator(listItems);
                listItems.AddRange(items7.Select(e => new ListItem(e.Id, e.Name)));
            };

            return listItems;
        }

        private static void AddListSeparator(List<ListItem> listItems)
        {
            listItems.Add(new ListItem(-1, "---"));
        }
    }
}
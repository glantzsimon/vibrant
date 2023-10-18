using K9.DataAccessLayer.Models;
using System;
using System.Collections.Generic;

namespace K9.WebApplication.Services
{
    public interface IOrderService : ICacheableService
    {
        Order Find(int id);
        Order FindPrevious(int id);
        Order FindNext(int id);
        Order Find(Guid id);
        Order Find(string orderNumber);
        Order GetFullOrder(Order order);
        Order FillZeroQuantities(Order order);
        Order UpdatePricesForContact(Order order);
        Order Duplicate(int id);
        void DeleteChildRecords(int id);

        List<Order> List(bool retrieveFullOrder = false);
    }
}
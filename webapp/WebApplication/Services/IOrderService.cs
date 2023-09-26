using K9.DataAccessLayer.Models;
using System;
using System.Collections.Generic;

namespace K9.WebApplication.Services
{
    public interface IOrderService
    {
        Order Find(int id);
        Order FindPrevious(int id);
        Order FindNext(int id);
        Order Find(Guid id);
        Order GetFullOrder(Order order);
        Order FillZeroQuantities(Order order);

        List<Order> List(bool retrieveFullOrder = false, bool includeCustomOrders = false);
    }
}
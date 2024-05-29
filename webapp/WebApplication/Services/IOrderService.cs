using K9.DataAccessLayer.Models;
using K9.WebApplication.Models;
using K9.WebApplication.ViewModels;
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
        Order UpdatePricesForClient(Order order);
        Order Duplicate(int id);
        RepCommissionViewModel CalculateRepCommission(int repId);
        void DeleteChildRecords(int id);
        void UpdateOrderNumberIfEmpty(Order order);
        void ProcessInvoicePayment(PurchaseModel purchaseModel);

        /// <summary>
        /// Update product pack amount and set to 1, if 0. This is the default behaviour when selecting product packs for the first time.
        /// </summary>
        void UpdateProductPacksSetDefaultAmountIfZero(int orderId);

        List<Order> List(bool retrieveFullOrder = false, bool retrieveCompleteOrders = false);
        List<Order> ListForClient(int clientId);
    }
}
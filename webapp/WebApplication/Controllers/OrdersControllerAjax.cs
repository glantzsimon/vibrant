using System;
using System.Linq;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    public partial class OrdersController
    {
        public JsonResult UpdateOrderIsPaid(int id, bool value)
        {
            try
            {
                var original = Repository.Find(id);

                if (original.IsPaid != value)
                {
                    if (value)
                    {
                        original.PaidOn = DateTime.Today;
                    }
                    else
                    {
                        original.PaidOn = null;
                    }
                }

                Repository.Update(original);
                _orderService.ClearCache();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public JsonResult UpdateOrderIsMade(int id, bool value)
        {
            try
            {
                var original = Repository.Find(id);
                var valueHasChanged = original.IsMade != value;

                if (valueHasChanged)
                {
                    if (value)
                    {
                        original.MadeOn = DateTime.Today;
                    }
                    else
                    {
                        original.MadeOn = null;
                    }

                    Repository.Update(original);
                    
                    if (value)
                    {
                        // Update children
                        var orderProducts = _orderProductsRepository.Find(e => e.OrderId == id).ToList();
                        foreach (var orderProduct in orderProducts)
                        {
                            orderProduct.AmountCompleted = orderProduct.Amount;
                            _orderProductsRepository.Update(orderProduct);
                        }
                    }

                    _orderService.ClearCache();
                }
                
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public JsonResult UpdateOrderIsComplete(int id, bool value)
        {
            try
            {
                var original = Repository.Find(id);

                if (original.IsComplete != value)
                {
                    if (value)
                    {
                        original.CompletedOn = DateTime.Today;
                    }
                    else
                    {
                        original.CompletedOn = null;
                    }
                }
                Repository.Update(original);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
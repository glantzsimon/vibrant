using System;
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
                        original.PaidOn = DateTime.Now;
                    }
                    else
                    {
                        original.PaidOn = null;
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

        public JsonResult UpdateOrderIsMade(int id, bool value)
        {
            try
            {
                var original = Repository.Find(id);

                if (original.IsMade != value)
                {
                    if (value)
                    {
                        original.MadeOn = DateTime.Now;
                    }
                    else
                    {
                        original.MadeOn = null;
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
        
        public JsonResult UpdateOrderIsComplete(int id, bool value)
        {
            try
            {
                var original = Repository.Find(id);

                if (original.IsComplete != value)
                {
                    if (value)
                    {
                        original.CompletedOn = DateTime.Now;
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
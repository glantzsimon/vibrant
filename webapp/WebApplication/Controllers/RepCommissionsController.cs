using System;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using K9.WebApplication.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    public class RepCommissionsController :  HtmlControllerBase<RepCommission>
	{
	    private readonly IRepository<RepCommission> _repCommissionsRepository;
	    private readonly IOrderService _orderService;

	    public RepCommissionsController(IControllerPackage<RepCommission> controllerPackage, IRepository<RepCommission> repCommissionsRepository, IOrderService orderService) : base(controllerPackage)
	    {
	        _repCommissionsRepository = repCommissionsRepository;
	        _orderService = orderService;
	    }

	    public ActionResult Review(int repId = 0)
	    {
	        if (repId == 0)
	        {
	            repId = _repCommissionsRepository.List().FirstOrDefault()?.Id ?? 0;
	        }

	        var model = _orderService.CalculateRepCommission(repId);
	        
	        return View(model);
	    }

	    [HttpPost]
	    [ValidateAntiForgeryToken]
	    public ActionResult Redeem(RepCommissionViewModel model)
	    {
            _repCommissionsRepository.Create(new RepCommission
            {
                RepId = model.RepId,
                RedeemedOn = DateTime.Today,
                AmountRedeemed = model.AmountRedeemable
            });
	        return RedirectToAction("Review", model.RepId);
	    }
	}
}

using K9.Base.WebApplication.Controllers;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class PromoCodesController : BaseController<PromoCode>
    {

        public PromoCodesController(IControllerPackage<PromoCode> controllerPackage)
            : base(controllerPackage)
        {
        }

        public ActionResult CreateMultiple()
        {
            return View(new PromoCode());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMultiple(PromoCode promoCode)
        {
            for (int i = 0; i < promoCode.NumberToCreate; i++)
            {
                var newPromoCode = new PromoCode
                {
                    SubscriptionType = promoCode.SubscriptionType,
                    Credits = promoCode.Credits
                };

                Repository.Create(newPromoCode);
            }

            return RedirectToAction("Index");
        }
    }
}
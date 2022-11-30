using K9.Base.WebApplication.Controllers;
using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.WebApplication.Extensions;
using System;
using System.Linq;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
	public class ProductsController : BaseController<Product>
	{
		public ProductsController(IControllerPackage<Product> controllerPackage) : base(controllerPackage)
		{
		    RecordBeforeCreate += ProductsController_RecordBeforeCreate;
            RecordBeforeCreated += ProductsController_RecordBeforeCreated;
            RecordBeforeUpdated += ProductsController_RecordBeforeUpdated;
		}

	    [Route("product/{seoFriendlyId}")]
	    public ActionResult Info(string seoFriendlyId)
	    {
	        var product = Repository.Find(e => e.SeoFriendlyId == seoFriendlyId).FirstOrDefault();
	        if (product == null)
	        {
	            return HttpNotFound();
	        }
	        return View(product);;
	    }

	    [Route("product/full-list")]
	    public ActionResult FullList(string seoFriendlyId)
	    {
	        return View(Repository.List());;
	    }

        private void ProductsController_RecordBeforeUpdated(object sender, CrudEventArgs e)
        {
            var product = e.Item as Product;
            var original = Repository.Find(product.Id);
            var titleHasChanged = original.Title != product.Title;
            if (string.IsNullOrEmpty(product.SeoFriendlyId) || titleHasChanged && original.SeoFriendlyId == original.Title.ToSeoFriendlyString())
            {
                product.SeoFriendlyId = product.Title.ToSeoFriendlyString();
            }
        }

        private void ProductsController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var product = e.Item as Product;
            if (string.IsNullOrEmpty(product.SeoFriendlyId))
            {
                product.SeoFriendlyId = product.Title.ToSeoFriendlyString();
            }
        }

        void ProductsController_RecordBeforeCreate(object sender, CrudEventArgs e)
	    {
	        var product = e.Item as Product;
	        product.IsLiveOn = DateTime.Now;
	    }
	}
}

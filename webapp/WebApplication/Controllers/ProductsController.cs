using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.Base.WebApplication.ViewModels;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.WebApplication.Extensions;
using K9.WebApplication.Services;
using System;
using System.Linq;
using System.Web.Mvc;
using K9.SharedLibrary.Models;
using WebGrease.Css.Extensions;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class ProductsController : HtmlControllerBase<Product>
    {
        private readonly IProductService _productService;
        private readonly IRepository<ProductIngredient> _productIngredientsRepository;

        public ProductsController(IControllerPackage<Product> controllerPackage, IProductService productService, IRepository<ProductIngredient> productIngredientsRepository) : base(controllerPackage)
        {
            _productService = productService;
            _productIngredientsRepository = productIngredientsRepository;
            RecordBeforeCreate += ProductsController_RecordBeforeCreate;
            RecordBeforeCreated += ProductsController_RecordBeforeCreated;
            RecordBeforeUpdated += ProductsController_RecordBeforeUpdated;
            RecordBeforeDetails += ProductsController_RecordBeforeDetails;
        }
        
        public ActionResult EditIngredientQuantities(int id)
        {
            var product = _productService.Find(id);
            return View(product);
        }

        public ActionResult LabSheet(int id, int index = 0)
        {
            var product = index == 1 ? _productService.FindNext(id) : index == -1 ? _productService.FindPrevious(id) : _productService.Find(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditIngredientQuantities(Product model)
        {
            var totalIngredients = model.Ingredients.Sum(e => e.Amount);
            if (totalIngredients != model.AmountPerServing)
            {
                ModelState.AddModelError("", $"Total ingredients must equal {model.AmountPerServing}. The current value is {totalIngredients}");
                return View(model);
            }

            foreach (var productIngredient in model.Ingredients)
            {
                var item = _productIngredientsRepository.Find(productIngredient.Id);
                item.Amount = productIngredient.Amount;
                _productIngredientsRepository.Update(item);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditIngredients(int id = 0)
        {
            return RedirectToAction("EditIngredientsForProduct", "ProductIngredients", new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditIngredientsForProduct(MultiSelectViewModel model)
        {
            return EditMultiple<Product, Ingredient>(model);
        }

        private void ProductsController_RecordBeforeUpdated(object sender, CrudEventArgs e)
        {
            var product = e.Item as Product;
            var original = Repository.Find(product.Id);
            var titleHasChanged = original.Name != product.Name;
            if (string.IsNullOrEmpty(product.SeoFriendlyId) || titleHasChanged && original.SeoFriendlyId == original.Name.ToSeoFriendlyString())
            {
                product.SeoFriendlyId = product.Name.ToSeoFriendlyString();
            }
        }

        private void ProductsController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var product = e.Item as Product;
            if (string.IsNullOrEmpty(product.SeoFriendlyId))
            {
                product.SeoFriendlyId = product.Name.ToSeoFriendlyString();
            }
        }

        private void ProductsController_RecordBeforeCreate(object sender, CrudEventArgs e)
        {
            var product = e.Item as Product;
            product.IsLiveOn = DateTime.Now;
        }

        private void ProductsController_RecordBeforeDetails(object sender, CrudEventArgs e)
        {
            var product = e.Item as Product;
            product = _productService.GetFullProduct(product);
        }
    }
}

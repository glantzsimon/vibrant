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
using K9.Base.WebApplication.ViewModels;
using K9.SharedLibrary.Models;
using WebGrease.Css.Extensions;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class ProductsController : HtmlControllerBase<Product>
    {
        private readonly IRepository<ProductIngredient> _productIngredientsRepository;
        private readonly IRepository<Ingredient> _ingredientsRepository;

        public ProductsController(IControllerPackage<Product> controllerPackage, IRepository<ProductIngredient> productIngredientsRepository, IRepository<Ingredient> ingredientsRepository) : base(controllerPackage)
        {
            _productIngredientsRepository = productIngredientsRepository;
            _ingredientsRepository = ingredientsRepository;
            RecordBeforeCreate += ProductsController_RecordBeforeCreate;
            RecordBeforeCreated += ProductsController_RecordBeforeCreated;
            RecordBeforeUpdated += ProductsController_RecordBeforeUpdated;
        }

        public ActionResult EditIngredientQuantities(int id)
        {
            var product = Repository.Find(id);
            product.ProductIngredients = _productIngredientsRepository.Find(e => e.ProductId == id).ToList();
            foreach (var productIngredient in product.ProductIngredients)
            {
                productIngredient.Ingredient =
                    _ingredientsRepository.Find(e => e.Id == productIngredient.IngredientId).FirstOrDefault();
            }
            product.Ingredients = product.ProductIngredients.OrderByDescending(e => e.Amount).ThenBy(e => e.Ingredient.Name).ToList();
            
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

        void ProductsController_RecordBeforeCreate(object sender, CrudEventArgs e)
        {
            var product = e.Item as Product;
            product.IsLiveOn = DateTime.Now;
        }
    }
}

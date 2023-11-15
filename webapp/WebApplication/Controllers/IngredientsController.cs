using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Interfaces;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Extensions;
using K9.WebApplication.Models;
using K9.WebApplication.Packages;
using K9.WebApplication.Services;
using ServiceStack.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public partial class IngredientsController : HtmlControllerBase<Ingredient>
    {
        private readonly IIngredientService _ingredientService;
        private readonly IRepository<IngredientSubstitute> _ingredientSubstituteRepository;
        private readonly IOrderService _ordersService;
        private readonly IProductService _productService;

        public IngredientsController(IControllerPackage<Ingredient> controllerPackage, IIngredientService ingredientService, IRepository<IngredientSubstitute> ingredientSubstituteRepository, IOrderService ordersService, IProductService productService, IPureControllerPackage pureControllerPackage) : base(controllerPackage, pureControllerPackage)
        {
            _ingredientService = ingredientService;
            _ingredientSubstituteRepository = ingredientSubstituteRepository;
            _ordersService = ordersService;
            _productService = productService;
            RecordBeforeCreated += IngredientsController_RecordBeforeCreated;
            RecordBeforeUpdated += IngredientsController_RecordBeforeUpdated;
            RecordBeforeDetails += IngredientsController_RecordBeforeDetails;
        }

        public ActionResult EditList()
        {
            return View(Repository.List().OrderBy(e => e.Name).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditList(List<Ingredient> model)
        {
            foreach (var ingredient in model)
            {
                var item = Repository.Find(ingredient.Id);
                item.Name = ingredient.Name;
                item.Cost = ingredient.Cost;
                item.Quantity = ingredient.Quantity;
                item.QuantityInStock = ingredient.QuantityInStock;
                item.Concentration = ingredient.Concentration;
                item.RecommendedDailyAllownace = ingredient.RecommendedDailyAllownace;
                item.Category = ingredient.Category;
                item.ItemCode = ingredient.ItemCode;

                Repository.Update(item);
            }

            _ingredientService.ClearCache();

            return RedirectToAction("EditList");
        }

        public ActionResult IngredientsPurchaseList()
        {
            var orders = _ordersService.List(true);
            var products = _productService.List(true);
            var productsUsed = products.Where(p =>
                orders.SelectMany(e => e.Products).Select(e => e.Product.Id).Contains(p.Id)).ToList();
            var ingredientsUsed = productsUsed.SelectMany(e => e.Ingredients).Select(e => e.IngredientId).ToList();

            var list = products.SelectMany(e => e.Ingredients).GroupBy(e => e.IngredientId).Select(group => new IngredientViewModel
            {
                Ingredient = _ingredientService.Find(group.Key),
                TotalAmountUsed = group.Sum(e => e.Amount),
                TotalProductsUsedIn = products.Count(e => e.Ingredients.Any(i => i.IngredientId == group.Key)),
                TotalOrdersUsedIn = ingredientsUsed.Count(e => e == group.Key)
            }).OrderByDescending(e => e.TotalOrdersUsedIn).ThenByDescending(e => e.TotalProductsUsedIn).ThenBy(e => e.Ingredient.Name).ToList();

            return View(list);
        }

        public ActionResult SelectIngredientsLabels()
        {
            return View(new IngredientsLabelsViewModel(_ingredientService.List(false, true)));
        }

        [HttpPost]
        public ActionResult ViewIngredientsLabels(IngredientsLabelsViewModel model)
        {
            return View("ViewIngredientsLabels", model);
        }

        public ActionResult EditIngredientSubstitutes(int id)
        {
            var ingredient = _ingredientService.FindWithSubstitutesSelectList(id);
            return View(ingredient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditIngredientSubstitutes(Ingredient model)
        {
            _ingredientService.EditIngredientSubstitutes(model);

            return RedirectToAction("Index");
        }

        public ActionResult EditIngredientSubstitutePriorities(int id)
        {
            var ingredient = _ingredientService.Find(id);
            return View(ingredient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditIngredientSubstitutePriorities(Ingredient model)
        {
            foreach (var substitute in model.Substitutes)
            {
                var item = _ingredientSubstituteRepository.Find(substitute.Id);
                item.Priority = substitute.Priority;
                _ingredientSubstituteRepository.Update(item);
            }

            _ingredientService.ClearCache();

            return RedirectToAction("Index");
        }

        [Route("ingredients/export/csv")]
        public ActionResult DownloadIngredientsCsv()
        {
            var ingredients = _ingredientService.List(true);
            var ingredientsItems = new List<IngredientItem>();

            foreach (var ingredient in ingredients)
            {
                var ingredientItem = ingredient.MapTo<IngredientItem>();
                ingredientItem.IngredientTypeText = ingredient.GetIngredientTypeText();
                ingredientsItems.Add(ingredientItem);
            }

            var data = ingredientsItems.ToCsv();

            Response.Clear();
            Response.ContentType = "application/CSV";
            Response.AddHeader("content-disposition", $"attachment; filename=\"Ingredients.csv\"");
            Response.Write(data);
            Response.End();

            return new EmptyResult();
        }

        [Route("ingredients/categories/export/csv")]
        public ActionResult DownloadIngredientCategoriesCsv()
        {
            var ingredients = _ingredientService.List();

            var data = ingredients.Select(e => e.CategoryText).Distinct()
                .Select(e => new CategoryItem
                {
                    Name = e.ToUpper()
                }).ToCsv();

            Response.Clear();
            Response.ContentType = "application/CSV";
            Response.AddHeader("content-disposition", $"attachment; filename=\"IngredientCategories.csv\"");
            Response.Write(data);
            Response.End();

            return new EmptyResult();
        }

        private void IngredientsController_RecordBeforeDetails(object sender, CrudEventArgs e)
        {
            var ingredient = e.Item as Ingredient;
            ingredient = _ingredientService.GetFullIngredient(ingredient);
        }

        private void IngredientsController_RecordBeforeUpdated(object sender, CrudEventArgs e)
        {
            var ingredient = e.Item as Ingredient;
            var original = Repository.Find(ingredient.Id);
            var titleHasChanged = original.Name != ingredient.Name;
            if (string.IsNullOrEmpty(ingredient.SeoFriendlyId) || titleHasChanged && original.SeoFriendlyId == original.Name.ToSeoFriendlyString())
            {
                ingredient.SeoFriendlyId = ingredient.Name.ToSeoFriendlyString();
            }

            var categoryHasChanged = original.Category != ingredient.Category;
            if (categoryHasChanged || original.ItemCode == 0)
            {
                var newItemCode = _ingredientService.GetItemCode(ingredient, new List<ICategorisable>(Repository.List()));
                ingredient.ItemCode = newItemCode;
            }
        }

        private void IngredientsController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var ingredient = e.Item as Ingredient;
            if (string.IsNullOrEmpty(ingredient.SeoFriendlyId))
            {
                ingredient.SeoFriendlyId = ingredient.Name.ToSeoFriendlyString();
            }
            ingredient.ItemCode = _ingredientService.GetItemCode(ingredient, new List<ICategorisable>(Repository.List()));
        }
    }
}

using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Helpers;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Extensions;
using K9.WebApplication.Helpers;
using K9.WebApplication.Models;
using K9.WebApplication.Services;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class ProductsController : HtmlControllerBase<Product>
    {
        private readonly IProductService _productService;
        private readonly IRepository<ProductIngredient> _productIngredientsRepository;
        private readonly IRepository<Ingredient> _ingredientsRepository;

        public ProductsController(IControllerPackage<Product> controllerPackage, IProductService productService, IRepository<ProductIngredient> productIngredientsRepository, IRepository<Ingredient> ingredientsRepository) : base(controllerPackage)
        {
            _productService = productService;
            _productIngredientsRepository = productIngredientsRepository;
            _ingredientsRepository = ingredientsRepository;
            RecordBeforeCreate += ProductsController_RecordBeforeCreate;
            RecordBeforeCreated += ProductsController_RecordBeforeCreated;
            RecordBeforeUpdated += ProductsController_RecordBeforeUpdated;
            RecordBeforeDetails += ProductsController_RecordBeforeDetails;
            RecordBeforeDeleted += ProductsController_RecordBeforeDeleted;
        }

        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult DuplicateProduct(int id)
        {
            return View(Repository.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Create)]
        public ActionResult DuplicateProduct(Product product)
        {
            var duplicate = _productService.Duplicate(product.Id);
            return RedirectToAction("Edit", new { id = duplicate.Id });
        }

        public ActionResult LabSheet(int productId = 0, int id = 0, int index = 0, int batchSize = 1)
        {
            if (productId == 0)
            {
                productId = id;
            }

            var product = index == 1 ? _productService.FindNext(productId) : index == -1 ? _productService.FindPrevious(productId) : _productService.Find(productId);
            if (batchSize > 1)
            {
                product = _productService.UpdateBatchSize(product, batchSize);
            }

            return View(product);
        }

        public ActionResult EditIngredientQuantities(int id)
        {
            var product = _productService.Find(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditIngredientQuantities(Product model)
        {
            if (!model.IngredientAmountsAreCorrect())
            {
                ModelState.AddModelError("", model.GetIngredientAmountIncorrectError());
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

        public ActionResult EditList()
        {
            var products = Repository.List();
            foreach (var product in products)
            {
                _productService.GetFullProduct(product);
                product.Benefits = product.Benefits.RemoveEmptyLines();
                HtmlParser.ParseHtml(product);
            }
            return View(products.OrderBy(e => e.Name).ToList());
        }

        public ActionResult View(int productId)
        {
            return RedirectToAction("Details", null, new { id = productId });
        }

        [Route("products/export/csv")]
        public ActionResult DownloadProductsCsv()
        {
            var products = _productService.List(true);
            var productItems = new List<ProductItem>();

            foreach (var product in products)
            {
                var productItem = product.MapTo<ProductItem>();
                productItems.Add(productItem);
            }

            var data = productItems.ToCsv();

            Response.Clear();
            Response.ContentType = "application/CSV";
            Response.AddHeader("content-disposition", $"attachment; filename=\"Products.csv\"");
            Response.Write(data);
            Response.End();

            return new EmptyResult();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditList(List<Product> model)
        {
            foreach (var product in model)
            {
                var item = Repository.Find(product.Id);
                item.Name = product.Name;
                item.SubTitleLabelText = product.SubTitleLabelText;
                item.Amount = product.Amount;
                item.CostOfMaterials = product.CostOfMaterials;
                item.Price = product.Price;
                item.MinDosage = product.MinDosage;
                item.MaxDosage = product.MaxDosage;
                item.ShortDescription = product.ShortDescription;
                item.Body = product.Body;
                item.Benefits = product.Benefits;
                item.Dosage = product.Dosage;
                item.Recommendations = product.Recommendations;

                HtmlParser.ParseHtml(item);

                Repository.Update(item);
            }

            return RedirectToAction("EditList");
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
            product.ExternalId = Guid.NewGuid();
        }

        private void ProductsController_RecordBeforeCreate(object sender, CrudEventArgs e)
        {
            var product = e.Item as Product;
        }

        private void ProductsController_RecordBeforeDetails(object sender, CrudEventArgs e)
        {
            var product = e.Item as Product;
            product = _productService.GetFullProduct(product);
        }

        private void ProductsController_RecordBeforeDeleted(object sender, CrudEventArgs e)
        {
            _productService.DeleteChildRecords(e.Item.Id);
        }
    }
}


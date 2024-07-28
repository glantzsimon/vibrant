using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Helpers;
using K9.DataAccessLayer.Interfaces;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using K9.WebApplication.Extensions;
using K9.WebApplication.Helpers;
using K9.WebApplication.Models;
using K9.WebApplication.Packages;
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
    public partial class ProductsController : HtmlControllerBase<Product>
    {
        private readonly IProductService _productService;
        private readonly IRepository<ProductIngredient> _productIngredientsRepository;
        private readonly IRepository<Ingredient> _ingredientsRepository;
        private readonly IIngredientService _ingredientService;
        private readonly ApiConfiguration _config;

        public ProductsController(IControllerPackage<Product> controllerPackage, IProductService productService, IRepository<ProductIngredient> productIngredientsRepository, IRepository<Ingredient> ingredientsRepository, IIngredientService ingredientService, IPureControllerPackage pureControllerPackage, IOptions<ApiConfiguration> config) : base(controllerPackage, pureControllerPackage)
        {
            _productService = productService;
            _productIngredientsRepository = productIngredientsRepository;
            _ingredientsRepository = ingredientsRepository;
            _ingredientService = ingredientService;
            _config = config.Value;
            RecordBeforeCreate += ProductsController_RecordBeforeCreate;
            RecordBeforeCreated += ProductsController_RecordBeforeCreated;
            RecordBeforeUpdated += ProductsController_RecordBeforeUpdated;
            RecordBeforeDetails += ProductsController_RecordBeforeDetails;
            RecordBeforeDeleted += ProductsController_RecordBeforeDeleted;

            RecordCreated += ProductsController_RecordCreated;
            RecordUpdated += ProductsController_RecordUpdated;
            RecordDeleted += ProductsController_RecordDeleted;
        }

        public ActionResult ProductQrCodes()
        {
            return View(_productService.List(false));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProductQrCodesQuantities(List<Product> products)
        {
            foreach (var product in products)
            {
                if (product.IsSelected)
                {
                    product.DisplayAmount = 1;
                }
            }
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewProductQrCodes(List<Product> products)
        {
            var selectedIds = products.Where(e => e.IsSelected).Select(e => e.Id).ToList();
            var selectedProducts = _productService.List(false).Where(e => selectedIds.Contains(e.Id)).ToList();

            foreach (var selectedProduct in selectedProducts)
            {
                var original = products.FirstOrDefault(e => e.Id == selectedProduct.Id);
                selectedProduct.DisplayAmount = original.DisplayAmount;
                var productUrl = Url.AbsoluteAction("Details", "Product", new { seoFriendlyId = selectedProduct.SeoFriendlyId });
                selectedProduct.QrCodeUrl = string.Format(_config.QrCodeApiUrl, 111, productUrl);
            }

            return View(selectedProducts);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LabSheet(Product model)
        {
            var product = _productService.Find(model.Id);
            if (model.BatchSize > 1)
            {
                product = _productService.UpdateBatchSize(product, model.BatchSize);
            }

            foreach (var productIngredient in model.IngredientsWithSubstitutes)
            {
                var ingredient = product.IngredientsWithSubstitutes.FirstOrDefault(e =>
                    e.Ingredient.Id == productIngredient.IngredientId);

                if (ingredient != null)
                    ingredient.IsSelected = productIngredient.IsSelected;
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

            _productService.ClearCache();

            return RedirectToAction("Index");
        }

        public ActionResult EditIngredientSubstitutes(int id)
        {
            var product = _productService.Find(id);
            if (!product.AllowIngredientSubstitutes)
            {
                ModelState.AddModelError("", Globalisation.Dictionary.ProductDoesNotAllowSubstitutes);
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditIngredientSubstitutes(Product model)
        {
            _productService.EditIngredientSubstitutes(model);
            _productService.ClearCache();
            return RedirectToAction("Index");
        }

        public ActionResult EditIngredients(int id = 0)
        {
            return View(_productService.FindWithIngredientsSelectList(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditIngredients(Product model)
        {
            _productService.EditIngredients(model);
            _productService.ClearCache();
            return RedirectToAction("Index");
        }

        public ActionResult View(int productId)
        {
            return RedirectToAction("Details", null, new { id = productId });
        }

        [Route("products/export/choose")]
        public ActionResult DownloadProducts()
        {
            return View();
        }

        [Route("products/export/all")]
        public ActionResult DownloadProductsAll()
        {
            return DownloadProductsCsv(_productService.ListProductItemsAll());
        }

        [Route("products/export/capsules100")]
        public ActionResult DownloadProducts100Capsules()
        {
            return DownloadProductsCsv(_productService.ListProductItems100Capsules());
        }

        [Route("products/export/capsules200")]
        public ActionResult DownloadProducts200Capsules()
        {
            return DownloadProductsCsv(_productService.ListProductItems200Capsules());
        }

        [Route("products/export/capsules400")]
        public ActionResult DownloadProducts400Capsules()
        {
            return DownloadProductsCsv(_productService.ListProductItems400Capsules());
        }

        [Route("products/export/liquids")]
        public ActionResult DownloadProductsLiquids()
        {
            return DownloadProductsCsv(_productService.ListProductItemsLiquids());
        }

        [Route("products/export/powders")]
        public ActionResult DownloadProductsPowders()
        {
            return DownloadProductsCsv(_productService.ListProductItemsPowders());
        }

        [Route("products/export/csv")]
        public ActionResult DownloadProductsCsv(List<ProductItem> items)
        {
            var data = items.ToCsv();

            Response.Clear();
            Response.ContentType = "application/CSV";
            Response.AddHeader("content-disposition", $"attachment; filename=\"Products.csv\"");
            Response.Write(data);
            Response.End();

            return new EmptyResult();
        }

        public ActionResult EditList()
        {
            var products = _productService.List(true);
            foreach (var product in products)
            {
                product.Benefits = product.Benefits.RemoveEmptyLines();
                HtmlParser.ParseHtml(product);
            }
            return View(products.OrderBy(e => e.Name).ToList());
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
                item.Category = product.Category;
                item.ItemCode = product.ItemCode;
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

                _productService.ClearCache();
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

            if (original.ItemCode == 0)
            {
                product.ItemCode = _productService.GetItemCode(product, new List<ICategorisable>(Repository.List()));
            }
        }

        private void ProductsController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var product = e.Item as Product;
            if (string.IsNullOrEmpty(product.SeoFriendlyId))
            {
                product.SeoFriendlyId = product.Name.ToSeoFriendlyString();
            }

            product.ItemCode = _productService.GetItemCode(product, new List<ICategorisable>(Repository.List()));
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

        private void ProductsController_RecordDeleted(object sender, CrudEventArgs e)
        {
            _productService.ClearCache();
        }

        private void ProductsController_RecordUpdated(object sender, CrudEventArgs e)
        {
            _productService.ClearCache();
        }

        private void ProductsController_RecordCreated(object sender, CrudEventArgs e)
        {
            _productService.ClearCache();
        }
    }
}


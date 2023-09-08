using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.Base.WebApplication.ViewModels;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Models;
using K9.WebApplication.Extensions;
using K9.WebApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class ProductPacksController : HtmlControllerBase<ProductPack>
    {
        private readonly IProductService _productService;
        private readonly IRepository<ProductPackProduct> _productPackProductRepository;
        private readonly IRepository<Ingredient> _ingredientsRepository;
        private readonly IRepository<ProductPack> _productPackRepository;

        public ProductPacksController(IControllerPackage<ProductPack> controllerPackage, IProductService productService, IRepository<ProductPackProduct> productPackProductRepository, IRepository<Ingredient> ingredientsRepository, IRepository<ProductPack> productPackRepository) : base(controllerPackage)
        {
            _productService = productService;
            _productPackProductRepository = productPackProductRepository;
            _ingredientsRepository = ingredientsRepository;
            _productPackRepository = productPackRepository;
            RecordBeforeCreated += ProductPacksController_RecordBeforeCreated;
            RecordBeforeUpdated += ProductPacksController_RecordBeforeUpdated;
            RecordBeforeEditMultiple += ProductPacksController_RecordBeforeEditMultiple;
            RecordBeforeDetails += ProductPacksController_RecordBeforeDetails;
        }
        
        public ActionResult EditProductQuantities(int id)
        {
            var productPack = _productService.FindPack(id);
            return View(productPack);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditProductQuantities(ProductPack model)
        {
            foreach (var product in model.Products)
            {
                var item = _productPackProductRepository.Find(product.Id);
                item.Amount = product.Amount;
                _productPackProductRepository.Update(item);
            }

            return RedirectToAction("Index");
        }

        public ActionResult EditProducts(int id = 0)
        {
            return RedirectToAction("EditProductsForProductPack", "ProductPackProducts", new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditProductsForProductPack(MultiSelectViewModel model)
        {
            return EditMultiple<ProductPack, Product>(model);
        }

        public ActionResult EditList()
        {
            var productPacks = Repository.List();
            foreach (var pack in productPacks)
            {
                _productService.GetFullProductPack(pack);
            }
            return View(productPacks.OrderBy(e => e.Name).ToList());
        }

        public ActionResult View(int productPackId)
        {
            return RedirectToAction("Details", null, new { id = productPackId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Permission = Permissions.Edit)]
        public ActionResult EditList(List<ProductPack> model)
        {
            foreach (var productPack in model)
            {
                var item = Repository.Find(productPack.Id);
                item.Name = productPack.Name;
                item.Price = productPack.Price;

                Repository.Update(item);
            }

            return RedirectToAction("EditList");
        }

        private void ProductPacksController_RecordBeforeUpdated(object sender, CrudEventArgs e)
        {
            var pack = e.Item as ProductPack;
            var original = Repository.Find(pack.Id);
            var titleHasChanged = original.Name != pack.Name;
            if (string.IsNullOrEmpty(pack.SeoFriendlyId) || titleHasChanged && original.SeoFriendlyId == original.Name.ToSeoFriendlyString())
            {
                pack.SeoFriendlyId = pack.Name.ToSeoFriendlyString();
            }
        }

        private void ProductPacksController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var pack = e.Item as ProductPack;
            if (string.IsNullOrEmpty(pack.SeoFriendlyId))
            {
                pack.SeoFriendlyId = pack.Name.ToSeoFriendlyString();
            }
            pack.ExternalId = Guid.NewGuid();
        }

        private void ProductPacksController_RecordBeforeEditMultiple(object sender, CrudEventArgs e)
        {
            var pack = _productService.FindPack(e.Item.Id);
            foreach (var productPackProduct in pack.Products)
            {
                if (productPackProduct.Amount == 0)
                {
                    var packProductToEdit = _productPackProductRepository.Find(productPackProduct.Id);
                    packProductToEdit.Amount = 1;
                    _productPackProductRepository.Update(packProductToEdit);
                }
            }
        }

        private void ProductPacksController_RecordBeforeDetails(object sender, CrudEventArgs e)
        {
            var productPack = e.Item as ProductPack;
            productPack = _productService.GetFullProductPack(productPack);
        }
    }
}

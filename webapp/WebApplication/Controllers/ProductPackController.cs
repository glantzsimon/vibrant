using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using NLog;
using System;
using System.Web.Mvc;
using K9.WebApplication.Packages;

namespace K9.WebApplication.Controllers
{
    public class ProductPackController : BasePureController
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IProductService _productService;

        public ProductPackController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IRepository<Product> productsRepository, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService, IProductService productService, IPureControllerPackage pureControllerPackage)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, pureControllerPackage)
        {
            _productsRepository = productsRepository;
            _productService = productService;
        }

        [Route("productpack/all")]
        public ActionResult Index()
        {
            var productPacks = _productService.ListProductPacks(true);
            foreach (var productPack in productPacks)
            {
                foreach (var productPackProduct in productPack.Products)
                {
                    LoadUploadedFiles(productPackProduct.Product, false, true);
                }
            }
            return View(productPacks);
        }

        [Route("productpack/{seoFriendlyId}")]
        public ActionResult Details(string seoFriendlyId)
        {
            var productPack = _productService.FindPack(seoFriendlyId);
            if (productPack == null)
            {
                return HttpNotFound();
            }

            foreach (var productPackProduct in productPack.Products)
            {
                LoadUploadedFiles(productPackProduct.Product, false, true);
            }

            LoadUploadedFiles(productPack);
            return View(productPack);
        }

        [Authorize]
        public ActionResult Link(Guid id)
        {
            if (!Roles.CurrentUserIsInRoles(Constants.Constants.UnicornUser) && !Roles.CurrentUserIsInRoles(RoleNames.Administrators))
            {
                return HttpNotFound();
            }

            var productPack = _productService.FindPack(id);
            if (productPack == null)
            {
                return HttpNotFound();
            }
            return View("Link", productPack);
        }

        public override string GetObjectName()
        {
            return typeof(NewsItem).Name;
        }
    }
}

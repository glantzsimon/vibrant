using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using NLog;
using System;
using System.Web.Mvc;
using K9.SharedLibrary.Authentication;

namespace K9.WebApplication.Controllers
{
    public class ProductController : BasePureController
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IProductService _productService;

        public ProductController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IRepository<Product> productsRepository, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService, IProductService productService)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, membershipService)
        {
            _productsRepository = productsRepository;
            _productService = productService;
        }

        [Route("product/all")]
        public ActionResult Index()
        {
            return View(_productService.List());
        }

        [Route("product/{seoFriendlyId}")]
        public ActionResult Details(string seoFriendlyId)
        {
            var product = _productService.Find(seoFriendlyId);
            if (product == null)
            {
                return HttpNotFound();
            }
            
            LoadUploadedFiles(product);
            return View(product);
        }

        [Authorize]
        public ActionResult Link(Guid id)
        {
            if(!Roles.CurrentUserIsInRoles(Constants.Constants.UnicornUser) && !Roles.CurrentUserIsInRoles(RoleNames.Administrators))
            {
                return HttpNotFound();
            }

            var product = _productService.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View("Link", product);
        }

        [Authorize]
        public ActionResult PackLink(Guid id)
        {
            if(!Roles.CurrentUserIsInRoles(Constants.Constants.UnicornUser) && !Roles.CurrentUserIsInRoles(RoleNames.Administrators))
            {
                return HttpNotFound();
            }

            var pack = _productService.FindPack(id);
            if (pack == null)
            {
                return HttpNotFound();
            }
            return View("PackLink", pack);
        }
        
        public override string GetObjectName()
        {
            return typeof(NewsItem).Name;
        }
    }
}

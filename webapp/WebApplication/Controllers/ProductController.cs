﻿using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Packages;
using K9.WebApplication.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using K9.WebApplication.Helpers;
using K9.WebApplication.Models;

namespace K9.WebApplication.Controllers
{
    public class ProductController : BasePureController
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IProductService _productService;

        public ProductController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IRepository<Product> productsRepository, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService, IProductService productService, IPureControllerPackage pureControllerPackage)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, pureControllerPackage)
        {
            _productsRepository = productsRepository;
            _productService = productService;
        }

        [Route("product/all")]
        public ActionResult Index()
        {
            var products = _productService.List(true);
            foreach (var product in products)
            {
                LoadUploadedFiles(product);
            }
            return View(products);
        }

        [Route("products/export/json")]
        private ActionResult GetProductsJson(List<ProductItem> items)
        {
            return Json(new { success = true, data = items }, JsonRequestBehavior.AllowGet);
        }

        [Route("products/export/json/all")]
        public ActionResult GetProductsJsonAll()
        {
            return GetProductsJson(_productService.ListProductItemsAll());
        }

        [Route("products/export/json/capsules100")]
        public ActionResult GetProductsJson100Capsules()
        {
            return GetProductsJson(_productService.ListProductItems100Capsules());
        }

        [Route("products/export/json/capsules200")]
        public ActionResult GetProductsJson200Capsules()
        {
            return GetProductsJson(_productService.ListProductItems200Capsules());
        }

        [Route("products/export/json/capsules400")]
        public ActionResult GetProductsJson400Capsules()
        {
            return GetProductsJson(_productService.ListProductItems400Capsules());
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
            if (!SessionHelper.CurrentUserIsUnicornUser() && !SessionHelper.CurrentUserIsAdmin())
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

        public override string GetObjectName()
        {
            return typeof(NewsItem).Name;
        }
    }
}

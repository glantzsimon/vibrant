﻿using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using NLog;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    public class SharedController : BasePureController
    {
        private readonly IAuthentication _authentication;
        private readonly IProductService _productService;
        private readonly IIngredientService _ingredientService;

        public SharedController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService, IProductService productService, IIngredientService ingredientService)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, membershipService)
        {
            _authentication = authentication;
            _productService = productService;
            _ingredientService = ingredientService;
        }

        [ChildActionOnly]
        public ActionResult ProductMenuList()
        {
            return PartialView("ProductMenuList", _productService.List());
        }

        [ChildActionOnly]
        public ActionResult IngredientMenuList()
        {
            return PartialView("IngredientMenuList", _ingredientService.List());
        }

        public override string GetObjectName()
        {
            return string.Empty;
        }
    }
}
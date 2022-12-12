using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using NLog;
using System.Linq;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    public class ProductController : BasePureController
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<ProductIngredient> _productIngredientsRepository;
        private readonly IRepository<Ingredient> _ingredientsRepository;

        public ProductController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IRepository<Product> productsRepository, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService, IRepository<ProductIngredient> productIngredientsRepository, IRepository<Ingredient> ingredientsRepository)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, membershipService)
        {
            _productsRepository = productsRepository;
            _productIngredientsRepository = productIngredientsRepository;
            _ingredientsRepository = ingredientsRepository;
        }

        [Route("product/all")]
        public ActionResult Index()
        {
            return View(_productsRepository.List());;
        }

        [Route("product/{seoFriendlyId}")]
        public ActionResult Details(string seoFriendlyId)
        {
            var product = _productsRepository.Find(e => e.SeoFriendlyId == seoFriendlyId).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }

            product.ProductIngredients = _productIngredientsRepository.Find(e => e.ProductId == product.Id).ToList();

            foreach (var productIngredient in product.ProductIngredients)
            {
                productIngredient.Ingredient =
                    _ingredientsRepository.Find(e => e.Id == productIngredient.IngredientId).FirstOrDefault();
            }

            LoadUploadedFiles(product);
            return View(product);
        }
        
        public override string GetObjectName()
        {
            return typeof(NewsItem).Name;
        }
    }
}

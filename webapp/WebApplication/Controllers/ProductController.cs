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

            var productIngredients = _productIngredientsRepository.Find(e => e.ProductId == product.Id).OrderByDescending(e => e.Amount).ToList();

            foreach (var productIngredient in productIngredients)
            {
                productIngredient.Ingredient =
                    _ingredientsRepository.Find(e => e.Id == productIngredient.IngredientId).FirstOrDefault();
            }

            product.ProductIngredients = productIngredients.OrderByDescending(e => e.Amount).ThenBy(e => e.Ingredient.Name);

            LoadUploadedFiles(product);
            return View(product);
        }
        
        public override string GetObjectName()
        {
            return typeof(NewsItem).Name;
        }
    }
}

using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Helpers;
using K9.SharedLibrary.Models;
using K9.WebApplication.Packages;
using K9.WebApplication.Services;
using NLog;
using System.Linq;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    public class IngredientController : BasePureController
    {
        private readonly IRepository<Ingredient> _ingredientsRepository;
        private readonly IIngredientService _ingredientService;

        public IngredientController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IRepository<Ingredient> ingredientsRepository, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService, IPureControllerPackage pureControllerPackage, IIngredientService ingredientService)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, pureControllerPackage)
        {
            _ingredientsRepository = ingredientsRepository;
            _ingredientService = ingredientService;
        }

        [Route("ingredient/all")]
        public ActionResult Index()
        {
            return View(_ingredientsRepository.Find(e => !e.IsHidden).OrderBy(e => e.Name).ToList());
        }

        [Route("ingredients/export/json")]
        public ActionResult GetIngredientsJson()
        {
            return Json(new { success = true, data = _ingredientService.ListIngredientItems() }, JsonRequestBehavior.AllowGet);
        }

        [Route("ingredient/{seoFriendlyId}")]
        public ActionResult Details(string seoFriendlyId)
        {
            var ingredient = _ingredientsRepository.Find(e => e.SeoFriendlyId == seoFriendlyId && !e.IsHidden).FirstOrDefault();
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            LoadUploadedFiles(ingredient);
            return View(ingredient);
        }
        
        public override string GetObjectName()
        {
            return typeof(NewsItem).Name;
        }
    }
}

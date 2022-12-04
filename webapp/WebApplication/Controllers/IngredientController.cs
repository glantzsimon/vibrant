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
    public class IngredientController : BasePureController
    {
        private readonly IRepository<Ingredient> _ingredientsRepository;

        public IngredientController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IRepository<Ingredient> ingredientsRepository, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, membershipService)
        {
            _ingredientsRepository = ingredientsRepository;
        }

        [Route("ingredient/all")]
        public ActionResult Index()
        {
            return View(_ingredientsRepository.List());;
        }

        [Route("ingredient/{seoFriendlyId}")]
        public ActionResult Details(string seoFriendlyId)
        {
            var ingredient = _ingredientsRepository.Find(e => e.SeoFriendlyId == seoFriendlyId).FirstOrDefault();
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

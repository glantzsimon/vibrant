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
    public class ArticleController : BaseVibrantController
    {
        private readonly IRepository<Article> _articlesRepository;

        public ArticleController(ILogger logger, IDataSetsHelper dataSetsHelper, IRoles roles, IRepository<Article> articlesRepository, IAuthentication authentication, IFileSourceHelper fileSourceHelper, IMembershipService membershipService)
            : base(logger, dataSetsHelper, roles, authentication, fileSourceHelper, membershipService)
        {
            _articlesRepository = articlesRepository;
        }

        [Route("articles")]
        public ActionResult Index()
        {
            return View(_articlesRepository.GetQuery($"SELECT TOP 10 * FROM [{nameof(Article)}] ORDER BY [{nameof(Article.CreatedOn)}] DESC").ToList());
        }

        [Route("articles/{seoFriendlyId}")]
        public ActionResult Details(string seoFriendlyId)
        {
            var article = _articlesRepository.Find(e => e.SeoFriendlyId == seoFriendlyId).FirstOrDefault();
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        public PartialViewResult ArticlesSummary()
        {
            return PartialView("_ArticlesSummary", _articlesRepository.GetQuery($"SELECT TOP 10 * FROM [{nameof(Article)}] ORDER BY [{nameof(Article.PublishedOn)}]").ToList());
        }

        public override string GetObjectName()
        {
            return typeof(NewsItem).Name;
        }
    }
}

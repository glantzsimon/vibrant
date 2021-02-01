using K9.Base.WebApplication.Controllers;
using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.WebApplication.Extensions;
using System;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
	public class ArticlesController : BaseController<Article>
	{
		public ArticlesController(IControllerPackage<Article> controllerPackage) : base(controllerPackage)
		{
		    RecordBeforeCreate += ArticlesController_RecordBeforeCreate;
            RecordBeforeCreated += ArticlesController_RecordBeforeUpdated;
            RecordBeforeUpdated += ArticlesController_RecordBeforeUpdated;
		}

        private void ArticlesController_RecordBeforeUpdated(object sender, CrudEventArgs e)
        {
            var article = e.Item as Article;
            var original = Repository.Find(article.Id);
            var subjectHasChanged = original.Subject != article.Subject;
            if (string.IsNullOrEmpty(article.SeoFriendlyId) || subjectHasChanged && original.SeoFriendlyId == original.Subject.ToSeoFriendlyString())
            {
                article.SeoFriendlyId = article.Subject.ToSeoFriendlyString();
            }
        }

        private void ArticlesController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var article = e.Item as Article;
            if (string.IsNullOrEmpty(article.SeoFriendlyId))
            {
                article.SeoFriendlyId = article.Subject.ToSeoFriendlyString();
            }
        }

        void ArticlesController_RecordBeforeCreate(object sender, CrudEventArgs e)
	    {
	        var article = e.Item as Article;
	        article.PublishedBy = WebSecurity.IsAuthenticated ? WebSecurity.CurrentUserName : string.Empty;
	        article.PublishedOn = DateTime.Now;
	    }
	}
}

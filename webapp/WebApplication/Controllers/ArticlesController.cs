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
	public class ArticlesController : HtmlControllerBase<Article>
	{
		public ArticlesController(IControllerPackage<Article> controllerPackage) : base(controllerPackage)
		{
		    RecordBeforeCreate += ArticlesController_RecordBeforeCreate;
            RecordBeforeCreated += ArticlesController_RecordBeforeCreated;
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
                article.Name = article.Subject;
            }
        }

        private void ArticlesController_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var article = e.Item as Article;
            if (string.IsNullOrEmpty(article.SeoFriendlyId))
            {
                article.SeoFriendlyId = article.Subject.ToSeoFriendlyString();
            }
            article.Name = article.Subject;
        }

        void ArticlesController_RecordBeforeCreate(object sender, CrudEventArgs e)
	    {
	        var article = e.Item as Article;
	        article.UserId = WebSecurity.CurrentUserId;
	        article.Name = Guid.NewGuid().ToString();
	        article.PublishedBy = WebSecurity.CurrentUserName;
	        article.PublishedOn = DateTime.Now;
	    }
	}
}

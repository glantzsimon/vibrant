using K9.Base.WebApplication.Controllers;
using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.UnitsOfWork;
using K9.SharedLibrary.Models;
using K9.WebApplication.Helpers;

namespace K9.WebApplication.Controllers
{
    public class HtmlControllerBase<T> : BaseController<T> where T : class, IObjectBase
    {
        public HtmlControllerBase(IControllerPackage<T> controllerPackage) : base(controllerPackage)
        {
            RecordBeforeCreated += HtmlControllerBase_RecordBeforeCreated;
            RecordBeforeUpdated += HtmlControllerBase_RecordBeforeUpdated;
            RecordBeforeUpdate += HtmlControllerBase_RecordBeforeUpdate;
            RecordBeforeDeleted += HtmlControllerBase_RecordBeforeDeleted;
        }

        private void HtmlControllerBase_RecordBeforeDeleted(object sender, CrudEventArgs e)
        {
            var model = e.Item as T;
            model.IsDeleted = true;
            Repository.Update(model);
        }

        private void HtmlControllerBase_RecordBeforeUpdated(object sender, CrudEventArgs e)
        {
            var model = e.Item as T;
            HtmlParser.ParseHtml(ref model);
        }

        private void HtmlControllerBase_RecordBeforeCreated(object sender, CrudEventArgs e)
        {
            var model = e.Item as T;
            HtmlParser.ParseHtml(ref model);
        }

        private void HtmlControllerBase_RecordBeforeUpdate(object sender, CrudEventArgs e)
        {
            var model = e.Item as T;
            HtmlParser.ParseHtml(ref model);
        }
    }
}

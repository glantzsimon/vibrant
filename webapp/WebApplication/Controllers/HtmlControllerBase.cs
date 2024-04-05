using K9.Base.WebApplication.Controllers;
using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.UnitsOfWork;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using K9.WebApplication.Helpers;
using K9.WebApplication.Packages;
using System.Linq;
using WebMatrix.WebData;

namespace K9.WebApplication.Controllers
{
    public class HtmlControllerBase<T> : BaseController<T>, IShoppingCartController where T : class, IObjectBase
    {
        private readonly IPureControllerPackage _pureControllerPackage;

        public Order ShoppingCart => WebSecurity.IsAuthenticated
            ? _pureControllerPackage.ShoppingCartService.GetShoppingCart(Current.UserId)
            : null;

        public HtmlControllerBase(IControllerPackage<T> controllerPackage, IPureControllerPackage pureControllerPackage) : base(controllerPackage)
        {
            _pureControllerPackage = pureControllerPackage;
            SetSessionRoles(Current.UserId);
            LoadDatasets();

            RecordBeforeCreated += HtmlControllerBase_RecordBeforeCreated;
            RecordBeforeUpdated += HtmlControllerBase_RecordBeforeUpdated;
            RecordBeforeUpdate += HtmlControllerBase_RecordBeforeUpdate;
            RecordBeforeDeleted += HtmlControllerBase_RecordBeforeDeleted;
        }

        public void SetSessionRoles(int userId)
        {
            SessionHelper.SetCurrentUserRoles(_pureControllerPackage.RolesRepository, _pureControllerPackage.UserRolesRepository, userId);
        }

        public void LoadDatasets()
        {
            Helpers.DatasetHelper.LoadDatasets(_pureControllerPackage.OrdersRepository);
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

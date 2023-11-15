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
            ? _pureControllerPackage.ShoppingCartService.GetShoppingCart(WebSecurity.CurrentUserId)
            : null;

        public HtmlControllerBase(IControllerPackage<T> controllerPackage, IPureControllerPackage pureControllerPackage) : base(controllerPackage)
        {
            _pureControllerPackage = pureControllerPackage;
            SetSessionRoles(WebSecurity.CurrentUserId);

            RecordBeforeCreated += HtmlControllerBase_RecordBeforeCreated;
            RecordBeforeUpdated += HtmlControllerBase_RecordBeforeUpdated;
            RecordBeforeUpdate += HtmlControllerBase_RecordBeforeUpdate;
            RecordBeforeDeleted += HtmlControllerBase_RecordBeforeDeleted;
        }

        public void SetSessionRoles(int userId)
        {
            var adminRole =  _pureControllerPackage.RolesRepository.Find(e => e.Name == Constants.Constants.Administrator).First();
            var powerUserRole = _pureControllerPackage.RolesRepository.Find(e => e.Name == Constants.Constants.ClientUser).First();
            var clientRole = _pureControllerPackage.RolesRepository.Find(e => e.Name == Constants.Constants.ClientUser).First();
            var practitionerUser = _pureControllerPackage.RolesRepository.Find(e => e.Name == Constants.Constants.ClientUser).First();
            var unicornRole = _pureControllerPackage.RolesRepository.Find(e => e.Name == Constants.Constants.UnicornUser).First();
            
            var isAmin = _pureControllerPackage.UserRolesRepository.Exists(e => e.UserId == userId && e.RoleId == adminRole.Id);
            var isPower = _pureControllerPackage.UserRolesRepository.Exists(e => e.UserId == userId && e.RoleId == powerUserRole.Id);
            var isClient = _pureControllerPackage.UserRolesRepository.Exists(e => e.UserId == userId && e.RoleId == clientRole.Id);
            var isPractitioner = _pureControllerPackage.UserRolesRepository.Exists(e => e.UserId == userId && e.RoleId == practitionerUser.Id);
            var isUnicorn = _pureControllerPackage.UserRolesRepository.Exists(e => e.UserId == userId && e.RoleId == unicornRole.Id);

            SessionHelper.SetCurrentUserRoles(isAmin, isPower, isClient, isPractitioner, isUnicorn);
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

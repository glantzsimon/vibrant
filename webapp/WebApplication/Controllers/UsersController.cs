using K9.Base.DataAccessLayer.Config;
using K9.Base.DataAccessLayer.Models;
using K9.Base.WebApplication.Controllers;
using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Models;
using System;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace K9.WebApplication.Controllers
{
    [Authorize]
    [RequirePermissions(Role = RoleNames.Administrators)]
    public class UsersController : BaseController<User>
    {
        private readonly IOptions<DatabaseConfiguration> _dataConfig;
        private readonly IRoles _roles;
        private readonly IRepository<UserRole> _userRolesRepository;
        private readonly IRepository<Role> _rolesRepository;

        public UsersController(IControllerPackage<User> controllerPackage, IOptions<DatabaseConfiguration> dataConfig, IRoles roles, IRepository<UserRole> userRolesRepository, IRepository<Role> rolesRepository)
            : base(controllerPackage)
        {
            _dataConfig = dataConfig;
            _roles = roles;
            _userRolesRepository = userRolesRepository;
            _rolesRepository = rolesRepository;
            RecordBeforeCreate += UsersController_RecordBeforeCreate;
            RecordCreated += UsersController_RecordCreated;
            RecordBeforeDeleted += UsersController_RecordBeforeDeleted;
        }

        private void UsersController_RecordBeforeCreate(object sender, CrudEventArgs e)
        {
            var user = e.Item as User;
            user.BirthDate = new DateTime(1970, 1, 1);
        }

        public ActionResult EditProtocols(int id = 0)
        {
            return RedirectToAction("EditProtocolsForUser", "UserProtocols", new { id });
        }
        
        private void UsersController_RecordBeforeDeleted(object sender, CrudEventArgs e)
        {
            var user = e.Item as User;
            try
            {
                user.SetToDeleted();
                Repository.Update(user);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        void UsersController_RecordCreated(object sender, CrudEventArgs e)
        {
            var user = e.Item as User;
            WebSecurity.CreateAccount(user.Username, $"{user.Username}1234");
            
            _roles.AddUserToRole(user.Username, RoleNames.DefaultUsers);
            _roles.AddUserToRole(user.Username, Constants.Constants.ClientUser);
        }

    }
}

using K9.Base.DataAccessLayer.Config;
using K9.Base.DataAccessLayer.Models;
using K9.Base.WebApplication.Controllers;
using K9.Base.WebApplication.EventArgs;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Models;
using System;
using System.Linq;
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
            RecordCreated += UsersController_RecordCreated;
            RecordBeforeDeleted += UsersController_RecordBeforeDeleted;
            RecordCreated += UsersController_RecordCreated1;
        }

        public ActionResult EditProtocols(int id = 0)
        {
            return RedirectToAction("EditProtocolsForUser", "UserProtocols", new { id });
        }

        private void UsersController_RecordCreated1(object sender, CrudEventArgs e)
        {
            var user = e.Item as User;

            // Add client user role
            var clientUserRole = _rolesRepository.Find(_ => _.Name == Constants.Constants.ClientUser).FirstOrDefault();
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = clientUserRole.Id
            };
            _userRolesRepository.Create(userRole);
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
            WebSecurity.CreateAccount(user.Username, _dataConfig.Value.DefaultUserPassword);
            _roles.AddUserToRole(user.Username, RoleNames.DefaultUsers);
        }

    }
}

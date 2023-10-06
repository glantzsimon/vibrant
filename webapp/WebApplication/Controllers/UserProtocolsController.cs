using K9.Base.DataAccessLayer.Models;
using K9.Base.WebApplication.Controllers;
using K9.Base.WebApplication.Filters;
using K9.Base.WebApplication.UnitsOfWork;
using K9.Base.WebApplication.ViewModels;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Models;
using System.Web.Mvc;

namespace K9.WebApplication.Controllers
{
    [Authorize]
	[RequirePermissions(Role = RoleNames.Administrators)]
	public class UserProtocolsController : BaseController<UserProtocol>
	{
	    private readonly IRepository<User> _usersRepository;
		
		public UserProtocolsController(IControllerPackage<UserProtocol> controllerPackage, IRepository<User> usersRepository)
			: base(controllerPackage)
		{
		    _usersRepository = usersRepository;
		}

	    public override ActionResult Index()
	    {
	        return RedirectToAction("Index", "Users");
	    }

		[RequirePermissions(Permission = Permissions.Edit)]
		public ActionResult EditProtocolsForUser(int id = 0)
		{
			return EditMultiple<User, Protocol>(_usersRepository.Find(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[RequirePermissions(Permission = Permissions.Edit)]
		public ActionResult EditProtocolsForUser(MultiSelectViewModel model)
		{
			return EditMultiple<User, Protocol>(model);
		}

	}
}

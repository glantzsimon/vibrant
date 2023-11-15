using K9.Base.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;

namespace K9.WebApplication.Packages
{
    public class PureControllerPackage : IPureControllerPackage
    {
        public PureControllerPackage(IShoppingCartService shoppingCartService, IMembershipService membershipService, IRepository<User> usersRepository, IRepository<Role> rolesRepository, IRepository<UserRole> userRolesRepository)
        {
            ShoppingCartService = shoppingCartService;
            MembershipService = membershipService;
            UsersRepository = usersRepository;
            RolesRepository = rolesRepository;
            UserRolesRepository = userRolesRepository;
        }

        public IShoppingCartService ShoppingCartService { get; set; }
        public IMembershipService MembershipService { get; set; }
        public IRepository<User> UsersRepository { get; set; }
        public IRepository<Role> RolesRepository { get; set; }
        public IRepository<UserRole> UserRolesRepository { get; set; }
    }
}

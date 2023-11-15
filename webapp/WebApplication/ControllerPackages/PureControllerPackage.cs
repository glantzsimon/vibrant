using K9.Base.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;

namespace K9.WebApplication.Packages
{
    public interface IPureControllerPackage
    {
        IShoppingCartService ShoppingCartService { get; set; }
        IMembershipService MembershipService { get; set; }
        IRepository<User> UsersRepository { get; set; }
        IRepository<Role> RolesRepository { get; set; }
        IRepository<UserRole> UserRolesRepository { get; set; }
    }
}
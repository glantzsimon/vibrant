using K9.WebApplication.Services;

namespace K9.WebApplication.Packages
{
    public interface IPureControllerPackage
    {
        IShoppingCartService ShoppingCartService { get; set; }
        IMembershipService MembershipService { get; set; }
    }
}
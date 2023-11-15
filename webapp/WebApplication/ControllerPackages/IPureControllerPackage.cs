using K9.WebApplication.Services;

namespace K9.WebApplication.Packages
{
    public class PureControllerPackage : IPureControllerPackage
    {
        public IShoppingCartService ShoppingCartService { get; set; }
        public IMembershipService MembershipService { get; set; }
    }
}

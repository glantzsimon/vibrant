using K9.DataAccessLayer.Models;

namespace K9.WebApplication.Controllers
{
    public interface IShoppingCartController
    {
        Order ShoppingCart { get; }
    }
}
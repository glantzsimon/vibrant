using K9.DataAccessLayer.Models;

namespace K9.WebApplication.Services
{
    public interface IShoppingCartService
    {
        Order GetShoppingCart(int userId);
        void SetShoppingCartIsPaid();
        void AddProductToCart(int productId, int amount);
        void UpdateProductAmount(int productId, int amount);
        void AddProductPackToCart(int productPackId, int amount);
        void UpdateProductPackAmount(int productPackId, int amount);
    }
}
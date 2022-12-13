using K9.DataAccessLayer.Models;

namespace K9.WebApplication.Services
{
    public interface IProductService
    {
        Product Find(int id);
        Product Find(string seoFriendlyId);
        Product GetFullProduct(Product product);
    }
}
using System.Collections.Generic;
using K9.DataAccessLayer.Models;

namespace K9.WebApplication.Services
{
    public interface IProductService
    {
        Product Find(int id);
        Product FindPrevious(int id);
        Product FindNext(int id);
        Product Find(string seoFriendlyId);
        Product GetFullProduct(Product product);
        Product DuplicateProduct(int id);
        List<Product> List(bool retrieveFullProduct = false);
    }
}
using K9.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using K9.SharedLibrary.Models;

namespace K9.WebApplication.Services
{
    public interface IProductService
    {
        Product Find(int id);
        Product FindPrevious(int id);
        Product FindNext(int id);
        Product Find(string seoFriendlyId);
        Product Find(Guid id);
        Product GetFullProduct(Product product);
        Product DuplicateProduct(int id);
        Product UpdateBatchSize(Product product, int batchSize);
        List<Product> List(bool retrieveFullProduct = false, bool includeCustomProducts = false);

        ProductPack FindPack(Guid id);
        ProductPack GetFullProductPack(ProductPack productPack);
    }
}
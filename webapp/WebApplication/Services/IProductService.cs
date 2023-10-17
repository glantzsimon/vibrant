using K9.DataAccessLayer.Models;
using System;
using System.Collections.Generic;

namespace K9.WebApplication.Services
{
    public interface IProductService : ICategorisableService
    {
        Product Find(int id);
        Product FindPrevious(int id);
        Product FindNext(int id);
        Product Find(string seoFriendlyId);
        Product Find(Guid id);
        Product GetFullProduct(Product product);
        Product Duplicate(int id);
        Product UpdateBatchSize(Product product, int batchSize);
        void DeleteChildRecords(int id);
        void EditIngredientSubstitutes(Product product);
        void UpdateProductCategories();
        List<Product> List(bool retrieveFullProduct = false, bool includeCustomProducts = false);
        List<ProductPack> ListProductPacks(bool retrieveFullProduct = false);

        ProductPack FindPack(Guid id);
        ProductPack FindPack(int id);
        ProductPack FindPack(string seoFriendlyId);
        ProductPack GetFullProductPack(ProductPack productPack);
    }
}
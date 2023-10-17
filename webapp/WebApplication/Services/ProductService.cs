using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Models;
using Microsoft.Extensions.Caching.Memory;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using K9.DataAccessLayer.Enums;

namespace K9.WebApplication.Services
{
    public class ProductService : CacheableServiceBase<Product>, IProductService
    {
        private readonly ILogger _logger;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<ProductIngredient> _productIngredientsRepository;
        private readonly IRepository<Ingredient> _ingredientsRepository;
        private readonly IRepository<ProductPackProduct> _productPackProductRepository;
        private readonly IRepository<ProductPack> _productPackRepository;
        private readonly IRepository<ProductIngredientSubstitute> _productIngredientSubstituteRepository;
        private readonly IIngredientService _ingredientService;

        public ProductService(ILogger logger, IRepository<Product> productsRepository, IRepository<ProductIngredient> productIngredientsRepository, IRepository<Ingredient> ingredientsRepository, IRepository<ProductPackProduct> productPackProductRepository, IRepository<ProductPack> productPackRepository, IRepository<ProductIngredientSubstitute> productIngredientSubstituteRepository, IIngredientService ingredientService)
        {
            _logger = logger;
            _productsRepository = productsRepository;
            _productIngredientsRepository = productIngredientsRepository;
            _ingredientsRepository = ingredientsRepository;
            _productPackProductRepository = productPackProductRepository;
            _productPackRepository = productPackRepository;
            _productIngredientSubstituteRepository = productIngredientSubstituteRepository;
            _ingredientService = ingredientService;
        }

        public Product Find(int id)
        {
            return MemoryCache.GetOrCreate(GetCacheKey(id), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.QuarterHour));

                var product = _productsRepository.Find(id);
                if (product != null)
                {
                    product = GetFullProduct(product);
                }

                return product;
            });
        }

        public Product FindNext(int id)
        {
            var product = _productsRepository.Find(e => e.Id > id).OrderBy(e => e.Id).FirstOrDefault() ?? _productsRepository.GetQuery("SELECT TOP 1 * FROM [Product] ORDER BY [Id]").FirstOrDefault();
            if (product != null)
            {
                product = GetFullProduct(product);
            }

            return product;
        }

        public Product FindPrevious(int id)
        {
            var product = _productsRepository.Find(e => e.Id < id).OrderByDescending(e => e.Id).FirstOrDefault() ?? _productsRepository.GetQuery("SELECT TOP 1 * FROM [Product] ORDER BY [Id] DESC").FirstOrDefault();
            if (product != null)
            {
                product = GetFullProduct(product);
            }

            return product;
        }

        public Product Find(string seoFriendlyId)
        {
            var product = _productsRepository.Find(e => e.SeoFriendlyId == seoFriendlyId).FirstOrDefault();
            if (product != null)
            {
                product = GetFullProduct(product);
            }

            return product;
        }

        public Product Find(Guid id)
        {
            var product = _productsRepository.Find(e => e.ExternalId == id).FirstOrDefault();
            if (product != null)
            {
                product = GetFullProduct(product);
            }

            return product;
        }

        public ProductPack FindPack(Guid id)
        {
            var pack = _productPackRepository.Find(e => e.ExternalId == id).FirstOrDefault();
            if (pack != null)
            {
                pack = GetFullProductPack(pack);
            }
            return pack;
        }

        public ProductPack FindPack(int id)
        {
            var pack = _productPackRepository.Find(id);
            if (pack != null)
            {
                pack = GetFullProductPack(pack);
            }
            return pack;
        }

        public ProductPack FindPack(string seoFriendlyId)
        {
            var productPack = _productPackRepository.Find(e => e.SeoFriendlyId == seoFriendlyId).FirstOrDefault();
            if (productPack != null)
            {
                productPack = GetFullProductPack(productPack);
            }

            return productPack;
        }

        public Product GetFullProduct(Product product)
        {
            return MemoryCache.GetOrCreate(GetCacheKey(product.Id), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.QuarterHour));

                var productIngredients = _productIngredientsRepository.Find(e => e.ProductId == product.Id).ToList();
                var productIngredientsWithSubstitutes = new List<ProductIngredient>();

                foreach (var productIngredient in productIngredients)
                {
                    productIngredient.Ingredient = _ingredientService.Find(productIngredient.IngredientId);
                    productIngredient.IngredientName = productIngredient.Ingredient.Name;
                    productIngredient.IngredientSubstitutes =
                        _productIngredientSubstituteRepository.Find(e => e.ProductIngredientId == productIngredient.Id).ToList();

                    foreach (var ingredientSubstitute in productIngredient.Ingredient.Substitutes)
                    {
                        var productIngredientSubstitute = productIngredient.IngredientSubstitutes.FirstOrDefault(e =>
                            e.SubstituteIngredientId == ingredientSubstitute.SubstituteIngredientId);

                        if (productIngredientSubstitute != null)
                        {
                            ingredientSubstitute.IsSelected = true;
                            ingredientSubstitute.Priority = productIngredientSubstitute.Priority;
                        }
                    }

                    productIngredient.Ingredient.Substitutes = productIngredient.Ingredient.Substitutes
                        .OrderByDescending(e => e.IsSelected)
                        .ThenBy(e => e.Priority).ToList();

                    if (productIngredient.Ingredient.IsInStock)
                    {
                        productIngredientsWithSubstitutes.Add(productIngredient);
                    }
                    else
                    {
                        var substitutes = productIngredient.IngredientSubstitutes
                            .Where(e => e.SubstituteIngredient.IsInStock)
                            .Take(productIngredient.NumberOfSubstitutesToUse)
                            .OrderBy(e => e.Priority).ToList();

                        var splitAmount = productIngredient.Amount / substitutes.Count;

                        productIngredientsWithSubstitutes.AddRange(substitutes.Select(e => new ProductIngredient
                        {
                            BatchSize = productIngredient.BatchSize,
                            Ingredient = e.SubstituteIngredient,
                            Amount = splitAmount
                        }));
                    }
                }

                product.ProductIngredients = productIngredients.OrderByDescending(e => e.Amount)
                                .ThenBy(e => e.Ingredient.Name);

                product.Ingredients = product.ProductIngredients.ToList();

                product.IngredientsWithSubstitutes = productIngredientsWithSubstitutes.OrderByDescending(e => e.Amount)
                    .ThenBy(e => e.Ingredient.Name).ToList();

                return product;
            });
        }

        public Product Duplicate(int id)
        {
            var product = Find(id);
            if (product == null)
            {
                return null;
            }

            var newProduct = new Product();
            var newProductExternalId = Guid.NewGuid();

            product.MapTo(newProduct);
            newProduct.Id = 0;
            newProduct.Name = $"{product.Name} (Copy)";
            newProduct.ExternalId = newProductExternalId;

            _productsRepository.Create(newProduct);

            // Get new Id
            newProduct = Find(newProductExternalId);

            if (newProduct == null)
            {
                throw new Exception("Error duplicating product");
            }

            // Copy Ingredients
            foreach (var ingredient in product.Ingredients)
            {
                var newIngredient = new ProductIngredient
                {
                    ProductId = newProduct.Id,
                    IngredientId = ingredient.IngredientId,
                    Amount = ingredient.Amount
                };

                _productIngredientsRepository.Create(newIngredient);
            }

            return newProduct;
        }

        public Product UpdateBatchSize(Product product, int batchSize)
        {
            if (batchSize > 1)
            {
                product.Ingredients.ForEach(e => e.BatchSize = batchSize);
            }

            product.BatchSize = batchSize;
            return product;
        }

        public void DeleteChildRecords(int id)
        {
            var product = Find(id);

            foreach (var productIngredient in product.Ingredients)
            {
                _productIngredientsRepository.Delete(productIngredient.Id);
            }
        }

        public List<Product> List(bool retrieveFullProduct = false, bool includeCustomProducts = false)
        {
            return MemoryCache.GetOrCreate(GetCacheKey(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.TwoHours));

                var products = _productsRepository.List().Where(e => !e.IsDeleted).OrderBy(e => e.Name).ToList();

                if (!includeCustomProducts)
                {
                    products = products.Where(e => e.ContactId <= 0 || !e.ContactId.HasValue).ToList();
                }

                if (retrieveFullProduct)
                {
                    var fullProducts = new List<Product>();
                    foreach (var product in products)
                    {
                        fullProducts.Add(GetFullProduct(product));
                    }

                    return fullProducts;
                }

                return products;
            });
        }

        public List<ProductPack> ListProductPacks(bool retrieveFullProduct = false)
        {
            return MemoryCache.GetOrCreate(GetCacheKey<ProductPack>(), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.TwoHours));

                var productPacks = _productPackRepository.List().Where(e => !e.IsDeleted).OrderBy(e => e.Name).ToList();

                if (retrieveFullProduct)
                {
                    foreach (var pack in productPacks)
                    {
                        foreach (var productPackProduct in pack.Products)
                        {
                            productPackProduct.Product = GetFullProduct(productPackProduct.Product);
                        }
                    }
                }

                return productPacks;
            });
        }

        public ProductPack GetFullProductPack(ProductPack productPack)
        {
            return MemoryCache.GetOrCreate(GetCacheKey<ProductPack>(productPack.Id), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.QuarterHour));

                var products = _productPackProductRepository.Find(e => e.ProductPackId == productPack.Id).ToList();

                foreach (var product in products)
                {
                    product.Product = _productsRepository.Find(e => e.Id == product.ProductId).FirstOrDefault();
                }

                productPack.Products = products.OrderBy(e => e.Product.Name).ToList();

                return productPack;
            });
        }

        public void EditIngredientSubstitutes(Product product)
        {
            foreach (var productIngredient in product.Ingredients.Where(e => e.Ingredient.Substitutes != null && e.Ingredient.Substitutes.Any()))
            {
                var existingSubstitutes = _productIngredientSubstituteRepository.Find(e => e.ProductIngredientId == productIngredient.Id).ToList();
                var newItems = productIngredient.Ingredient.Substitutes.Where(e => e.IsSelected).ToList();

                foreach (var item in existingSubstitutes)
                {
                    _productIngredientSubstituteRepository.Delete(item.Id);
                }

                foreach (var item in newItems)
                {
                    var newItem = new ProductIngredientSubstitute
                    {
                        ProductIngredientId = productIngredient.Id,
                        SubstituteIngredientId = item.SubstituteIngredientId,
                        Priority = item.Priority
                    };
                    _productIngredientSubstituteRepository.Create(newItem);
                }

                var productIngredientRecord = _productIngredientsRepository.Find(productIngredient.Id);
                productIngredientRecord.NumberOfSubstitutesToUse = productIngredient.NumberOfSubstitutesToUse;
                _productIngredientsRepository.Update(productIngredientRecord);
            }
        }

        public void UpdateProductCategories()
        {
            foreach (var product in _productsRepository.List())
            {
                product.Category = ECategory.DietarySupplement;
                _productsRepository.Update(product);
            }

            var itemCode = (int)ECategory.DietarySupplement + Constants.Constants.ItemCodeGap;

            foreach (var product in _productsRepository.List().OrderBy(e => e.Name).ToList())
            {
                product.ItemCode = itemCode;
                _productsRepository.Update(product);
                itemCode += Constants.Constants.ItemCodeGap;
            }
        }
    }
}
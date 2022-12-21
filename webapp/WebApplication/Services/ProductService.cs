using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace K9.WebApplication.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger _logger;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<ProductIngredient> _productIngredientsRepository;
        private readonly IRepository<Ingredient> _ingredientsRepository;

        public ProductService(ILogger logger, IRepository<Product> productsRepository, IRepository<ProductIngredient> productIngredientsRepository, IRepository<Ingredient> ingredientsRepository)
        {
            _logger = logger;
            _productsRepository = productsRepository;
            _productIngredientsRepository = productIngredientsRepository;
            _ingredientsRepository = ingredientsRepository;
        }

        public Product Find(int id)
        {
            var product = _productsRepository.Find(id);
            if (product != null)
            {
                product = GetFullProduct(product);
            }

            return product;
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

        public Product GetFullProduct(Product product)
        {
            var productIngredients = _productIngredientsRepository.Find(e => e.ProductId == product.Id)
                .OrderByDescending(e => e.Amount).ToList();

            foreach (var productIngredient in productIngredients)
            {
                productIngredient.Ingredient =
                    _ingredientsRepository.Find(e => e.Id == productIngredient.IngredientId).FirstOrDefault();
            }

            product.ProductIngredients = productIngredients.OrderByDescending(e => e.Amount)
                .ThenBy(e => e.Ingredient.Name);

            product.Ingredients = product.ProductIngredients.ToList();

            return product;
        }

        public Product DuplicateProduct(int id)
        {
            var product = Find(id);
            if (product == null)
            {
                return null;
            }

            var newProduct = new Product();
            product.MapTo(newProduct);
            newProduct.Id = 0;
            var newProductName = $"{product.Name} Copy";
            newProduct.Name = newProductName;
            
            _productsRepository.Create(newProduct);
            newProduct = Find(newProductName);
            if (newProduct == null)
            {
                throw new Exception("Error duplicating product");
            }

            newProduct.ProductIngredients = product.ProductIngredients.ToList();
            foreach (var pi in newProduct.ProductIngredients)
            {
                pi.Id = 0;
                pi.ProductId = newProduct.Id;
            }
            _productIngredientsRepository.CreateBatch(newProduct.ProductIngredients.ToList());

            return product;
        }

        public List<Product> List(bool retrieveFullProduct = false, bool includeCustomProducts = false)
        {
            var products = _productsRepository.List().OrderBy(e => e.Name).ToList();

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
        }
    }
}
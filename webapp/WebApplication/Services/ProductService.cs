using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using NLog;
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
    }
}
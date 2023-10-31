using K9.Base.WebApplication.Config;
using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using K9.WebApplication.Services;
using Moq;
using NLog;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using K9.DataAccessLayer.Interfaces;
using Xunit;

namespace K9.WebApplication.Tests.Unit.Services
{

    public class ProductServiceTests
    {
        private readonly Mock<IRepository<Activity>> _activitiesRepository = new Mock<IRepository<Activity>>();
        private readonly Mock<IRepository<DietaryRecommendation>> _dietaryRecommendationsRepository = new Mock<IRepository<DietaryRecommendation>>();
        private readonly Mock<IRepository<Product>> _productsRepository = new Mock<IRepository<Product>>();
        private readonly Mock<IRepository<ProductIngredient>> _productIngredientsRepository = new Mock<IRepository<ProductIngredient>>();
        private readonly Mock<IRepository<Ingredient>> _ingredientsRepository = new Mock<IRepository<Ingredient>>();
        private readonly Mock<IRepository<ProductPackProduct>> _productPackProductRepository = new Mock<IRepository<ProductPackProduct>>();
        private readonly Mock<IRepository<ProductPack>> _productPackRepository = new Mock<IRepository<ProductPack>>();
        private readonly Mock<IRepository<ProductIngredientSubstitute>> _productIngredientSubstitutesRepository = new Mock<IRepository<ProductIngredientSubstitute>>();
        private readonly Mock<IRepository<Protocol>> _protocolsRepository = new Mock<IRepository<Protocol>>();
        private readonly Mock<IRepository<IngredientSubstitute>> _ingredientSubstitutesRepository = new Mock<IRepository<IngredientSubstitute>>();
        private readonly Mock<IRepository<ProductPackProduct>> _productPackProductsRepository = new Mock<IRepository<ProductPackProduct>>();

        private readonly Mock<IIngredientService> _ingredientService = new Mock<IIngredientService>();

        private readonly Mock<ILogger> _logger = new Mock<ILogger>();
        private readonly Mock<IOptions<WebsiteConfiguration>> _config = new Mock<IOptions<WebsiteConfiguration>>();


        private ProductService _productService;

        private readonly Product _adaptogens = new Product
        {
            Id = 1,
            Name = "Adaptogens",
            Category = ECategory.DietarySupplement,
            ItemCode = 600133,
            Price = 1800
        };

        private readonly Product _antiStress = new Product
        {
            Id = 2,
            Name = "Anti Stress",
            Category = ECategory.DietarySupplement,
            ItemCode = 600155,
            Price = 1800
        };

        private readonly Product _bComplex = new Product
        {
            Id = 3,
            Name = "B Complex Vitamins",
            Category = ECategory.DietarySupplement,
            ItemCode = 600177,
            Price = 1800
        };

        private readonly Product _calm = new Product
        {
            Id = 1,
            Name = "Calm",
            Category = ECategory.DietarySupplement,
            ItemCode = 600199,
            Price = 1800
        };

        private readonly Product _newProduct = new Product
        {
            Name = "Fat Soluble Vitamins",
            Category = ECategory.DietarySupplement,
            Price = 1800,
            ExpectedItemCode = 600211
        };

        private readonly Product _minerals = new Product
        {
            Id = 2,
            Name = "Minerals",
            Category = ECategory.DietarySupplement,
            ItemCode = 600222,
            Price = 1800
        };
        
        private readonly Product _sleepSupport = new Product
        {
            Id = 1,
            Name = "Sleep Support",
            Category = ECategory.DietarySupplement,
            ItemCode = 600244,
            Price = 1800
        };

        private readonly Product _newProductAtEnd = new Product
        {
            Name = "Tired & Wired",
            Category = ECategory.DietarySupplement,
            Price = 1800,
            ExpectedItemCode = 600466
        };

        private readonly Product _newProductAtBeginning = new Product
        {
            Name = "AA Support",
            Category = ECategory.DietarySupplement,
            ExpectedItemCode = 600066
        };

        private List<Product> _products;

        public ProductServiceTests()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
            );

            _config.SetupGet(_ => _.Value).Returns(new WebsiteConfiguration
            {
                CompanyLogoUrl = "http://local",
                CompanyName = "Glantz Consulting",
                SupportEmailAddress = "support@gc.com"
            });
            
            _productService = new ProductService(
                _logger.Object,
                _productsRepository.Object,
                _productIngredientsRepository.Object,
                _ingredientsRepository.Object,
                _productPackProductRepository.Object,
                _productPackRepository.Object,
                _productIngredientSubstitutesRepository.Object,
                _ingredientService.Object,
                _protocolsRepository.Object,
                _ingredientSubstitutesRepository.Object,
                _activitiesRepository.Object,
                _dietaryRecommendationsRepository.Object,
                _productPackProductsRepository.Object);

            _products = new List<Product>
            {
                _adaptogens,
                _antiStress,
                _bComplex,
                _calm,
                _minerals,
                _sleepSupport
            };
        }

        [Fact]
        public void CreateItemCode()
        {
            var result = _productService.GetItemCode(_newProduct, new List<ICategorisable>(_products));
            var result2 = _productService.GetItemCode(_newProductAtEnd, new List<ICategorisable>(_products));
            var result3 = _productService.GetItemCode(_newProductAtBeginning, new List<ICategorisable>(_products));

            Assert.Equal(_newProduct.ExpectedItemCode, result);
            Assert.Equal(_newProductAtEnd.ExpectedItemCode, result2);
            Assert.Equal(_newProductAtBeginning.ExpectedItemCode, result3);
        }
    }
}

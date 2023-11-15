using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using Microsoft.Extensions.Caching.Memory;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace K9.WebApplication.Services
{
    public class ProtocolService : CacheableServiceBase<Protocol>, IProtocolService
    {
        private readonly ILogger _logger;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<ProductPack> _productPackRepository;
        private readonly IRepository<Client> _clientsRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<Protocol> _protocolsRepository;
        private readonly IRepository<ProtocolProduct> _protocolProductsRepository;
        private readonly IRepository<ProtocolProductPack> _protocolProductPackRepository;
        private readonly IRepository<ProtocolSection> _protocolProtocolSectionRepository;
        private readonly IRepository<Section> _protocolSectionRepository;
        private readonly IRepository<ProtocolSectionProduct> _protocolProtocolSectionProductsRepository;
        private readonly IRepository<ProductPackProduct> _productPackProductRepository;
        private readonly IRepository<ProtocolActivity> _protocolActivitiesRepository;
        private readonly IRepository<Activity> _activitiesRepository;
        private readonly IRepository<ProtocolFoodItem> _protocolFoodItemsRepository;
        private readonly IRepository<HealthQuestionnaire> _healthQuestionnaiRepository;
        private readonly IRepository<ProductPackProduct> _productPackProductsRepository;
        private readonly IRepository<ProtocolDietaryRecommendation> _protocolDietaryRecommendationRepository;
        private readonly IRepository<DietaryRecommendation> _dietaryRecommendationRepository;
        private readonly DefaultValuesConfiguration _defaultValues;

        public ProtocolService(ILogger logger, IRepository<Product> productsRepository,
            IRepository<ProductPack> productPackRepository, IOptions<DefaultValuesConfiguration> defaultValues,
            IRepository<Client> clientsRepository, IRepository<User> usersRepository,
            IRepository<Protocol> protocolsRepository, IRepository<ProtocolProduct> protocolProductsRepository,
            IRepository<ProtocolProductPack> protocolProductPackRepository,
            IRepository<ProtocolSection> protocolProtocolSectionRepository,
            IRepository<Section> protocolSectionRepository,
            IRepository<ProtocolSectionProduct> protocolProtocolSectionProductsRepository,
            IRepository<ProductPackProduct> productPackProductRepository,
            IRepository<ProtocolActivity> protocolActivitiesRepository,
            IRepository<ProtocolDietaryRecommendation> protocolDietaryRecommendationRepository,
            IRepository<DietaryRecommendation> dietaryRecommendationRepository,
            IRepository<Ingredient> ingredientsRepository,
            IRepository<IngredientSubstitute> ingredientSubstitutesRepository,
            IRepository<ProductIngredient> productIngredientsRepository,
            IRepository<ProductIngredientSubstitute> productIngredientSubstitutesRepository,
            IRepository<Activity> activitiesRepository,
            IRepository<DietaryRecommendation> dietaryRecommendationsRepository,
            IRepository<FoodItem> foodItemsRepository,
            IRepository<ProtocolFoodItem> protocolFoodItemsRepository,
            IRepository<HealthQuestionnaire> healthQuestionnaiRepository,
            IRepository<ProductPackProduct> productPackProductsRepository) :
            base(productsRepository, productPackRepository, ingredientsRepository, protocolsRepository,
                ingredientSubstitutesRepository, productIngredientsRepository, productIngredientSubstitutesRepository,
                activitiesRepository, dietaryRecommendationsRepository, productPackProductsRepository, foodItemsRepository)
        {

            _logger = logger;
            _productsRepository = productsRepository;
            _productPackRepository = productPackRepository;
            _clientsRepository = clientsRepository;
            _usersRepository = usersRepository;
            _protocolsRepository = protocolsRepository;
            _protocolProductsRepository = protocolProductsRepository;
            _protocolProductPackRepository = protocolProductPackRepository;
            _protocolProtocolSectionRepository = protocolProtocolSectionRepository;
            _protocolSectionRepository = protocolSectionRepository;
            _protocolProtocolSectionProductsRepository = protocolProtocolSectionProductsRepository;
            _productPackProductRepository = productPackProductRepository;
            _protocolActivitiesRepository = protocolActivitiesRepository;
            _activitiesRepository = activitiesRepository;
            _protocolFoodItemsRepository = protocolFoodItemsRepository;
            _healthQuestionnaiRepository = healthQuestionnaiRepository;
            _productPackProductsRepository = productPackProductsRepository;
            _protocolDietaryRecommendationRepository = protocolDietaryRecommendationRepository;
            _dietaryRecommendationRepository = dietaryRecommendationRepository;
            _defaultValues = defaultValues.Value;
        }

        public Protocol FindAutoGenerated(int clientId)
        {
            return MemoryCache.GetOrCreate(GetCacheKey(clientId), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(Constants.Constants.OneWeek));

                var protocol = _protocolsRepository.Find(e => e.ClientId == clientId && e.Type == EProtocolType.AutoGenerated).FirstOrDefault();
                if (protocol != null)
                {
                    protocol = GetFullProtocol(protocol);
                }

                return protocol;
            });
        }

        public Protocol Find(int id)
        {
            return MemoryCache.GetOrCreate(GetCacheKey(id), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(Constants.Constants.OneWeek));

                var protocol = _protocolsRepository.Find(id);
                if (protocol != null)
                {
                    protocol = GetFullProtocol(protocol);
                }

                return protocol;
            });
        }

        public Protocol FindNext(int id)
        {
            var protocol = _protocolsRepository.Find(e => e.Id > id).OrderBy(e => e.Id).FirstOrDefault() ??
                           _protocolsRepository.GetQuery("SELECT TOP 1 * FROM [Protocol] ORDER BY [Id]")
                               .FirstOrDefault();
            if (protocol != null)
            {
                protocol = GetFullProtocol(protocol);
            }

            return protocol;
        }

        public Protocol FindPrevious(int id)
        {
            var protocol = _protocolsRepository.Find(e => e.Id < id).OrderByDescending(e => e.Id).FirstOrDefault() ??
                           _protocolsRepository.GetQuery("SELECT TOP 1 * FROM [Protocol] ORDER BY [Id] DESC")
                               .FirstOrDefault();
            if (protocol != null)
            {
                protocol = GetFullProtocol(protocol);
            }

            return protocol;
        }

        public Protocol Find(Guid id)
        {
            var protocol = _protocolsRepository.Find(e => e.ExternalId == id).FirstOrDefault();
            if (protocol != null)
            {
                protocol = GetFullProtocol(protocol);
            }

            return protocol;
        }

        public Protocol GetFullProtocol(Protocol protocol)
        {
            return MemoryCache.GetOrCreate(GetCacheKey(protocol.Id), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(Constants.Constants.OneWeek));

                return GetFullProtocolNoCache(protocol);
            });
        }

        public Protocol GetFullProtocolNoCache(Protocol protocol)
        {
            protocol.Activities = _protocolActivitiesRepository.Find(e => e.ProtocolId == protocol.Id).ToList();
            foreach (var protocolActivity in protocol.Activities)
            {
                protocolActivity.Activity = GetActivities().FirstOrDefault(e => e.Id == protocolActivity.ActivityId);
            }

            protocol.DietaryRecommendations = _protocolDietaryRecommendationRepository
                .Find(e => e.ProtocolId == protocol.Id).ToList();
            foreach (var protocolActivity in protocol.DietaryRecommendations)
            {
                protocolActivity.DietaryRecommendation =
                    GetDietaryRecommendations().FirstOrDefault(e => e.Id == protocolActivity.DietaryRecommendationId);
            }

            protocol.Products = _protocolProductsRepository.Find(e => e.ProtocolId == protocol.Id).ToList();
            foreach (var protocolProduct in protocol.Products)
            {
                protocolProduct.Product = GetProducts().FirstOrDefault(e => e.Id == protocolProduct.ProductId);
            }

            protocol.ProductPacks = _protocolProductPackRepository.Find(e => e.ProtocolId == protocol.Id).ToList();
            foreach (var protocolProductPack in protocol.ProductPacks)
            {
                protocolProductPack.ProductPack =
                    GetProductPacks().FirstOrDefault(e => e.Id == protocolProductPack.ProductPackId);

                protocolProductPack.ProductPack.Products =
                    _productPackProductRepository.Find(e => e.ProductPackId == protocolProductPack.ProductPack.Id)
                        .ToList();

                foreach (var productPackProduct in protocolProductPack.ProductPack.Products)
                {
                    productPackProduct.Product =
                        GetProducts().FirstOrDefault(e => e.Id == productPackProduct.ProductId);
                }
            }

            protocol.ProtocolSections = _protocolProtocolSectionRepository.Find(e => e.ProtocolId == protocol.Id)
                .OrderBy(e => e.Section.DisplayOrder).ToList();
            foreach (var section in protocol.ProtocolSections)
            {
                section.Section = _protocolSectionRepository.Find(section.SectionId);

                section.ProtocolSectionProducts =
                    _protocolProtocolSectionProductsRepository.Find(e => e.ProtocolSectionId == section.Id)
                        .ToList();

                foreach (var protocolProtocolSectionProduct in section.ProtocolSectionProducts)
                {
                    protocolProtocolSectionProduct.Product =
                        GetProducts().FirstOrDefault(e => e.Id == protocolProtocolSectionProduct.ProductId);
                    protocolProtocolSectionProduct.FormattedAmount =
                        protocolProtocolSectionProduct.GetFormattedAmount();
                }
            }

            protocol.Client = _clientsRepository.Find(protocol.ClientId ?? 0);
            protocol.ClientName = protocol.Client?.FullName;

            UpdateProtocolProductsAndProductPackQuantities(protocol);

            return protocol;
        }

        public Protocol GetProtocolWithProtocolSections(Guid id)
        {
            var protocol = _protocolsRepository.Find(e => e.ExternalId == id).FirstOrDefault();
            return GetProtocolWithProtocolSections(protocol?.Id ?? 0);
        }

        public Protocol GetProtocolWithProtocolSections(int id)
        {
            var protocol = _protocolsRepository.Find(id);
            if (protocol != null)
            {
                protocol = GetFullProtocol(protocol);
            }

            foreach (var protocolProtocolSection in protocol.ProtocolSections)
            {
                foreach (var protocolProduct in protocol.Products)
                {
                    var existing = protocolProtocolSection.ProtocolSectionProducts.FirstOrDefault(e =>
                        e.ProductId == protocolProduct.ProductId);

                    if (existing != null)
                    {
                        existing.IsVisible =
                            protocolProduct.Product.CheckRecommendations(
                                protocolProtocolSection.Section.Recommendations);
                    }
                    else
                    {
                        var protocolSectionProduct = new ProtocolSectionProduct
                        {
                            ProtocolSectionId = protocolProtocolSection.Id,
                            ProductId = protocolProduct.ProductId,
                            Product = protocolProduct.Product,
                            IsVisible = protocolProduct.Product.CheckRecommendations(protocolProtocolSection.Section
                                .Recommendations)
                        };
                        protocolProtocolSection.ProtocolSectionProducts.Add(protocolSectionProduct);
                    }
                }

                foreach (var protocolProductPack in protocol.ProductPacks)
                {
                    foreach (var productPackProduct in protocolProductPack.ProductPack.Products)
                    {
                        var existing = protocolProtocolSection.ProtocolSectionProducts.FirstOrDefault(e =>
                            e.ProductId == productPackProduct.ProductId);

                        if (existing != null)
                        {
                            existing.IsVisible =
                                productPackProduct.Product.CheckRecommendations(
                                    protocolProtocolSection.Section.Recommendations);
                        }
                        else
                        {
                            var protocolSectionProduct = new ProtocolSectionProduct
                            {
                                ProtocolSectionId = protocolProtocolSection.Id,
                                ProductId = productPackProduct.ProductId,
                                Product = productPackProduct.Product,
                                IsVisible = productPackProduct.Product.CheckRecommendations(protocolProtocolSection
                                    .Section.Recommendations)
                            };
                            protocolProtocolSection.ProtocolSectionProducts.Add(protocolSectionProduct);
                        }
                    }
                }
            }

            return protocol;
        }

        public List<Protocol> List(bool retrieveFullProtocol = false)
        {
            return MemoryCache.GetOrCreate(GetCacheKey(retrieveFullProtocol), entry =>
            {
                entry.SetOptions(GetMemoryCacheEntryOptions(SharedLibrary.Constants.OutputCacheConstants.TenMinutes));

                var protocols = _protocolsRepository.List().Where(e => !e.IsDeleted).OrderBy(e => e.Name).ToList();

                if (retrieveFullProtocol)
                {
                    var fullProtocols = new List<Protocol>();
                    foreach (var protocol in protocols)
                    {
                        fullProtocols.Add(GetFullProtocol(protocol));
                    }

                    return fullProtocols;
                }

                return protocols;
            });
        }

        public Protocol Duplicate(int id)
        {
            var protocol = _protocolsRepository.Find(id);
            if (protocol != null)
            {
                protocol = GetFullProtocolNoCache(protocol);
            }

            var newProtocolExternalId = Guid.NewGuid();

            var newProtocol = new Protocol
            {
                Name = $"{protocol.Name} (Copy)",
                ShortDescription = protocol.ShortDescription,
                Body = protocol.Body,
                ClientId = protocol.ClientId,
                ExternalId = newProtocolExternalId
            };

            _protocolsRepository.Create(newProtocol);

            // Get Id
            newProtocol = Find(newProtocolExternalId);

            // Copy products
            foreach (var protocolProduct in protocol.Products)
            {
                var newProduct = new ProtocolProduct
                {
                    ProtocolId = newProtocol.Id,
                    ProductId = protocolProduct.ProductId
                };

                _protocolProductsRepository.Create(newProduct);
            }

            // Copy product packs
            foreach (var protocolProductPack in protocol.ProductPacks)
            {
                var newProductPack = new ProtocolProductPack
                {
                    ProtocolId = newProtocol.Id,
                    ProductPackId = protocolProductPack.ProductPackId
                };

                _protocolProductPackRepository.Create(newProductPack);
            }

            // Copy sections
            foreach (var section in protocol.ProtocolSections)
            {
                var newProtocolSection = new ProtocolSection
                {
                    ProtocolId = newProtocol.Id,
                    SectionId = section.SectionId
                };

                _protocolProtocolSectionRepository.Create(newProtocolSection);

                // Copy products and product packs
                var protocolSection =
                    _protocolProtocolSectionRepository.Find(e => e.SectionId == section.SectionId).FirstOrDefault();

                var sectionProducts = _protocolProtocolSectionProductsRepository.Find(e =>
                    e.ProtocolSectionId == protocolSection.SectionId).ToList();

                foreach (var product in sectionProducts)
                {
                    // ProtocolId = newProtocol.Id,
                    var newProtocolSectionProduct = new ProtocolSectionProduct
                    {
                        ProtocolSectionId = protocolSection.SectionId,
                        ProductId = product.ProductId
                    };

                    _protocolProtocolSectionProductsRepository.Create(newProtocolSectionProduct);
                }
            }

            return newProtocol;
        }

        public void DeleteChildRecords(int id)
        {
            var protocol = Find(id);

            foreach (var product in protocol.Products)
            {
                _protocolProductsRepository.Delete(product.Id);
            }

            foreach (var pack in protocol.ProductPacks)
            {
                _protocolProductPackRepository.Delete(pack.Id);
            }

            foreach (var section in protocol.ProtocolSections)
            {
                foreach (var sectionProtocolSectionProduct in section.ProtocolSectionProducts)
                {
                    _protocolProtocolSectionProductsRepository.Delete(sectionProtocolSectionProduct.Id);
                }

                _protocolProtocolSectionRepository.Delete(section.Id);
            }
        }

        public void AddDefaultSections(int id)
        {
            var protocol = Find(id);
            AddDefaultSections(protocol);
        }

        public void UpdateProtocolProductsAndProductPackQuantities(Protocol protocol)
        {
            if (protocol.Products != null && protocol.Products.Any())
            {
                foreach (var protocolProduct in protocol.Products)
                {
                    var productSectionProducts = protocol.ProtocolSections
                        .SelectMany(s => s.ProtocolSectionProducts)
                        .Where(e => e.ProductId == protocolProduct.ProductId).ToList();

                    var numberOfProductDosesRequiredPerDay = productSectionProducts.Sum(e => e.Amount);
                    var numberPerDuration =
                        protocol.GetNumberOfDaysOnPerDuration() * numberOfProductDosesRequiredPerDay;

                    if (numberPerDuration > 0)
                    {
                        protocolProduct.AmountRequired =
                            (int)Math.Ceiling(numberPerDuration / (decimal)protocolProduct.Product.Amount);
                        protocolProduct.Amount = protocolProduct.AmountRequired;
                    }
                }
            }

            if (protocol.ProductPacks != null && protocol.ProductPacks.Any())
            {
                foreach (var protocolProductPack in protocol.ProductPacks)
                {
                    foreach (var productPackProduct in protocolProductPack.ProductPack.Products)
                    {
                        var productSectionProducts = protocol.ProtocolSections
                            .SelectMany(s => s.ProtocolSectionProducts)
                            .Where(e => e.ProductId == productPackProduct.ProductId).ToList();

                        var numberOfProductDosesRequiredPerDay = productSectionProducts.Sum(e => e.Amount);
                        var numberPerDuration =
                            protocol.GetNumberOfDaysOnPerDuration() * numberOfProductDosesRequiredPerDay;

                        if (numberPerDuration > 0)
                        {
                            productPackProduct.AmountRequired =
                                (int)Math.Ceiling(numberPerDuration /
                                                   (decimal)productPackProduct.Product.Amount);
                        }
                    }
                }
            }
        }

        public void UpdateSectionDetails(Protocol protocol)
        {
            foreach (var protocolProtocolSection in protocol.ProtocolSections)
            {
                foreach (var protocolProduct in protocolProtocolSection.ProtocolSectionProducts)
                {
                    var items = _protocolProtocolSectionProductsRepository.Find(e =>
                        e.ProtocolSectionId == protocolProtocolSection.Id && e.ProductId == protocolProduct.Product.Id);
                    var item = items.FirstOrDefault();
                    var product = _productsRepository.Find(protocolProduct.Product.Id);

                    if (protocolProduct.Amount == 0 && !items.Any())
                    {
                        continue;
                    }

                    if (protocolProduct.Amount > 0 && !items.Any())
                    {
                        // Create new
                        var newItem = new ProtocolSectionProduct
                        {
                            ProtocolSectionId = protocolProtocolSection.Id,
                            ProductId = protocolProduct.Product.Id,
                            Amount = protocolProduct.Amount
                        };

                        var sectionMessage =
                            $"ProductName: {product.Name}, {nameof(ProtocolSection.SectionName)}: {protocolProtocolSection.Section.Name}";
                        var acceptableMessage = string.Format(Globalisation.Dictionary.AcceptableValuesMessage,
                            product.MinDosage, product.MaxDosage);

                        if (newItem.Amount > product.MaxDosage)
                        {
                            throw new ArgumentOutOfRangeException("Amount",
                                $"{Globalisation.Dictionary.ValueTooHighException} {acceptableMessage} {sectionMessage}");
                            ;
                        }

                        if (newItem.Amount < product.MinDosage)
                        {
                            throw new ArgumentOutOfRangeException("Amount",
                                $"{Globalisation.Dictionary.ValueTooLowException} {acceptableMessage} {sectionMessage}");
                            ;
                        }

                        _protocolProtocolSectionProductsRepository.Create(newItem);
                    }
                    else if (items.Any() && protocolProduct.Amount == 0)
                    {
                        // Remove item
                        _protocolProtocolSectionProductsRepository.Delete(item.Id);
                    }
                    else if (items.Any() && item.Amount != protocolProduct.Amount)
                    {
                        // Change amount
                        item.Amount = protocolProduct.Amount;
                        _protocolProtocolSectionProductsRepository.Update(item);
                    }
                }
            }
        }

        public static bool CheckSchedule(Protocol protocol, DayOfWeek dayofWeek)
        {
            if (protocol.ProtocolFrequency == EProtocolFrequency.Daily)
            {
                if (protocol.NumberOfPeriodsOff >= 7)
                {
                    return false;
                }

                switch (dayofWeek)
                {
                    case DayOfWeek.Monday:
                        return true;

                    case DayOfWeek.Tuesday:
                        return protocol.NumberOfPeriodsOff <= 2;

                    case DayOfWeek.Wednesday:
                        return new[] { 4, 3, 1, 0 }.Contains(protocol.NumberOfPeriodsOff);

                    case DayOfWeek.Thursday:
                        return new[] { 5, 2, 1, 0 }.Contains(protocol.NumberOfPeriodsOff);

                    case DayOfWeek.Friday:
                        return protocol.NumberOfPeriodsOff <= 4;

                    case DayOfWeek.Saturday:
                        return protocol.NumberOfPeriodsOff <= 3;

                    case DayOfWeek.Sunday:
                        return protocol.NumberOfPeriodsOff == 0;
                }
            }

            return false;
        }

        public static int[] GetSchedule(Protocol protocol)
        {
            if (protocol.ProtocolFrequency == EProtocolFrequency.Monthly)
            {
                var intervalLength = protocol.GetPeriodLength();
                var interval = (int)Math.Floor((double)intervalLength / protocol.NumberOfPeriodsOn);
                var list = new List<int>();

                for (int i = 1; i <= intervalLength; i += interval)
                {
                    list.Add(i);
                }

                return list.ToArray();
            }

            return null;
        }

        public void CheckProductsAndProductPacksDoNotOverlap(Protocol protocol)
        {
            if (protocol.ProtocolProductPacks != null && protocol.ProtocolProductPacks.Any() &&
                protocol.ProtocolProducts != null && protocol.ProtocolProducts.Any())
            {
                // Check that the products in the product packs don't duplicate the products and raise an error if so!
                if (protocol.ProtocolProductPacks.SelectMany(e => e.ProductPack.Products).Select(e => e.ProductId)
                    .Any(pid => protocol.ProtocolProducts.Select(pp => pp.ProductId).Contains(pid)))
                {
                    throw new Exception(Globalisation.Dictionary.ProtocolProductsAndProductPacksOverlap);
                }
            }
        }
        
        private void AddDefaultSections(Protocol protocol)
        {
            if (!protocol.ProtocolSections.Any())
            {
                var defaultSections = _protocolSectionRepository.List();

                // Copy default sections
                foreach (var section in defaultSections)
                {
                    var newProtocolSection = new ProtocolSection
                    {
                        ProtocolId = protocol.Id,
                        SectionId = section.Id
                    };

                    _protocolProtocolSectionRepository.Create(newProtocolSection);
                }
            }
        }

    }
}
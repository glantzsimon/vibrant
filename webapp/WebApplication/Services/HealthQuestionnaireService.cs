using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Attributes;
using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Models;
using K9.WebApplication.Config;
using K9.WebApplication.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace K9.WebApplication.Services
{
    public class HealthQuestionnaireService : CacheableServiceBase<HealthQuestionnaire>, IHealthQuestionnaireService
    {
        private readonly IRepository<HealthQuestionnaire> _healthQuestionnaireRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<Client> _clientsRepository;
        private readonly IClientService _clientService;
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<ProductPack> _productPacksRepository;
        private readonly IRepository<Protocol> _protocolsRepository;
        private readonly IRepository<Activity> _activitiesRepository;
        private readonly IRepository<DietaryRecommendation> _dietaryRecommendationsRepository;
        private readonly IRepository<FoodItem> _foodItemsRepository;
        private readonly IRepository<ProtocolActivity> _protocolActivitiesRepository;
        private readonly IRepository<ProtocolDietaryRecommendation> _protocolDietaryRecommendationRepository;
        private readonly IRepository<ProtocolFoodItem> _protocolFoodItemsRepository;
        private readonly IRepository<ProtocolProductPack> _protocolProductPackRepository;
        private readonly IRepository<ProtocolProduct> _protocolProductsRepository;
        private readonly IProtocolService _protocolService;

        public HealthQuestionnaireService(ILogger logger,
            IRepository<Product> productsRepository,
            IRepository<ProductPack> productPackRepository, IOptions<DefaultValuesConfiguration> defaultValues,
            IRepository<HealthQuestionnaire> healthQuestionnaireRepository,
            IRepository<User> usersRepository,
            IRepository<Client> clientsRepository,
            IClientService clientService,
            IRepository<ProductPack> productPacksRepository,
            IRepository<Protocol> protocolsRepository, IRepository<Activity> activitiesRepository,
            IRepository<DietaryRecommendation> dietaryRecommendationsRepository,
            IRepository<FoodItem> foodItemsRepository,
            IRepository<Ingredient> ingredientsRepository,
            IRepository<IngredientSubstitute> ingredientSubstitutesRepository,
            IRepository<ProtocolActivity> protocolActivitiesRepository,
            IRepository<ProductIngredient> productIngredientsRepository,
            IRepository<ProductIngredientSubstitute> productIngredientSubstitutesRepository,
            IRepository<ProtocolDietaryRecommendation> protocolDietaryRecommendationRepository,
            IRepository<ProtocolFoodItem> protocolFoodItemsRepository,
            IRepository<ProtocolProductPack> protocolProductPackRepository,
            IRepository<ProductPackProduct> productPackProductsRepository,
            IRepository<ProtocolProduct> protocolProductsRepository,
            IProtocolService protocolService) :
            base(productsRepository, productPackRepository, ingredientsRepository, protocolsRepository,
                ingredientSubstitutesRepository, productIngredientsRepository, productIngredientSubstitutesRepository,
                activitiesRepository, dietaryRecommendationsRepository, productPackProductsRepository,
                foodItemsRepository)
        {
            _healthQuestionnaireRepository = healthQuestionnaireRepository;
            _usersRepository = usersRepository;
            _clientsRepository = clientsRepository;
            _clientService = clientService;
            _productsRepository = productsRepository;
            _productPacksRepository = productPacksRepository;
            _protocolsRepository = protocolsRepository;
            _activitiesRepository = activitiesRepository;
            _dietaryRecommendationsRepository = dietaryRecommendationsRepository;
            _foodItemsRepository = foodItemsRepository;
            _protocolActivitiesRepository = protocolActivitiesRepository;
            _protocolDietaryRecommendationRepository = protocolDietaryRecommendationRepository;
            _protocolFoodItemsRepository = protocolFoodItemsRepository;
            _protocolProductPackRepository = protocolProductPackRepository;
            _protocolProductsRepository = protocolProductsRepository;
            _protocolService = protocolService;
        }

        public HealthQuestionnaire GetHealthQuestionnaireForUser(int userId)
        {
            var user = _usersRepository.Find(userId);
            var client = _clientsRepository.Find(e => e.UserId == userId).FirstOrDefault();

            if (client == null)
            {
                client = _clientsRepository.Find(e => e.EmailAddress == user.EmailAddress).FirstOrDefault();

                if (client == null)
                {
                    client = _clientService.GetOrCreateClient("", user.FullName, user.EmailAddress,
                        user.PhoneNumber,
                        user.Id);
                }
            }

            return GetHealthQuestionnaireForClient(client.Id);
        }

        public HealthQuestionnaire GetHealthQuestionnaireForClient(int clientId)
        {
            var client = _clientsRepository.Find(clientId);
            return GetHealthQuestionnaireForClient(client);
        }

        public GeneticProfileMatchedItemsViewModel GetGeneticProfileMatchedItems(int id)
        {
            var hq = _healthQuestionnaireRepository.Find(id);
            if (hq == null)
            {
                return null;
            }

            var genoType = hq.CalculateGenotype();

            return new GeneticProfileMatchedItemsViewModel
            {
                Products = GetGenoTypeFilteredItems(hq, genoType.GenoType,
                    new List<Product>(_productsRepository.List()), 5),
                ProductPacks = GetGenoTypeFilteredItems(hq, genoType.GenoType,
                    new List<ProductPack>(_productPacksRepository.List()), 3),
                DietaryRecommendations = GetGenoTypeFilteredItems(hq, genoType.GenoType,
                    new List<DietaryRecommendation>(_dietaryRecommendationsRepository.List())),
                Activities =
                    GetGenoTypeFilteredItems(hq, genoType.GenoType, new List<Activity>(_activitiesRepository.List())),
                Foods = GetGenoTypeFilteredItems(hq, genoType.GenoType, new List<FoodItem>(_foodItemsRepository.List()))
            };
        }

        public List<Protocol> GetGeneticProfileMatchedProtocols(int clientId)
        {
            var hq = GetHealthQuestionnaireForClient(clientId);
            if (hq == null)
            {
                return null;
            }

            var genoType = hq.CalculateGenotype();

            return GetGenoTypeFilteredItems(hq, genoType.GenoType, new List<Protocol>(_protocolsRepository.List()), 3);
        }

        public void Save(HealthQuestionnaire model)
        {
            _healthQuestionnaireRepository.Update(model);
        }

        public Protocol GetAutoGeneratedProtocolFromGeneticProfile(int clientId)
        {
            var hq = GetHealthQuestionnaireForClient(clientId);
            if (hq != null)
            {
                return GetAutoGeneratedProtocolFromGeneticProfile(hq);
            }

            return null;
        }

        public Protocol GetAutoGeneratedProtocolFromGeneticProfile(HealthQuestionnaire hq)
        {
            var protocol = _protocolsRepository
                .Find(e => e.Type == EProtocolType.AutoGenerated && e.ClientId == hq.ClientId).FirstOrDefault();
            if (protocol != null)
            {
                return _protocolService.Find(protocol.Id);
            }

            return null;
        }

        public void AutoGenerateProtocolFromGeneticProfile(int clientId)
        {
            var hq = GetHealthQuestionnaireForClient(clientId);
            if (hq != null)
            {
                AutoGenerateProtocolFromGeneticProfile(hq);
            }
        }

        public void AutoGenerateProtocolFromGeneticProfile(HealthQuestionnaire hq)
        {
            var protocol = _protocolsRepository
                .Find(e => e.Type == EProtocolType.AutoGenerated && e.ClientId == hq.ClientId).FirstOrDefault();

            if (protocol == null)
            {
                // Create new one
                var externalId = Guid.NewGuid();
                protocol = new Protocol
                {
                    Name = Globalisation.Dictionary.GenoTypePersonalisedProtocol,
                    ShortDescription = K9.Globalisation.Dictionary.GeneticProfileProtocolDescription,
                    ClientId = hq.ClientId,
                    ExternalId = externalId,
                    Type = EProtocolType.AutoGenerated,
                    Period = EPeriod.Days,
                    ProtocolFrequency = EProtocolFrequency.Daily,
                    NumberOfPeriodsOff = 1,
                    Duration = EProtocolDuration.ThreeMonths
                };

                _protocolsRepository.Create(protocol);
                protocol = _protocolsRepository.Find(e => e.ExternalId == externalId).First();
            }

            DeleteProtocolChildRecords(protocol.Id);
            RecreateChildRecords(hq, protocol);
        }

        private void RecreateChildRecords(HealthQuestionnaire hq, Protocol protocol)
        {
            var matchedItems = GetGeneticProfileMatchedItems(hq.Id);

            protocol.Activities = matchedItems.Activities.Select(e => new ProtocolActivity
            {
                ProtocolId = protocol.Id,
                ActivityId = e.ActivityId
            }).ToList();

            protocol.DietaryRecommendations = matchedItems.DietaryRecommendations.Select(e =>
                new ProtocolDietaryRecommendation
                {
                    ProtocolId = protocol.Id,
                    DietaryRecommendationId = e.DietaryRecommendationId
                }).ToList();

            protocol.RecommendedFoods = matchedItems.Foods.Select(e => new ProtocolFoodItem
            {
                ProtocolId = protocol.Id,
                FoodItemId = e.FoodItemId,
            }).ToList();

            protocol.Products = matchedItems.Products.Select(e => new ProtocolProduct
            {
                ProtocolId = protocol.Id,
                ProductId = e.ProductId,
            }).ToList();

            protocol.ProductPacks = matchedItems.ProductPacks.Select(e => new ProtocolProductPack
            {
                ProtocolId = protocol.Id,
                ProductPackId = e.ProductPackId,
            }).ToList();

            _protocolActivitiesRepository.CreateBatch(protocol.Activities);
            _protocolDietaryRecommendationRepository.CreateBatch(protocol.DietaryRecommendations);
            _protocolFoodItemsRepository.CreateBatch(protocol.RecommendedFoods);
            _protocolProductsRepository.CreateBatch(protocol.Products);
            _protocolProductPackRepository.CreateBatch(protocol.ProductPacks);
        }

        private void DeleteProtocolChildRecords(int id)
        {
            var activities = _protocolActivitiesRepository.Find(e => e.ProtocolId == id).ToList();
            if (activities != null)
            {
                _protocolActivitiesRepository.DeleteBatch(activities);
            }

            var dietaryRecommendations = _protocolDietaryRecommendationRepository.Find(e => e.ProtocolId == id).ToList();
            if (dietaryRecommendations != null)
            {
                _protocolDietaryRecommendationRepository.DeleteBatch(dietaryRecommendations);
            }

            var recommendedFoods = _protocolFoodItemsRepository.Find(e => e.ProtocolId == id).ToList();
            if (recommendedFoods != null)
            {
                _protocolFoodItemsRepository.DeleteBatch(recommendedFoods);
            }

            var products = _protocolProductsRepository.Find(e => e.ProtocolId == id).ToList();
            if (products != null)
            {
                _protocolProductsRepository.DeleteBatch(products);
            }

            var productPacks = _protocolProductPackRepository.Find(e => e.ProtocolId == id).ToList();
            if (productPacks != null)
            {
                _protocolProductPackRepository.DeleteBatch(productPacks);
            }
        }

        private List<T> GetGenoTypeFilteredItems<T>(HealthQuestionnaire hq, EGenoType genoType, List<T> items, int take = -1) where T : GenoTypeBase
        {
            foreach (var item in items)
            {
                item.Score = GetGenoTypeItemScore(hq, genoType, item);
            }

            var results = items
                    .Where(e => e.Score > 0)
                    .OrderByDescending(e => e.Score).ToList();

            if (take > 0)
            {
                results = results.Take(take).ToList();
            }

            return results;
        }

        private int GetGenoTypeItemScore(HealthQuestionnaire hq, EGenoType genoType, GenoTypeBase item)
        {
            var score = 0;

            if (hq.DietaryPreference == EDietaryPreference.Vegetarian)
            {
                if (item.Carnivore || item.Pescatarian)
                {
                    return 0;
                }
            }
            if (hq.DietaryPreference == EDietaryPreference.Vegan)
            {
                if (item.Vegetarian || item.Carnivore || item.Pescatarian)
                {
                    return 0;
                }
            }
            if (hq.DietaryPreference == EDietaryPreference.Fruitarian)
            {
                if (item.Vegan || item.Vegetarian || item.Carnivore || item.Pescatarian)
                {
                    return 0;
                }
            }
            if (hq.DietaryPreference == EDietaryPreference.Carnivore)
            {
                if (item.Vegan || item.Vegetarian || item.Fruitarian)
                {
                    return 0;
                }
            }
            if (hq.DietaryPreference == EDietaryPreference.Pescatarian)
            {
                if (item.Carnivore)
                {
                    return 0;
                }
            }

            var scoredProperties = hq.GetPropertiesWithAttribute(typeof(ScoreAttribute)).ToList();
            foreach (var scoredProperty in scoredProperties)
            {
                var scoredAttribute = scoredProperty.GetAttribute<ScoreAttribute>();
                var attributeProperties = scoredAttribute.GetProperties();
                foreach (var attributeProperty in attributeProperties.Where(e => item.HasProperty(e.Name)))
                {
                    var attributeValue = scoredAttribute.GetProperty(attributeProperty);
                    var itemValue = item.GetProperty(attributeProperty.Name);

                    if (attributeValue == itemValue)
                    {
                        score++;
                    }
                }
            }

            // Custom scores
            var scores = GetType().Assembly.GetTypes()
                .Where(p => p.IsClass && typeof(IScore).IsAssignableFrom(p)).ToList();

            foreach (var scoreImplementation in scores)
            {
                var instance = Activator.CreateInstance(scoreImplementation) as IScore;
                score += instance.GetScore(hq, genoType, item);
            }

            return score;
        }

        private HealthQuestionnaire GetHealthQuestionnaireForClient(Client client)
        {
            var hq = _healthQuestionnaireRepository.Find(e => e.ClientId == client.Id).FirstOrDefault();

            if (hq == null)
            {
                var hqId = Guid.NewGuid();

                hq = new HealthQuestionnaire
                {
                    ExternalId = hqId,
                    ClientId = client.Id,
                    Name = $"{client.Name} - {Globalisation.Dictionary.HealthQuestionnaire}",
                    CookingFrequency = EFrequency.SeveralTimesAWeek,
                };

                _healthQuestionnaireRepository.Create(hq);

                hq = _healthQuestionnaireRepository.Find(e => e.ExternalId == hqId).First();
            }

            hq.Client = client;
            return hq;
        }

    }
}
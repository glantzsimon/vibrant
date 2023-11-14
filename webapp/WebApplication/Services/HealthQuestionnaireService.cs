using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using K9.WebApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace K9.WebApplication.Services
{
    public class HealthQuestionnaireService : IQuestionnaireService
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

        public HealthQuestionnaireService(IRepository<HealthQuestionnaire> healthQuestionnaireRepository,
            IRepository<User> usersRepository, IRepository<Client> clientsRepository, IClientService clientService,
            IRepository<Product> productsRepository, IRepository<ProductPack> productPacksRepository,
            IRepository<Protocol> protocolsRepository, IRepository<Activity> activitiesRepository,
            IRepository<DietaryRecommendation> dietaryRecommendationsRepository,
            IRepository<FoodItem> foodItemsRepository)
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

        public GenoTypeBaseItemsViewModel GetGenoTypeBaseItems(int id)
        {
            var hq = _healthQuestionnaireRepository.Find(id);
            if (hq == null)
            {
                return null;
            }

            var genoType = hq.CalculateGenotype();

            return new GenoTypeBaseItemsViewModel
            {
                Products = GetGenoTypeFilteredItems(hq, genoType.GenoType, new List<Product>(_productsRepository.List())),
                ProductPacks = GetGenoTypeFilteredItems(hq, genoType.GenoType, new List<ProductPack>(_productPacksRepository.List())),
                Protocols = GetGenoTypeFilteredItems(hq, genoType.GenoType, new List<Protocol>(_protocolsRepository.List())),
                DietaryRecommendations = GetGenoTypeFilteredItems(hq, genoType.GenoType, new List<DietaryRecommendation>(_dietaryRecommendationsRepository.List())),
                Activities = GetGenoTypeFilteredItems(hq, genoType.GenoType, new List<Activity>(_activitiesRepository.List())),
                Foods = GetGenoTypeFilteredItems(hq, genoType.GenoType, new List<FoodItem>(_foodItemsRepository.List()))
            };
        }

        public void Save(HealthQuestionnaire model)
        {
            _healthQuestionnaireRepository.Update(model);
        }

        private List<T> GetGenoTypeFilteredItems<T>(HealthQuestionnaire hq, EGenoType genoType, List<T> items) where T : GenoTypeBase
        {
            foreach (var item in items)
            {
                item.Score = GetGenoTypeItemScore(hq, genoType, item);
            }

            return items.OrderByDescending(e => e.Score).ToList();
        }

        private int GetGenoTypeItemScore(HealthQuestionnaire hq, EGenoType genoType, GenoTypeBase item)
        {
            var score = 0;
            var scores = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IScore).IsAssignableFrom(p)).ToList();

            foreach (var scoreItem in scores)
            {
                var instance = Activator.CreateInstance(scoreItem) as IScore;
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
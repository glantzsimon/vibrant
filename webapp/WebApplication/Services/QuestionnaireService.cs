using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using System;
using System.Linq;

namespace K9.WebApplication.Services
{
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly IRepository<HealthQuestionnaire> _healthQuestionnaireRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<Client> _clientsRepository;
        private readonly IClientService _clientService;

        public QuestionnaireService(IRepository<HealthQuestionnaire> healthQuestionnaireRepository, IRepository<User> usersRepository, IRepository<Client> clientsRepository, IClientService clientService)
        {
            _healthQuestionnaireRepository = healthQuestionnaireRepository;
            _usersRepository = usersRepository;
            _clientsRepository = clientsRepository;
            _clientService = clientService;
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
            var hq = _healthQuestionnaireRepository.Find(e => e.ClientId == clientId).FirstOrDefault();
            var client = _clientsRepository.Find(clientId);

            if (hq == null)
            {
                var hqId = Guid.NewGuid();

                hq = new HealthQuestionnaire
                {
                    ExternalId = hqId,
                    ClientId = clientId,
                    Name = $"{client.Name} - {Globalisation.Dictionary.HealthQuestionnaire}"
                };

                _healthQuestionnaireRepository.Create(hq);


                hq = _healthQuestionnaireRepository.Find(e => e.ExternalId == hqId).First();
            }

            return hq;
        }
    }
}
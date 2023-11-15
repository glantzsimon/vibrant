using K9.DataAccessLayer.Models;
using K9.WebApplication.ViewModels;
using System.Collections.Generic;

namespace K9.WebApplication.Services
{
    public interface IHealthQuestionnaireService
    {
        HealthQuestionnaire GetHealthQuestionnaireForUser(int userId);
        HealthQuestionnaire GetHealthQuestionnaireForClient(int clientId);
        void Save(HealthQuestionnaire model);
        GeneticProfileMatchedItemsViewModel GetGeneticProfileMatchedItems(int id);
        List<Protocol> GetGeneticProfileMatchedProtocols(int clientId);
    }
}
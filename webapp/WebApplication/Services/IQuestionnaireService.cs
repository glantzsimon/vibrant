using K9.DataAccessLayer.Models;
using K9.WebApplication.ViewModels;

namespace K9.WebApplication.Services
{
    public interface IQuestionnaireService
    {
        HealthQuestionnaire GetHealthQuestionnaireForUser(int userId);
        HealthQuestionnaire GetHealthQuestionnaireForClient(int clientId);
        void Save(HealthQuestionnaire model);
        GeneticProfileMatchedItemsViewModel GetGeneticProfileMatchedItems(int id);
    }
}
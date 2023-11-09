using K9.DataAccessLayer.Models;

namespace K9.WebApplication.Services
{
    public interface IQuestionnaireService
    {
        HealthQuestionnaire GetHealthQuestionnaireForUser(int userId);
        HealthQuestionnaire GetHealthQuestionnaireForClient(int clientId);
    }
}
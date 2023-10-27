using K9.DataAccessLayer.Models;

namespace K9.WebApplication.Services
{
    public interface IConsultationService
    {
        void CreateConsultation(Consultation consultation, Client client);
    }
}
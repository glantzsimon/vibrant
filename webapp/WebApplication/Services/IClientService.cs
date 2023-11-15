using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Models;
using System.Collections.Generic;

namespace K9.WebApplication.Services
{
    public interface IClientService
    {
        Client GetOrCreateClient(string stripeCustomerId, string fullName, string emailAddress, string phoneNumber = "",
            int? userId = null);
        Client GetOrCreateClientFromUser(User user);
        Client Find(int id);
        Client FindFromUser(int userId);
        Client Find(string emailAddress);
        List<Client> ListClients();
        bool Unsubscribe(string code);
    }
}
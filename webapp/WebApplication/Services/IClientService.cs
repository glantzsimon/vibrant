using K9.DataAccessLayer.Models;
using System.Collections.Generic;

namespace K9.WebApplication.Services
{
    public interface IClientService
    {
        Client GetOrCreateClient(string stripeCustomerId, string fullName, string emailAddress, string phoneNumber = "");
        Client Find(int id);
        Client Find(string emailAddress);
        List<Client> ListClients();
        bool Unsubscribe(string code);
    }
}
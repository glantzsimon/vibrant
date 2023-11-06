using K9.DataAccessLayer.Models;
using K9.SharedLibrary.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using K9.SharedLibrary.Extensions;

namespace K9.WebApplication.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Client> _clientsRepository;
        private readonly ILogger _logger;

        public ClientService(IRepository<Client> clientsRepository, ILogger logger)
        {
            _clientsRepository = clientsRepository;
            _logger = logger;
        }

        public Client GetOrCreateClient(string stripeCustomerId, string fullName, string emailAddress, string phoneNumber = "", int? userId = null)
        {
            if (!string.IsNullOrEmpty(emailAddress))
            {
                try
                {
                    var existingCustomer = _clientsRepository.Find(_ => _.StripeCustomerId == stripeCustomerId || _.EmailAddress == emailAddress || _.UserId == userId).FirstOrDefault();
                    if (existingCustomer == null)
                    {
                        _clientsRepository.Create(new Client
                        {
                            StripeCustomerId = stripeCustomerId,
                            FullName = string.IsNullOrEmpty(fullName) ? emailAddress : fullName,
                            EmailAddress = emailAddress,
                            PhoneNumber = phoneNumber,
                            UserId = userId
                        });
                        return _clientsRepository.Find(e => e.StripeCustomerId == stripeCustomerId || e.EmailAddress == emailAddress || e.UserId == userId).FirstOrDefault();
                    }

                    var isUpdated = false;
                    if (existingCustomer.FullName != fullName)
                    {
                        existingCustomer.FullName = fullName;
                        isUpdated = true;
                    }

                    if (existingCustomer.EmailAddress != emailAddress)
                    {
                        existingCustomer.EmailAddress = emailAddress;
                        isUpdated = true;
                    }

                    if (existingCustomer.UserId != userId)
                    {
                        existingCustomer.UserId = userId;
                        isUpdated = true;
                    }

                    if (existingCustomer.StripeCustomerId != stripeCustomerId)
                    {
                        existingCustomer.StripeCustomerId = stripeCustomerId;
                        isUpdated = true;
                    }

                    if (isUpdated)
                    {
                        _clientsRepository.Update(existingCustomer);
                    }

                    return existingCustomer;
                }
                catch (Exception e)
                {
                    _logger.Error($"ClientService => CreateCustomer => {e.GetFullErrorMessage()}");
                    throw;
                }
            }

            _logger.Error($"ClientService => CreateCustomer => Email Address is Empty");
            return null;
        }

        public Client Find(int id)
        {
            return _clientsRepository.Find(id);
        }

        public Client Find(string emailAddress)
        {
            return _clientsRepository.Find(e => e.EmailAddress == emailAddress).FirstOrDefault();
        }

        public List<Client> ListClients()
        {
            return _clientsRepository.List().OrderBy(e => e.FullName).ToList();
        }

        public bool Unsubscribe(string code)
        {
            var client = _clientsRepository.Find(e => e.Name == code).FirstOrDefault();
            if (client != null)
            {
                client.IsUnsubscribed = true;
                _clientsRepository.Update(client);
                return true;
            }

            return false;
        }
    }
}
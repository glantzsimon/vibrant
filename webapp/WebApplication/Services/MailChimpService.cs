using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Models;
using MailChimp.Net;
using MailChimp.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using MailChimpConfiguration = K9.WebApplication.Config.MailChimpConfiguration;

namespace K9.WebApplication.Services
{
    public class MailChimpService : IMailChimpService
    {
        private readonly IClientService _clientService;
        private readonly MailChimpConfiguration _mailChimpConfig;

        public MailChimpService(IOptions<MailChimpConfiguration> mailChimpConfig, IClientService clientService)
        {
            _clientService = clientService;
            _mailChimpConfig = mailChimpConfig.Value;
        }

        public void AddClient(string firstName, string lastName, string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                throw new Exception("Email address cannot be empty");
            }

            var mailChimpManager = new MailChimpManager(_mailChimpConfig.MailChimpApiKey);

            mailChimpManager.Members.AddOrUpdateAsync(_mailChimpConfig.MailChimpListId, new Member
            {
                EmailAddress = emailAddress,
                Status = Status.Subscribed,
                StatusIfNew = Status.Subscribed,
                MergeFields = new Dictionary<string, object>
                {
                    {"FNAME", firstName},
                    {"LNAME", lastName},
                }
            });
        }

        public void AddClient(string name, string emailAddress)
        {
            var names = name.Split(' ');
            var firstName = names.FirstOrDefault().ToProperCase();
            var lastName = names.LastOrDefault().ToProperCase();
            lastName = lastName == firstName ? string.Empty : lastName;

            AddClient(firstName, lastName, emailAddress);
        }

        public void AddAllClients()
        {
            foreach (var client in _clientService.ListClients())
            {
                AddClient(client.FullName, client.EmailAddress);
            }
        }
    }
}
using K9.DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace K9.WebApplication.Models
{
    public class AddressLabelViewModel
    {
        public const int MaxRecipientsPerPage = 6;
        public const int MaxRecipientsPerSection = 3;

        public List<Client> Recipients { get; }
        public Client Sender { get; set; }

        public AddressLabelViewModel(List<Client> recipients)
        {
            if (recipients.Count >= 6)
            {
                Recipients = recipients.Take(6).ToList();
                return;
            }

            var allRecipients = new List<Client>(recipients);
            switch (recipients.Count)
            {
                case 5:
                    allRecipients.Add(recipients.First());
                    break;

                case 4:
                    allRecipients.Add(recipients[0]);
                    allRecipients.Add(recipients[1]);
                    break;

                case 3:
                    allRecipients.Add(recipients[0]);
                    allRecipients.Add(recipients[1]);
                    allRecipients.Add(recipients[3]);
                    break;

                case 2:
                    allRecipients.Add(recipients[0]);
                    allRecipients.Add(recipients[0]);
                    allRecipients.Add(recipients[1]);
                    allRecipients.Add(recipients[1]);
                    break;

                case 1:
                    allRecipients.Add(recipients[0]);
                    allRecipients.Add(recipients[0]);
                    allRecipients.Add(recipients[0]);
                    allRecipients.Add(recipients[0]);
                    allRecipients.Add(recipients[0]);
                    break;
            }

            Recipients = allRecipients;
        }
    }
}
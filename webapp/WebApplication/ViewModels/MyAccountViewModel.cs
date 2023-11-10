using K9.Base.DataAccessLayer.Models;
using K9.DataAccessLayer.Models;
using System.Collections.Generic;

namespace K9.WebApplication.ViewModels
{
    public class MyAccountViewModel
    {
        public User User { get; set; }
        public Client Client { get; set; }
        public UserMembership Membership { get; set; }
        public List<Protocol> Protocols { get; set; }
        public List<Order> Orders { get; set; }
        public HealthQuestionnaire HealthQuestionnaire { get; set; }
    }
}
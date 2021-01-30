using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Base.Globalisation;
using K9.SharedLibrary.Attributes;
using K9.SharedLibrary.Authentication;
using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Grammar(ResourceType = typeof(Dictionary), DefiniteArticleName = Strings.Grammar.MasculineDefiniteArticle, IndefiniteArticleName = Strings.Grammar.MasculineIndefiniteArticle)]
    [Name(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Names.MembershipOption, PluralName = Globalisation.Strings.Names.MembershipOptions)]
    [DefaultPermissions(Role = RoleNames.DefaultUsers)]
    public class UserMembership : ObjectBase, IUserData
    {
        [UIHint("User")]
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [UIHint("MembershipOption")]
        [Required]
        [ForeignKey("MembershipOption")]
        public int MembershipOptionId { get; set; }

        [Required]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.StartsOnLabel)]
        public DateTime StartsOn { get; set; }

        [Required]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.EndsOnLabel)]
        public DateTime EndsOn { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.AutoRenewLabel)]
        public bool IsAutoRenew { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.DeactivatedLabel)]
        public bool IsDeactivated { get; set; }

        public virtual User User { get; set; }

        public virtual MembershipOption MembershipOption { get; set; }

        public virtual ICollection<UserConsultation> UserConsultations { get; set; }

        [LinkedColumn(LinkedTableName = "User", LinkedColumnName = "Username")]
        public string UserName { get; set; }

        [LinkedColumn(LinkedTableName = "MembershipOption", LinkedColumnName = "Description")]
        public string MembershipOptionName { get; set; }

        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.NumberOfConsultationsLabel)]
        public int NumberOfConsultationsLeft => MembershipOption.NumberOfConsultations - UserConsultations.Where(e => e.UserMembershipId.HasValue && e.UserMembershipId == Id)?.Count() ?? 0;

        [Display(ResourceType = typeof(Globalisation.Dictionary),
            Name = Globalisation.Strings.Labels.NumberOfConsultationsLabel)]
        public string NumberOfConsultationsLeftText => NumberOfConsultationsLeft.ToString();

        [NotMapped]
        [Display(ResourceType = typeof(Globalisation.Dictionary), Name = Globalisation.Strings.Labels.NumberOfCreditsLeft)]
        public int NumberOfCreditsLeft { get; set; }

        public bool IsActive => DateTime.Today.IsBetween(StartsOn.Date, EndsOn.Date) && !IsDeactivated;

        public TimeSpan Duration => EndsOn.Subtract(StartsOn);

        public double CostOfRemainingActiveSubscription => GetCostOfRemainingActiveSubscription();

        private double GetCostOfRemainingActiveSubscription()
        {
            var timeRemaining = EndsOn.Subtract(DateTime.Today);
            var percentageRemaining = (double)timeRemaining.Ticks / (double)Duration.Ticks;
            return MembershipOption?.Price * percentageRemaining ?? 0;
        }
    }
}

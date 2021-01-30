using K9.WebApplication.Models;
using System.Collections.Generic;
using System.Linq;

namespace K9.WebApplication.ViewModels
{
    public class MembershipViewModel
    {
        public List<MembershipModel> MembershipModels { get; set; }

        public MembershipModel MonthlyMembershipModel =>
            MembershipModels.FirstOrDefault(e => e.MembershipOption.IsMonthly);

        public MembershipModel FreeMembershipModel =>
            MembershipModels.FirstOrDefault(e => e.MembershipOption.IsFree);

        public int MonthlyMaxNumberOfConsultations =>
            MonthlyMembershipModel?.MembershipOption?.NumberOfConsultations ?? 3;
        
        public int FreeMaxNumberOfProfileReadings =>
            FreeMembershipModel?.MembershipOption?.NumberOfConsultations ?? 1;
    }
}
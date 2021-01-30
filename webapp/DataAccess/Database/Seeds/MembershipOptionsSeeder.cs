using K9.DataAccessLayer.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace K9.DataAccessLayer.Database.Seeds
{
    public static class MembershipOptionsSeeder
    {
        public static void Seed(DbContext context)
        {
            AddMembershipOption(context, "FreeMembership", "free_membership_description", MembershipOption.ESubscriptionType.Free, 0, 1);
            AddMembershipOption(context, "MonthlyStandardMembership", "standard_monthly_membership_description", MembershipOption.ESubscriptionType.MonthlyStandard, 67, 3);
            AddMembershipOption(context, "YearlyStandardMembership", "standard_annual_membership_description", MembershipOption.ESubscriptionType.AnnualStandard, 970, 36);
            AddMembershipOption(context, "MonthlyPlatinumMembership", "platinum_monthly_membership_description", MembershipOption.ESubscriptionType.MonthlyPlatinum, 270, 9);
            AddMembershipOption(context, "YearlyPlatninumMembership", "platinum_annual_membership_description", MembershipOption.ESubscriptionType.AnnualPlatinum, 2700, 108);

            context.SaveChanges();
        }

        private static void AddMembershipOption(DbContext context, string name, string details, MembershipOption.ESubscriptionType type, double price, int numberOfConsultations)
        {
            if (!context.Set<MembershipOption>().Any(a => a.Name == name))
            {
                context.Set<MembershipOption>().AddOrUpdate(new MembershipOption
                {
                    Name = name,
                    SubscriptionDetails = details,
                    SubscriptionType = type,
                    Price = price,
                    NumberOfConsultations = numberOfConsultations,
                    IsSystemStandard = true
                });
            }
        }
    }
}

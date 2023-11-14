using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;

namespace K9.WebApplication.Services
{
    public class GenoTypeScore : IScore
    {
        public int GetScore(HealthQuestionnaire hq, EGenoType genoType, GenoTypeBase item)
        {
            if (item.Hunter)
            {
                if (genoType == EGenoType.Hunter)
                {
                    return 5;
                }
            }
            if (item.Gatherer)
            {
                if (genoType == EGenoType.Gatherer)
                {
                    return 5;
                }
            }
            if (item.Teacher)
            {
                if (genoType == EGenoType.Teacher)
                {
                    return 5;
                }
            }
            if (item.Explorer)
            {
                if (genoType == EGenoType.Explorer)
                {
                    return 5;
                }
            }
            if (item.Warrior)
            {
                if (genoType == EGenoType.Warrior)
                {
                    return 5;
                }
            }
            if (item.Nomad)
            {
                if (genoType == EGenoType.Nomad)
                {
                    return 5;
                }
            }

            return 0;
        }
    }
}
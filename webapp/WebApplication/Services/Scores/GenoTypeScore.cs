using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;

namespace K9.WebApplication.Services
{
    public class GenoTypeScore : IScore
    {
        public int GetScore(HealthQuestionnaire hq, EGenoType genoType, GenoTypeBase item)
        {
            var score = 0;

            if (item.Hunter && genoType == EGenoType.Hunter)
            {
                score += 7;
            }
            if (item.Gatherer && genoType == EGenoType.Gatherer)
            {
                score += 7;
            }
            if (item.Teacher && genoType == EGenoType.Teacher)
            {
                score += 7;
            }
            if (item.Explorer && genoType == EGenoType.Explorer)
            {
                score += 7;
            }
            if (item.Warrior && genoType == EGenoType.Warrior)
            {
                score += 7;
            }
            if (item.Nomad && genoType == EGenoType.Nomad)
            {
                score += 7;
            }

            return score;
        }
    }
}
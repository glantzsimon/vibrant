using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;

namespace K9.WebApplication.Services
{
    public class FiveElementScore : IScore
    {
        public int GetScore(HealthQuestionnaire hq, EGenoType genoType, GenoTypeBase item)
        {
            var score = 0;
            var nineStarKi = hq.GetNineStarKiModel();

            if (item.Water && nineStarKi.MainEnergy == ENineStarKiEnergy.Water)
            {
                score += 7;
            }
            if (item.Tree && nineStarKi.MainEnergy == ENineStarKiEnergy.Thunder || 
                             nineStarKi.MainEnergy == ENineStarKiEnergy.Wind)
            {
                score += 7;
            }
            if (item.Earth && nineStarKi.MainEnergy == ENineStarKiEnergy.Soil || 
                              nineStarKi.MainEnergy == ENineStarKiEnergy.CoreEarth ||
                              nineStarKi.MainEnergy == ENineStarKiEnergy.Mountain)
            {
                score += 7;
            }
            if (item.Metal && nineStarKi.MainEnergy == ENineStarKiEnergy.Heaven ||
                              nineStarKi.MainEnergy == ENineStarKiEnergy.Lake)
            {
                score += 7;
            }
            if (item.Fire && nineStarKi.MainEnergy == ENineStarKiEnergy.Fire)
            {
                score += 7;
            }

            return score;
        }
    }
}
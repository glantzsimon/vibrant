using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;

namespace K9.WebApplication.Services
{
    public class DoshasScore : IScore
    {
        public int GetScore(HealthQuestionnaire hq, EGenoType genoType, GenoTypeBase item)
        {
            var score = 0;
            var dosha = hq.GetDoshas().GetDosha();
            
            if (item.VataDosha && dosha == EDosha.Vata || dosha == EDosha.KaphaVata || dosha == EDosha.VataPitta )
            {
                score += 7;
            }

            if (item.PittaDosha && dosha == EDosha.Pitta || dosha == EDosha.PittaKapha || dosha == EDosha.VataPitta )
            {
                score += 7;
            }

            if (item.KaphaDosha && dosha == EDosha.Kapha || dosha == EDosha.KaphaVata || dosha == EDosha.PittaKapha )
            {
                score += 7;
            }
            
            return score;
        }
    }
}
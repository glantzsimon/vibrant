using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;

namespace K9.WebApplication.Services
{
    public class DoshasScore : IScore
    {
        public int GetScore(HealthQuestionnaire hq, EGenoType genoType, GenoTypeBase item)
        {
            var score = 0;
            var threshold = 22;
            var prakrutiDoshas = hq.GetPrakrutiDoshas();
            var prakruti = prakrutiDoshas.GetDosha();
            var vikruti = hq.GetVikrutiDoshas();

            if (item.VataDosha)
            {
                if (prakruti == EDosha.Vata || prakruti == EDosha.KaphaVata || prakruti == EDosha.VataPitta)
                {
                    score += 7;
                }

                if (vikruti.VataDoshaScore - prakrutiDoshas.VataDoshaScore > threshold)
                {
                    score += 7;
                }
            }

            if (item.PittaDosha)
            {
                if (prakruti == EDosha.Pitta || prakruti == EDosha.PittaKapha || prakruti == EDosha.VataPitta)
                {
                    score += 7;
                }

                if (vikruti.PittaDoshaScore - prakrutiDoshas.PittaDoshaScore > threshold)
                {
                    score += 7;
                }
            }

            if (item.KaphaDosha)
            {
                if (prakruti == EDosha.Kapha || prakruti == EDosha.KaphaVata || prakruti == EDosha.PittaKapha)
                {
                    score += 7;
                }
                
                if (vikruti.KaphaDoshaScore - prakrutiDoshas.KaphaDoshaScore > threshold)
                {
                    score += 7;
                }
            }

            return score;
        }
    }
}
using K9.DataAccessLayer.Enums;
using K9.DataAccessLayer.Models;

namespace K9.WebApplication.Services
{
    public class CellularHealthScore : IScore
    {
        public int GetScore(HealthQuestionnaire hq, EGenoType genoType, GenoTypeBase item)
        {
            var score = 0;

            if (hq.AmmoniaSmell == EYesNo.Yes)
            {
                score++;
            }
            if (hq.Autoimmunity == EYesNo.Yes)
            {
                score++;
            }
            if (hq.EasilyOutOfBreath == EYesNo.Yes)
            {
                score++;
            }
            if (hq.Palpitations == EYesNo.Yes)
            {
                score++;
            }
            if (hq.BrainFog == EYesNo.Yes)
            {
                score++;
            }
            if (hq.FamilyHistoryOfAutoimmuneDisease == EYesNo.Yes)
            {
                score++;
            }
            if (hq.FamilyHistoryOfHeartDiseaseStrokeOrDiabetes == EYesNo.Yes)
            {
                score++;
            }
            if (hq.FamilyHistoryOfCancer == EYesNo.Yes)
            {
                score++;
            }
            if (hq.FamilyHistoryOfNeurologicalDisease == EYesNo.Yes)
            {
                score++;
            }
            if (hq.HistoryOfchronicFatigueOrFibromyalgia == EYesNo.Yes)
            {
                score++;
            }
            if (hq.LowMorningEnergy == EYesNo.Yes)
            {
                score++;
            }
            if (hq.MemoryProblems == EYesNo.Yes)
            {
                score++;
            }
            if (hq.ChestPain == EYesNo.Yes)
            {
                score++;
            }
            
            return score;
        }
    }
}
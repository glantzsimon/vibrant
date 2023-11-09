using K9.DataAccessLayer.Enums;

namespace K9.DataAccessLayer.Models
{
    public class GenoTypeStrengthTestResult
    {
        public EGenoType GenoType { get; set; }
        public int Count { get; set; }

        public EStrength GetStrength()
        {
            if (Count > 20)
            {
                return EStrength.VeryStrong;
            }

            if (Count >= 11)
            {
                return EStrength.Strong;
            }

            if (Count >= 5)
            {
                return EStrength.Positive;
            }

            return EStrength.Negative;
        }
        
    }
}
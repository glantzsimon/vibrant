using System.ComponentModel.DataAnnotations;
using K9.DataAccessLayer.Enums;
using K9.Globalisation;

namespace K9.DataAccessLayer.Models
{
    public class GenoTypeStrengthTestResult
    {
        [UIHint("BloodGroup")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.GenoTypeLabel)]
        public EGenoType GenoType { get; set; }

        public int Count { get; set; }

        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.StrengthLabel)]
        public EStrength Strength => GetStrength();

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
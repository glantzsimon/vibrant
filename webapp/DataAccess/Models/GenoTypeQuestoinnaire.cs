using K9.Base.DataAccessLayer.Attributes;
using K9.Base.DataAccessLayer.Models;
using K9.Globalisation;
using System.ComponentModel.DataAnnotations;
using K9.DataAccessLayer.Enums;

namespace K9.DataAccessLayer.Models
{
    [AutoGenerateName]
    [Name(ResourceType = typeof(Dictionary), ListName = Strings.Names.GenoTypeQuestionnaires, PluralName = Strings.Names.GenoTypeQuestionnaires, Name = Strings.Names.GenoTypeQuestionnaire)]
    public class GenoTypeQuestoinnaire : ObjectBase
    {

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.TorsoLengthLabel)]
        [Required]
        public int TorsoLength { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftLegLengthLabel)]
        [Required]
        public int LegLengthLeft { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightLegLengthLabel)]
        [Required]
        public int LegLengthRight { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftIndexFingerLengthLabel)]
        [Required]
        public int IndexFingerLengthLeft { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightIndexFingerLengthLabel)]
        [Required]
        public int IndexFingerLengthRight { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.LeftRingFingerLengthLabel)]
        [Required]
        public int RingFingerLengthLeft { get; set; }

        [UIHint("Measurement")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightRingFingerLengthLabel)]
        [Required]
        public int RingFingerLengthRight { get; set; }

        [UIHint("BloodGroup")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightRingFingerLengthLabel)]
        [Required]
        public EBloodGroup BloodGroup { get; set; }

        [UIHint("RhesusFactor")]
        [Display(ResourceType = typeof(Dictionary), Name = Strings.Labels.RightRingFingerLengthLabel)]
        [Required]
        public ERhesusFactor RhesusFactor { get; set; }
       
    }
}

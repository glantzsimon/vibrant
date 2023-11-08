using K9.DataAccessLayer.Enums;

namespace K9.DataAccessLayer.Models
{
    public class NineStarKiModel 
    {
        public ENineStarKiEnergy MainEnergy { get; set; }
        public ENineStarKiEnergy CharacterEnergy { get; set; }
        public ENineStarKiEnergy SurfaceEnergy { get; set; }
        public ENineStarKiEnergy YearlyCycleEnergy { get; set; }
        public ENineStarKiEnergy MonthlyCycleEnergy { get; set; }
    }
}

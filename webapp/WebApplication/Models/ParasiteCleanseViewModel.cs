using K9.DataAccessLayer.Models;

namespace K9.WebApplication.Models
{
    public class ParasiteCleanseViewModel
    {
        public ProtocolProduct ParasiteCleanseProduct { get; set; }
        public ProtocolProduct ParasiteCleanse2Product { get; set; }
        public ProtocolProduct ParasiteCleanse3Product { get; set; }
        public int[] ParasiteCleanseDosages { get; set; }
        public int[] ParasiteCleanse2Dosages { get; set; }
        public int[] ParasiteCleanse3Dosages { get; set; }
        public int NumberOfDays { get; set; }
    }
}
using K9.DataAccessLayer.Enums;

namespace K9.DataAccessLayer.Models
{
    public class Doshas
    {
        private const double triDoshaBoundary = 0.31;
        private const double dualBoundary = 0.42;
        private const double singleBoundary = 0.54;

        public double VataDoshaScore { get; set; }
        public double PittaDoshaScore { get; set; }
        public double KaphaDoshaScore { get; set; }

        public int GetVataDoshaPercentage() => (int)(VataDoshaScore * 100);
        public int GetPittaDoshaPercentage() => (int)(PittaDoshaScore * 100);
        public int GetKaphaDoshaPercentage() => (int)(KaphaDoshaScore * 100);

        public EDosha GetDosha()
        {
            if (VataDoshaScore >= triDoshaBoundary && PittaDoshaScore >= triDoshaBoundary && KaphaDoshaScore >= triDoshaBoundary)
            {
                return EDosha.Tridoshic;
            }
            if (KaphaDoshaScore >= dualBoundary && PittaDoshaScore >= dualBoundary)
            {
                return EDosha.PittaKapha;
            }
            if (KaphaDoshaScore >= dualBoundary && VataDoshaScore >= dualBoundary)
            {
                return EDosha.KaphaVata;
            }

            if (VataDoshaScore > singleBoundary)
            {
                return EDosha.Vata;
            }
            if (PittaDoshaScore > singleBoundary)
            {
                return EDosha.Pitta;
            }
            if (KaphaDoshaScore > singleBoundary)
            {
                return EDosha.Kapha;
            }

            return EDosha.Tridoshic;
        }
    }
}

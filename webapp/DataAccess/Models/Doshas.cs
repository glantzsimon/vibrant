namespace K9.DataAccessLayer.Models
{
    public class Doshas
    {
        public double VataDoshaScore { get; set; }
        public double PittaDoshaScore { get; set; }
        public double KaphaDoshaScore { get; set; }

        public int GetVataDoshaPercentage() => (int)(VataDoshaScore * 100);
        public int GetPittaDoshaPercentage() => (int)(PittaDoshaScore * 100);
        public int GetKaphaDoshaPercentage() => (int)(KaphaDoshaScore * 100);
    }
}

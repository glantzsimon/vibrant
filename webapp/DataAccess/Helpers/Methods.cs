using System;

namespace K9.DataAccessLayer.Helpers
{
    public static class Methods
    {
        public static readonly Random RandomGenerator = new Random();

        public static double RoundToInteger(double value, int roundValue)
        {
            return Math.Round(value / roundValue, 0) * roundValue;
        }
    }
}

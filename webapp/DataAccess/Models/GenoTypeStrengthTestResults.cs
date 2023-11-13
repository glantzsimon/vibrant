using System;
using System.Collections.Generic;
using System.Linq;

namespace K9.DataAccessLayer.Models
{
    public class GenoTypeStrengthTestResults
    {
        public List<GenoTypeStrengthTestResult> Results { get; set; }

        public double GetMaxValue() => Results.Max(e => e.Max);

        public double GetResultAsPercentage(GenoTypeStrengthTestResult result)
        {
            return Math.Ceiling((result.Count / GetMaxValue()) * 100);
        }

        public double GetGaugaeValue(GenoTypeStrengthTestResult result)
        {
            return Math.Ceiling(100 / GetMaxValue() * result.Count);
        }

    }
}
using HtmlAgilityPack;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace K9.DataAccessLayer.Helpers
{
    public static class Methods
    {
        public static readonly Random RandomGenerator = new Random();

        public static double RoundToInteger(double value, int roundValue)
        {
            return Math.Round(value / roundValue, 0) * roundValue;
        }

        public static string HtmlToText(this string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc.DocumentNode.InnerText;
        }

        public static string RemoveEmptyLines(this string value)
        {
            return string.Join(Environment.NewLine, Regex.Split(value, Environment.NewLine).Select(e => e.Trim()).Where(e => !string.IsNullOrEmpty(e) && !string.IsNullOrWhiteSpace(e)));
        }
    }
}

using HtmlAgilityPack;
using K9.SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using K9.DataAccessLayer.Constants;
using K9.Globalisation;

namespace K9.DataAccessLayer.Helpers
{
    public static class Methods
    {
        public static readonly Random RandomGenerator = new Random();

        public static int InvertScore(this int score)
        {
            return 100 - score;
        }

        public static double ToInternationalPrice(this double value)
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName != Strings.LanguageCodes.Thai)
            {
                return Math.Round(value * ConversionConstants.BahtToDollarsRate / 3, MidpointRounding.AwayFromZero) * 3;
            }

            return value;
        }

        public static double ToBritishPounds(this double value)
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName != Strings.LanguageCodes.Thai)
            {
                return Math.Round(value * ConversionConstants.BahtToBritishPoundsRate / 3, MidpointRounding.AwayFromZero) * 3;
            }

            return value;
        }

        public static double RoundToInteger(double value, int roundValue)
        {
            return Math.Round(value / roundValue, 0) * roundValue;
        }

        public static string HtmlToText(this string html)
        {
            if (string.IsNullOrEmpty(html))
            {
                return string.Empty;
            }
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc.DocumentNode.InnerText;
        }

        public static string ToDisplayList(this string[] items)
        {
            return items == null ? string.Empty : string.Join(Environment.NewLine, items);
        }

        public static string ToDisplayList(this IEnumerable<string> items)
        {
            return items.ToArray().ToDisplayList();
        }

        public static string RemoveEmptyLines(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return string.Join(Environment.NewLine, Regex.Split(value, Environment.NewLine).Select(e => e.Trim()).Where(e => !string.IsNullOrEmpty(e) && !string.IsNullOrWhiteSpace(e)));
            }

            return string.Empty;
        }

        public static string SelectLines(this string value, int numberOfLines)
        {
            return string.Join(Environment.NewLine, Regex.Split(value, Environment.NewLine).Select(e => e.Trim()).Where(e => !string.IsNullOrEmpty(e) && !string.IsNullOrWhiteSpace(e)).Take(numberOfLines));
        }

        public static string ToCurrency(this double value, string currencyFormat = "฿")
        {
            return $"{currencyFormat}{value:n0}";
        }

        public static string Pluralise(this string value)
        {
            return $"{value}s";
        }

        public static double GetSuggestedBulkDiscount(this double price)
        {
            return price <= 11110 ? 0 : (1 - (11110 / price)) * 0.11;
        }

        public static double GetSuggestedBulkDiscountPrice(this double price)
        {
            return price - (price * price.GetSuggestedBulkDiscount());
        }

        public static string GetBookMark(this IObjectBase model)
        {
            return $"{model.GetType().Name}-{model.Id}";
        }

        public static string ToDelimitedList(this object[] parameters)
        {
            return parameters != null && parameters.Any() ? string.Join("-", parameters) : string.Empty;
        }
    }
}

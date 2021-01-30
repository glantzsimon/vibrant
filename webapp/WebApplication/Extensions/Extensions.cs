﻿using K9.DataAccessLayer.Models;
using K9.WebApplication.Controllers;
using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace K9.WebApplication.Extensions
{
    public static partial class Extensions
    {
        public static string ToSeoFriendlyString(this string value)
        {
            var regex = new Regex("[^a-zA-Z0-9 -]");
            var alphaNumericString = regex.Replace(value, "");

            return string.Join("-", alphaNumericString.ToLower().Split(' '));
        }

        public static string ToPreviewText(this string value, int length = 100)
        {
            var valueLength = value.Length;
            var canBeAbbreviated = valueLength > length;
            var substring = value.Substring(0, canBeAbbreviated ? length : valueLength);
            var abbrevationSuffix = canBeAbbreviated ? "..." : string.Empty;
            return $"{substring}{abbrevationSuffix}";
        }

        public static UserMembership GetActiveUserMembership(this WebViewPage view)
        {
            try
            {
                var baseController = view.ViewContext.Controller as BaseVibrantController;
                return baseController?.GetActiveUserMembership();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}

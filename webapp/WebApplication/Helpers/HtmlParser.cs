﻿using K9.SharedLibrary.Extensions;
using K9.SharedLibrary.Models;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace K9.WebApplication.Helpers
{
    public static class HtmlParser
    {
        public static void ParseHtml<T>(ref T model)
        {
            foreach (var propertyInfo in model.GetProperties())
            {
                if (propertyInfo.GetAttribute<AllowHtmlAttribute>() != null)
                {
                    var value = model.GetProperty(propertyInfo);
                    if (value != null)
                    {
                        model.SetProperty(propertyInfo, ParseHtml(value.ToString()));
                    }
                }
            }
        }

        private static string ParseHtml(string value)
        {
            var sb = new StringBuilder();

            using (StringReader sr = new StringReader(value)) {
                var line = "";

                while ((line = sr.ReadLine()) != null) {
                    if (line.Contains("{"))
                    {
                        var html = line.Replace("{", "<");
                        html = html.Replace("}", ">");
                        sb.AppendLine(html);
                    }
                    else if (line.Contains("<"))
                    {
                        var html = line.Replace("<", "{");
                        html = html.Replace(">", "}");
                        sb.AppendLine(html);
                    }
                    else
                    {
                        sb.AppendLine(line);
                    }
                }
            }

            return sb.ToString();
        }
    }
}
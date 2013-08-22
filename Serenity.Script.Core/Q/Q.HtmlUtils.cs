﻿using jQueryApi;
using System.Html;
using System.Runtime.CompilerServices;
using Regex = System.Text.RegularExpressions.Regex;

namespace Serenity
{
    public static partial class Q
    {
        /// <summary>
        /// Html encodes a string
        /// </summary>
        /// <param name="value">string</param>
        /// <returns>encoded string</returns>
        public static string HtmlEncode(object value)
        {
            string text = value == null ? "" : value.ToString();

            if (new Regex("[><&]", "g").Test(text))
                return text.Replace(new Regex("[><&]", "g"), HtmlEncodeReplacer);

            return text;
        }

        /// <summary>
        /// Creates a new div and appends it to document body
        /// </summary>
        /// <returns>Newly created div</returns>
        public static jQueryObject NewBodyDiv()
        {
            return jQuery.FromHtml("<div/>").AppendTo(Document.Body);
        }

        private static string HtmlEncodeReplacer(string a)
        {
            switch (a)
            {
                case "&": return "&amp;";
                case ">": return "&gt;";
                case "<": return "&lt;";
            }
            return a;
        }

        public static void ClearOptions(jQueryObject select)
        {
            select.Html("");
        }

        public static void AddEmptyOption(jQueryObject select)
        {
            AddOption(select, "", "--seçiniz--");
        }

        public static void AddOption(jQueryObject select, string key, string text)
        {
            jQuery.FromHtml("<option/>")
                .Value(key)
                .Text(text)
                .AppendTo(select);
        }

        public static jQueryObject FindElementWithRelativeId(jQueryObject element, string relativeId)
        {
            var elementId = element.GetAttribute("id");

            if (elementId.IsEmptyOrNull())
                return jQuery.Select("#" + relativeId);

            var result = jQuery.Select(elementId + relativeId);
            if (result.Length > 0)
                return result;

            result = jQuery.Select(elementId + "_" + relativeId);
            if (result.Length > 0)
                return result;

            while (true)
            {
                var idx = elementId.LastIndexOf('_');
                if (idx <= 0)
                    return jQuery.Select("#" + relativeId);

                elementId = elementId.Substring(0, idx);

                result = jQuery.Select("#" + elementId + "_" + relativeId);
                if (result.Length > 0)
                    return result;
            }
        }
    }
}
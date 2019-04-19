using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Framework.Core.Helpers
{
    public static class StringExtensions
    {
        /*
        public static string Trim(this string @string, params string[] trimStrings)
        {
            return Trim(@string, StringComparison.CurrentCulture, trimStrings);
        }
        */

        /*
        public static string Trim(this string @string, StringComparison comparisonType, params string[] trimStrings)
        {
            //Throw.IfArgumentNull(@string, "string");
            var result = @string;
            result = result.TrimStart(comparisonType, trimStrings);
            result = result.TrimEnd(comparisonType, trimStrings);
            return result;
        }

        public static string TrimEnd(this string @string, params string[] trimStrings)
        {
            return TrimEnd(@string, StringComparison.CurrentCulture, trimStrings);
        }
        */

        /*
        public static string TrimEnd(this string @string, StringComparison comparisonType, params string[] trimStrings)
        {
            Throw.IfArgumentNull(@string, "string");
            if (CollectionHelpers.IsNullOrEmpty(trimStrings))
            {
                return @string.TrimEnd();
            }

            var str = "";
            var result = @string;

            for (int i = @string.Length - 1; i > 0; i--)
            {
                str = @string[i] + str;
                if (trimStrings.Any(s => s.Equals(str, comparisonType)))
                {
                    result = @string.Substring(0, i);
                    str = "";
                }
            }

            return result;
        }
        */

        /*
        public static string TrimStart(this string @string, params string[] trimStrings)
        {
            return TrimStart(@string, StringComparison.CurrentCulture, trimStrings);
        }
        */
        
        /*
        public static string TrimStart(this string @string, StringComparison comparisonType, params string[] trimStrings)
        {
            Throw.IfArgumentNull(@string, "string");
            if (CollectionHelpers.IsNullOrEmpty(trimStrings))
            {
                return @string.TrimStart();
            }

            var str = "";
            var result = @string;

            for (int i = 0; i < @string.Length; i++)
            {
                str = str + @string[i];
                if (trimStrings.Any(s => s.Equals(str, comparisonType)))
                {
                    result = @string.Substring(i + 1);
                    str = "";
                }
            }

            return result;
        }
        */
        
        public static bool EnclosedBy(this string @string, string start, string end)
        {
            //Throw.IfArgumentNull(@string, "string");
            return @string.StartsWith(start) && @string.EndsWith(end);
        }

        public static bool EqualsIgnoreCase(this string @string, string value)
        {
            return String.Equals(@string, @value, StringComparison.OrdinalIgnoreCase);
        }

        public static string FormatWith(this string @string, params object[] args)
        {
            return String.Format(@string, args);
        }

        public static string UnformatWith(this string @string, params string[] placeHolders)
        {
            var escapedString = @string.Replace("{", "{{").Replace("}", "}}");
            for (int i = 0; i < placeHolders.Length; i++)
            {
                escapedString = escapedString.Replace(placeHolders[i], "{" + i + "}");
            }
            return escapedString;
        }

        public static byte[] ToByteArray(this string hex)
        {
            return Enumerable.Range(0, hex.Length).
                Where(x => 0 == x % 2).
                Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).
                ToArray();
        }

        public static string ToHexString(this byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        public static string Ellipsize(this string text, int characterCount)
        {
            //return text.Ellipsize(characterCount, "&#160;&#8230;");
            return text.Ellipsize(characterCount, "...");
        }

        public static string Ellipsize(this string text, int characterCount, string ellipsis)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";

            if (characterCount < 0 || text.Length <= characterCount)
                return text;

            return Regex.Replace(text.Substring(0, characterCount + 1), @"\s+\S*$", "") + ellipsis;
        }
    }
}

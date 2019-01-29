
using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace NeoGemino {
    public static class Utilities {
        public static string TrimLineStart(string text) {
            var whitespaceLines =  Regex.Matches(text, @"^\s+", RegexOptions.Multiline);
            var minimumWhitespaceCount = whitespaceLines.Select(x => x.Length).Min();
            var prefix = string.Join("", Enumerable.Repeat(" ", minimumWhitespaceCount).ToArray());

            var result = Regex.Replace(text, "^" + prefix, "", RegexOptions.Multiline);
            
            return result;
        }
    }
}
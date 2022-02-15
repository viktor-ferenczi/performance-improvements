using System.Linq;
using System.Text.RegularExpressions;

namespace Shared.Extensions
{
    // ReSharper disable once UnusedType.Global
    public static class StringExtensions
    {
        // Copied from Humanizer.Core to avoid the dependency.
        // We cannot use NuGet packages due to local compilation by Plugin Loader.
        // See the original at: https://github.com/Humanizr/Humanizer

        private static readonly Regex Pascalizer = new Regex("(?:^|_| +)(.)", RegexOptions.Compiled);
        private static readonly Regex PascalCaseWordPartsRegex = new Regex(@"[\p{Lu}]?[\p{Ll}]+|[0-9]+[\p{Ll}]*|[\p{Lu}]+(?=[\p{Lu}][\p{Ll}]|[0-9]|\b)|[\p{Lo}]+", RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        private static readonly Regex FreestandingSpacingCharRegex = new Regex(@"\s[-_]|[-_]\s", RegexOptions.Compiled);

        /// <summary>
        /// Dehumanizes a string; e.g. 'some string', 'Some String', 'Some string' -> 'SomeString'
        /// If a string is already dehumanized then it leaves it alone 'SomeStringAndAnotherString' -> 'SomeStringAndAnotherString'
        /// </summary>
        /// <param name="input">The string to be dehumanized</param>
        /// <returns></returns>
        public static string Dehumanize(this string input)
        {
            var pascalizedWords = input.Split(' ').Select(word => word.Pascalize());
            return string.Join("", pascalizedWords).Replace(" ", "");
        }

        /// <summary>
        /// By default, pascalize converts strings to UpperCamelCase also removing underscores
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Pascalize(this string input)
        {
            return Pascalizer.Replace(input, match => match.Groups[1].Value.ToUpper());
        }

        private static string FromUnderscoreDashSeparatedWords(string input)
        {
            return string.Join(" ", input.Split(new[] { '_', '-' }));
        }

        private static string FromPascalCase(string input)
        {
            var result = string.Join(" ", PascalCaseWordPartsRegex
                .Matches(input).Cast<Match>()
                .Select(match => match.Value.ToCharArray().All(char.IsUpper) &&
                                 (match.Value.Length > 1 || (match.Index > 0 && input[match.Index - 1] == ' ') || match.Value == "I")
                    ? match.Value
                    : match.Value.ToLower()));

            if (result.Replace(" ", "").ToCharArray().All(c => char.IsUpper(c)) &&
                result.Contains(" "))
            {
                result = result.ToLower();
            }

            return result.Length > 0
                ? char.ToUpper(result[0]) +
                  result.Substring(1, result.Length - 1)
                : result;
        }

        /// <summary>
        /// Humanizes the input string; e.g. Underscored_input_String_is_turned_INTO_sentence -> 'Underscored input String is turned INTO sentence'
        /// </summary>
        /// <param name="input">The string to be humanized</param>
        /// <returns></returns>
        public static string Humanize(this string input)
        {
            // if input is all capitals (e.g. an acronym) then return it without change
            if (input.ToCharArray().All(char.IsUpper))
            {
                return input;
            }

            // if input contains a dash or underscore which preceeds or follows a space (or both, e.g. free-standing)
            // remove the dash/underscore and run it through FromPascalCase
            if (FreestandingSpacingCharRegex.IsMatch(input))
            {
                return FromPascalCase(FromUnderscoreDashSeparatedWords(input));
            }

            if (input.Contains("_") || input.Contains("-"))
            {
                return FromUnderscoreDashSeparatedWords(input);
            }

            return FromPascalCase(input);
        }
    }
}
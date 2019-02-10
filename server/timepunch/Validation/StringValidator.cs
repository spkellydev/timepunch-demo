using System.Text.RegularExpressions;

namespace timepunch.Validation
{
    public static class StringValidator
    {
        public static bool HasNumber(string input) => Validate(input, @"[0-9]+");
        public static bool HasUppercase(string input) => Validate(input, @"[A-Z]+");
        public static bool HasLowercase(string input) => Validate(input, @"[a-z]+");
        public static bool HasSymbols(string input) => Validate(input, @"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        private static bool Validate(string input, string pattern) => new Regex(pattern).IsMatch(input);
    }
}
using System.Text.RegularExpressions;

namespace APPZ_lab1_v6.Helpers
{
    public static class ValidationRegex
    {
        public static readonly Regex NamePattern = new Regex(@"^[\p{L}'-]{2,}$", RegexOptions.Compiled);
        public static readonly Regex AgePattern = new Regex(@"^([1-9]|1[0-9]|2[0-5])$", RegexOptions.Compiled);
        public static readonly Regex FloatPattern = new Regex(@"^[0-9]+(\,[0-9]+)?$", RegexOptions.Compiled);
        public static readonly Regex ColorPattern = new Regex(@"^[\p{L}'-]{2,}$", RegexOptions.Compiled);
        public static readonly Regex MenuChoicePattern = new Regex(@"^[0-9]$", RegexOptions.Compiled);
    }
} 
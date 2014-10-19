namespace Medlars.Core
{
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        private static readonly Regex EmailRegex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");

        public static bool IsNullOrInvalidEmail(this string s)
        {
            return string.IsNullOrWhiteSpace(s) || !EmailRegex.IsMatch(s);
        }
    }
}

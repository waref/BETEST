using System.Text.RegularExpressions;


namespace ServiceLayer.Statics
{
    public static class StringHelper
    {
        public static bool IsRegexValid(string row, string regex)
        {
            return !string.IsNullOrEmpty(row) &&
                !string.IsNullOrEmpty(regex)
                && new Regex(regex).IsMatch(row);
        }
    }
}

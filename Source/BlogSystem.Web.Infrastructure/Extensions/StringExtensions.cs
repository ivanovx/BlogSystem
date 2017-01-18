using System;

namespace BlogSystem.Web.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToUrl(this string str, int id, string title, DateTime createdOn)
        {
            return string.Empty;
        }

        public static string GenerateUrl(this string str)
        {
            return str;
        }
    }
}
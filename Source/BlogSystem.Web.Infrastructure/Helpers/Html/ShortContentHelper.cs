namespace BlogSystem.Web.Infrastructure.Helpers.Html
{
    using System.Web.Mvc;
    using AngleSharp.Parser.Html;

    public static class ShortContentHelper
    {
        public static string Truncate(this HtmlHelper helper, string text)
        {
            var parser = new HtmlParser();
            var document = parser.Parse(text);
            var p = document.QuerySelector("p");

            if (p != null)
            {
                return $"{p.InnerHtml}...";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
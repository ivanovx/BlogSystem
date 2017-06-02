namespace BlogSystem.Web.Infrastructure.Helpers.Html
{
    using System.Web;
    using System.Web.Mvc;
    using AngleSharp.Parser.Html;

    public static class ShortContentHelper
    {
        public static IHtmlString Truncate(this HtmlHelper helper, string text)
        {
            var htmlParser = new HtmlParser();
            var document = htmlParser.Parse(text);
            var p = document.QuerySelector("p");

            if (p == null)
            {
                return helper.Raw(string.Empty);
            }

            return helper.Raw(p.InnerHtml);
        }
    }
}
namespace BlogSystem.Web.Infrastructure.HtmlHelpers
{
    using System.Web.Mvc;
    using Extensions;

    public static class HtmlTextTruncateHelper
    {
        public static string TruncateHtmlContent(this HtmlHelper helper, string text)
        {
            return new HtmlTextTruncate().TruncateHtmlContent(text, 1000);
        }
    }
}
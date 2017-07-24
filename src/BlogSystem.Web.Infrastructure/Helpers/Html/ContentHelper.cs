namespace BlogSystem.Web.Infrastructure.Helpers.Html
{
    using System.Web;
    using System.Web.Mvc;
    using AngleSharp.Parser.Html;

    public static class ContentHelper
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

        /*
         * 
         * TODO: Implements this
         * */
        public static IHtmlString Content(this HtmlHelper helper, string text)
        {
            var htmlParser = new HtmlParser();
            var document = htmlParser.Parse(text);

            var images = document.GetElementsByTagName("img");

            foreach (var image in images)
            {
                image.ClassName = "img-fluid";
            }
            
            var html = document.Body.InnerHtml;

            return helper.Raw(html);
        }
    }
}
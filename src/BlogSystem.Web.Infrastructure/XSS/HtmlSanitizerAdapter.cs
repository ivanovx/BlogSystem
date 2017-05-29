namespace BlogSystem.Web.Infrastructure.XSS
{
    using Ganss.XSS;

    public class HtmlSanitizerAdapter : ISanitizer
    {
        public string Sanitize(string html)
        {
            var sanitizer = new HtmlSanitizer();

            sanitizer.AllowedTags.Add("iframe");

            return sanitizer.Sanitize(html);
        }
    }
}
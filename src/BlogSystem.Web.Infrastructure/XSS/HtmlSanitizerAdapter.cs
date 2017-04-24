namespace BlogSystem.Web.Infrastructure.XSS
{
    using Ganss.XSS;

    public class HtmlSanitizerAdapter : ISanitizer
    {
        public string Sanitize(string html)
        {
            var sanitizer = new HtmlSanitizer();

            return sanitizer.Sanitize(html);
        }
    }
}
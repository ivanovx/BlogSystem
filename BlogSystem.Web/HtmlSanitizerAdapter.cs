using Ganss.XSS;

namespace BlogSystem.Web
{
    public class HtmlSanitizerAdapter : ISanitizer
    {
        public string Sanitize(string html)
        {
            HtmlSanitizer sanitizer = new HtmlSanitizer();

            return sanitizer.Sanitize(html);
        }
    }
}

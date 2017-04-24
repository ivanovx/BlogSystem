namespace BlogSystem.Web.Infrastructure.XSS
{
    public interface ISanitizer
    {
        string Sanitize(string html);
    }
}
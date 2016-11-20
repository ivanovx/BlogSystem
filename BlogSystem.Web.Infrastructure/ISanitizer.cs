namespace BlogSystem.Web.Infrastructure
{
    public interface ISanitizer
    {
        string Sanitize(string html);
    }
}
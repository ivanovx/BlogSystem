namespace BlogSystem.Web
{
    public interface ISanitizer
    {
        string Sanitize(string html);
    }
}
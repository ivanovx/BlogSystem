namespace BlogSystem.Web.Infrastructure.Helpers
{
    using System;

    public interface IUrlGenerator
    {
        string ToUrl(int id, string title, DateTime createdOn);

        string GenerateUrl(string title);
    }
}
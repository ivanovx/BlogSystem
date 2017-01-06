namespace BlogSystem.Web.Infrastructure.Helpers
{
    using System;

    public interface IUrlGenerator
    {
        string GenerateUrl(string title);

        string GeneratePostUrl(int id, string title, DateTime createdOn);
    }
}
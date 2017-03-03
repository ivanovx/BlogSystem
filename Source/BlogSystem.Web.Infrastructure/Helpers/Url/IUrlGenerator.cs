namespace BlogSystem.Web.Infrastructure.Helpers.Url
{
    using System;

    public interface IUrlGenerator
    {
        string ToUrl(int id, string title, DateTime createdOn);

        string GenerateUrl(string title);
    }
}
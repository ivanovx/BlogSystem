namespace BlogSystem.Web.Infrastructure.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdministrationLogAttribute : Attribute
    {
    }
}

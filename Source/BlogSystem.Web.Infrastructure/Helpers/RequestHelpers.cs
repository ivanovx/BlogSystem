namespace BlogSystem.Web.Infrastructure.Helpers
{
    using System;
    using System.Net;
    using System.Web.Routing;

    public static class RequestHelpers
    {
        public static string GetClientIpAddress(RequestContext requestContext)
        {
            try
            {
                var userHostAddress = requestContext.HttpContext.Request.UserHostAddress;

                IPAddress.Parse(userHostAddress);

                var xForwardedFor =  requestContext.HttpContext.Request.ServerVariables["X_FORWARDED_FOR"];

                if (string.IsNullOrEmpty(xForwardedFor))
                {
                    return userHostAddress;
                }

                return xForwardedFor;
            }
            catch (Exception)
            {
                return "0.0.0.0";
            }
        }
    }
}

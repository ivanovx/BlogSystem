namespace BlogSystem.Web.Infrastructure.Helpers.Html
{
    using System;
    using System.Web.Mvc;

    public static class DateTimeHelper
    {
        private static string ToRelativeDate(DateTime dateTime)
        {
            TimeSpan timeSpan = DateTime.Now - dateTime;

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                return string.Format("{0} seconds ago", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                return timeSpan.Minutes > 1 ? String.Format("about {0} minutes ago", timeSpan.Minutes) : "about a minute ago";
            }
            else if(timeSpan <= TimeSpan.FromHours(24))
            {
                return timeSpan.Hours > 1 ? String.Format("about {0} hours ago", timeSpan.Hours) : "about an hour ago";
            }
            else if(timeSpan <= TimeSpan.FromDays(30))
            {
                return timeSpan.Days > 1 ? String.Format("about {0} days ago", timeSpan.Days) : "yesterday";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                return timeSpan.Days > 30 ? String.Format("about {0} months ago", timeSpan.Days / 30) : "about a month ago";
            }
            else
            {
                return timeSpan.Days > 365 ? String.Format("about {0} years ago", timeSpan.Days / 365) : "about a year ago";
            }
        }

        public static MvcHtmlString Timeago(this HtmlHelper helper, DateTime dateTime)
        {
            return MvcHtmlString.Create(ToRelativeDate(dateTime));
        }
    }
}
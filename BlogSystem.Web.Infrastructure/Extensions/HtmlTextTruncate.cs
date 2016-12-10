namespace BlogSystem.Web.Infrastructure.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class HtmlTextTruncate
    {
        public string TruncateHtmlContent(string text, int charCount)
        {
            bool inTag = false;
            int cntr = 0;
            int cntrContent = 0;

            // loop through html, counting only viewable content
            foreach (Char c in text)
            {
                if (cntrContent == charCount) break;
                cntr++;
                if (c == '<')
                {
                    inTag = true;
                    continue;
                }

                if (c == '>')
                {
                    inTag = false;
                    continue;
                }
                if (!inTag) cntrContent++;
            }

            string substr = text.Substring(0, cntr);

            //search for nonclosed tags        
            MatchCollection openedTags = new Regex("<[^/](.|\n)*?>").Matches(substr);
            MatchCollection closedTags = new Regex("<[/](.|\n)*?>").Matches(substr);

            // create stack          
            Stack<string> opentagsStack = new Stack<string>();
            Stack<string> closedtagsStack = new Stack<string>();

            // to be honest, this seemed like a good idea then I got lost along the way 
            // so logic is probably hanging by a thread!! 
            foreach (Match tag in openedTags)
            {
                string openedtag = tag.Value.Substring(1, tag.Value.Length - 2);
                // strip any attributes, sure we can use regex for this!
                if (openedtag.IndexOf(" ") >= 0)
                {
                    openedtag = openedtag.Substring(0, openedtag.IndexOf(" "));
                }

                // ignore brs as self-closed
                if (openedtag.Trim() != "br")
                {
                    opentagsStack.Push(openedtag);
                }
            }

            foreach (Match tag in closedTags)
            {
                string closedtag = tag.Value.Substring(2, tag.Value.Length - 3);
                closedtagsStack.Push(closedtag);
            }

            if (closedtagsStack.Count < opentagsStack.Count)
            {
                while (opentagsStack.Count > 0)
                {
                    string tagstr = opentagsStack.Pop();

                    if (closedtagsStack.Count == 0 || tagstr != closedtagsStack.Peek())
                    {
                        substr += "</" + tagstr + ">";
                    }
                    else
                    {
                        closedtagsStack.Pop();
                    }
                }
            }

            return substr;
        }
    }
}

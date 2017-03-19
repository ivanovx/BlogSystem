namespace BlogSystem.Web.Infrastructure.Helpers.Url
{
    using System;
    using System.Text;

    public class UrlGenerator : IUrlGenerator
    {
        public string ToUrl(int id, string title, DateTime createdOn) 
            => $"/Posts/{createdOn.Year:0000}/{createdOn.Month:00}/{this.GenerateUrl(title)}/{id}";

        public string GenerateUrl(string uglyString)
        {
            var resultString = new StringBuilder(uglyString.Length);
            var isLastCharacterDash = false;

            uglyString = uglyString.Replace("C#", "CSharp");
            uglyString = uglyString.Replace("F#", "FSharp");
            uglyString = uglyString.Replace("C++", "CPlusPlus");
            uglyString = uglyString.Replace("ASP.NET", "AspNet");
            uglyString = uglyString.Replace(".NET", "DotNet");

            foreach (var character in uglyString)
            {
                if (char.IsLetterOrDigit(character))
                {
                    resultString.Append(character);

                    isLastCharacterDash = false;
                }
                else if (!isLastCharacterDash)
                {
                    resultString.Append('-');

                    isLastCharacterDash = true;
                }
            }

            return resultString.ToString().Trim('-');
        }
    }
}
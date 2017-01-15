namespace BlogSystem.Web.Infrastructure.Helpers
{
    using System.Text;

    public class UrlGenerator : IUrlGenerator
    {
        public string GenerateUrl(string uglyString)
        {
            var resultString = new StringBuilder(uglyString.Length);
            var isLastCharacterDash = false;

            uglyString = uglyString.Replace("C#", "CSharp");
            uglyString = uglyString.Replace("F#", "FSharp");
            uglyString = uglyString.Replace("C++", "CPlusPlus");
            uglyString = uglyString.Replace("ASP.NET", "AspNet");
            uglyString = uglyString.Replace(".NET", "DotNet");

            foreach (char character in uglyString)
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
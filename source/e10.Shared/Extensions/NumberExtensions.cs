using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace e10.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string StripHTML(this string HTMLText, bool decode = true)
        {
            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            var stripped = reg.Replace(HTMLText, "");
            return decode ? HttpUtility.HtmlDecode(stripped) : stripped;
        }
    }
    public static class NumberExtensions
    {
        private static readonly char[] Jokers =
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V','W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v','w', 'x', 'y', 'z'
        };

        private static readonly Base10Converter Convertror = new Base10Converter(Jokers);


        public static long ToBase10(this string hexValue)
        {
            return Convertror.StringToBase10(hexValue);
        }

        public static string Base10ToString(this long number)
        {
            return Convertror.Base10ToString(number);
        }

        public static string Base10ToString(this int number)
        {
            return Convertror.Base10ToString(number);
        }

    }
}

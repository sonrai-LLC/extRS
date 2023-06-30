using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using System.Xml;

namespace Sonrai.ExtRS
{
    public class FormattingService
    {
        public static string ConvertInchesToFeetAndInches(int inches, bool forHtml = false)
        {
            var feet = inches / 12;
            inches %= 12;
            return feet.ToString() + (forHtml ? "&#39;" : "\'") + " " + inches + (forHtml ? " &#34;" : "\"");
        }

        public static string ToCurrencyUSD(decimal value)
        {
            return value.ToString("C");
        }

        public static decimal FromAbbreviatedValue(decimal value, string abbreviation)
        {
            switch (abbreviation)
            {
                case "K":
                    return value * 1000;
                case "M":
                    return value * 1000000;
                case "B":
                    return value * 1000000000;
                case "T":
                    return value * 1000000000000;
                default:
                    return value;
            }
        }

        public static decimal ToThousands(decimal value)
        {
            return Convert.ToDecimal(value) / 1000;
        }

        public static decimal ToMillions(decimal value)
        {
            return Convert.ToDecimal(value) / 1000000;
        }

        public static decimal ToBillions(decimal value)
        {
            return Convert.ToDecimal(value) / 1000000000;
        }

        public static decimal ToTrillions(decimal value)
        {
            return Convert.ToDecimal(value) / 1000000000000;
        }

        public static decimal FromThousands(decimal value)
        {
            return Convert.ToDecimal(value) * 1000;
        }

        public static decimal FromMillions(decimal value)
        {
            return Convert.ToDecimal(value) * 1000000;
        }

        public static decimal FromBillions(decimal value)
        {
            return Convert.ToDecimal(value) * 1000000000;
        }

        public static decimal FromTrillions(decimal value)
        {
            return Convert.ToDecimal(value) * 1000000000000;
        }

        public static string ConvertXmlToJson(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            return JsonConvert.SerializeXmlNode(doc);
        }

        public static string ConvertJsonToXml(string json)
        {
            return JsonConvert.DeserializeXNode(json, "Root").ToString();
        }

        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        #region ref
        // ref: https://stackoverflow.com/questions/10824165/converting-a-csv-file-to-json-using-c-sharp
        #endregion      
        public static string CsvToJson(string input, string delimiter)
        {
            using (TextFieldParser parser = new TextFieldParser(new MemoryStream(Encoding.UTF8.GetBytes(input))))
            {
                parser.Delimiters = new string[] { delimiter };
                string[] headers = parser.ReadFields();
                if (headers == null) return null;
                string[] row;
                string comma = "";
                var sb = new StringBuilder((int)(input.Length * 1.1));
                sb.Append('[');
                while ((row = parser.ReadFields()) != null)
                {
                    var dict = new Dictionary<string, object>();
                    for (int i = 0; row != null && i < row.Length; i++)
                        dict[headers[i]] = row[i];

                    var obj = JsonConvert.SerializeObject(dict);
                    sb.Append(comma + obj);
                    comma = ",";
                }

                return sb.Append(']').ToString();
            }
        }

        public static bool StringHasDupes(string value, bool caseSensitive)
        {
            if (caseSensitive)
                value = value.ToUpper();
            var valueLen = value.Length;
            var charLen = new List<char>(value.ToCharArray()).GroupBy(a => a).Count();

            return valueLen != charLen;
        }

        public static string Shrug = "¯\\_(ツ)_/¯";

        #region CommonRegex
        //ref: //https://digitalfortress.tech/tips/top-15-commonly-used-regex/ 

        public static string WholeNumbers = "^\\d+$";

        public static string DecimalNumbers = "^\\d*\\.\\d+$";

        public static string WholeAndDecimalNumbers = "^\\d*(\\.\\d+)?$";

        public static string Alphanumeric = "^[a-zA-Z][a-zA-Z0-9]*$";

        public static string AlphanumericWithSpaces = "^[a-zA-Z][a-zA-Z0-9 ]*$";

        // Should have 1 lowercase letter, 1 uppercase letter, 1 number, 1 special character and be at least 8 characters long
        public static string ComplexPassword = @"(?=(.*[0-9]))(?=.*[\!@#$%^&*()\\[\]{}\-_+=~`|:;""'<>,./?])(?=.*[a-z])(?=(.*[A-Z]))(?=(.*)).{8,}";

        // Should have 1 lowercase letter, 1 uppercase letter, 1 number, and be at least 8 characters long
        public static string ModeratePassword = "(?=(.*[0-9]))((?=.*[A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z]))^.{8,}$";

        // Alphanumeric string that may include _ and – having a length of 3 to 16 characters
        public static string HttpsUrl = "https?:\\/\\/(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{2,256}\\.[a-z]{2,6}\\b([-a-zA-Z0-9@:%_\\+.~#()?&//=]*)";

        public static string IpV4 = "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";

        public static string IpV6 = @"(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])\\.){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))";

        public static string UserName = "^[A-za-z0-9_-]{3,16}$";

        public static string IpV4AndIpV6 = @"((^\s*((([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]))\s*$)|(^\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d\d|[1-9]?\d)){3}))|:)))(%.+)?\s*$))";

        // Date Format YYYY-MM-dd
        public static string IsoDashDate = "^(\\d{4,5}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01]))$";

        public static string DateWithSlashes = "^[0-3]?[0-9]/[0-3]?[0-9]/(?:[0-9]{2})?[0-9]{2}$";

        public static string BritishEnglishDate = "(0?[1-9]|[12][0-9]|3[01])[- /.](0?[1-9]|1[012])[- /.](19|20)\\d\\d";

        public static string AmericanEnglishDate = "(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)[0-9]{2}";

        public static string SqlServerDate = "(\\d{4})-(\\d{2})-(\\d{2}) (\\d{2}):(\\d{2}):(\\d{2})";

        public static string Time12Hour = "((1[0-1]|0?[1-9]):([0-5][0-9]) ?([AaPp][Mm]))";

        public static string Time24Hour = "^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$";

        public static string HtmlTag = "<\\/?[\\w\\s]*>|<.+[\\W]>";

        public static string HasDupelicates = "(\\b\\w+\\b)(?=.*\\b\\1\\b)";

        public static string PhoneNumberInternational = "^(?:(?:\\(?(?:00|\\+)([1-4]\\d\\d|[1-9]\\d?)\\)?)?[\\-\\.\\ \\\\\\/]?)?((?:\\(?\\d{1,}\\)?[\\-\\.\\ \\\\\\/]?){0,})(?:[\\-\\.\\ \\\\\\/]?(?:#|ext\\.?|extension|x)[\\-\\.\\ \\\\\\/]?(\\d+))?$";

        public static string FilePath = "^(.+)\\/([^\\/]+)$";

        public static string UsPostalCode = "^(?!0{3})[0-9]{3,5}$";

        public static string SocialSecurity = "^((?!219-09-9999|078-05-1120)(?!666|000|9\\d{2})\\d{3}-(?!00)\\d{2}-(?!0{4})\\d{4})|((?!219 09 9999|078 05 1120)(?!666|000|9\\d{2})\\d{3} (?!00)\\d{2} (?!0{4})\\d{4})|((?!219099999|078051120)(?!666|000|9\\d{2})\\d{3}(?!00)\\d{2}(?!0{4})\\d{4})$";

        public static string USPassport = "^[0-9]{9}$";

        // can use either hypen(-) or space( ) character as separator
        // ref: https://stackoverflow.com/questions/9315647/regex-credit-card-number-tests

        public static string CardAmex = "^3[47][0-9]{13}$";

        public static string CardDiscover = "^6(?:011\\d{12}|5\\d{14}|4[4-9]\\d{13}|22(?:1(?:2[6-9]|[3-9]\\d)|[2-8]\\d{2}|9(?:[01]\\d|2[0-5]))\\d{10})$";

        public static string CardMastercard = "^(5[1-5][0-9]{14}|2(22[1-9][0-9]{12}|2[3-9][0-9]{13}|[3-6][0-9]{14}|7[0-1][0-9]{13}|720[0-9]{12}))$";

        public static string CardVisaMastercard = "^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14})$";

        public static string Hexidecimnal = "^#?([a-f0-9]{6}|[a-f0-9]{3})$";

        #endregion

        public static async Task<string> ShortenUrl(string url)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync("https://is.gd/create.php?format=simple&url=www.example.com" + url);
        }

        public static async Task<string> UnshortenUrl(string shortUrl)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync("https://unshorten.me/json/goo.gl/" + shortUrl);
        }

        public static string FormatUpRankedValue(decimal value, decimal one, decimal two, decimal three = 0, decimal four = 0)
        {
            if (value < one)
                return "magenta";
            else if (value > one && value < two)
                return "red";
            else if (value > one && value > two && value < three)
                return "darkgray";
            else if (value > three && value < four)
                return "green";
            else if (value > four)
                return "lime";
            else
                return "#000000";
        }

        public static string FormatDownRankedValue(decimal value, decimal one, decimal two, decimal three = 0, decimal four = 0)
        {
            if (value < one)
                return "lime";
            else if (value > one && value < two)
                return "green";
            else if (value > one && value > two && value < three)
                return "darkgray";
            else if (value > three && value < four)
                return "red";
            else if (value > four)
                return " magenta";
            else
                return "#000000";
        }

        public static string CopyrightSymbol()
        {
            return "&#169;";
        }

        public static string TMSymbol()
        {
            return "&#8482;";
        }

        public static string USDate(string date)
        {
            return DateTime.Parse(date, new CultureInfo("en-US")).ToString();
        }

        public static string UKDate(string date)
        {
            return DateTime.Parse(date, new CultureInfo("en-UK")).ToString();
        }

        [Obsolete("This method will be deprecated soon.")]
        public bool DeprecatedExample()
        {
            return true;
        }

        // ref: https://github.com/mono/mono/blob/master/mcs/class/System.Json/System.Json/JsonValue.cs
        private static bool NeedEscape(string src, int i)
        {
            char c = src[i];
            return c < 32 || c == '"' || c == '\\'
                // Broken lead surrogate
                || (c >= '\uD800' && c <= '\uDBFF' &&
                    (i == src.Length - 1 || src[i + 1] < '\uDC00' || src[i + 1] > '\uDFFF'))
                // Broken tail surrogate
                || (c >= '\uDC00' && c <= '\uDFFF' &&
                    (i == 0 || src[i - 1] < '\uD800' || src[i - 1] > '\uDBFF'))
                // To produce valid JavaScript
                || c == '\u2028' || c == '\u2029'
                // Escape "</" for <script> tags
                || (c == '/' && i > 0 && src[i - 1] == '<');
        }

        public static string EscapeString(string src)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            int start = 0;
            for (int i = 0; i < src.Length; i++)
            {
                if (NeedEscape(src, i))
                {
                    sb.Append(src, start, i - start);
                    switch (src[i])
                    {
                        case '\b': sb.Append("\\b"); break;
                        case '\f': sb.Append("\\f"); break;
                        case '\n': sb.Append("\\n"); break;
                        case '\r': sb.Append("\\r"); break;
                        case '\t': sb.Append("\\t"); break;
                        case '\"': sb.Append("\\\""); break;
                        case '\\': sb.Append("\\\\"); break;
                        case '/': sb.Append("\\/"); break;
                        default:
                            sb.Append("\\u");
                            sb.Append(((int)src[i]).ToString("x04"));
                            break;
                    }

                    start = i + 1;
                }
            }

            sb.Append(src, start, src.Length - start);
            return sb.ToString();
        }
    }
}

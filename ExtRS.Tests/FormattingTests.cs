using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Sonrai.ExtRS.UnitTests
{
    [TestClass]
    public class FomattingTests
    {
        [TestMethod]
        public void CopyrightSymbolSucceeds()
        {
            Assert.IsTrue(FormattingService.CopyrightSymbol() == "&#169;");
        }

        [TestMethod]
        public void TMSymbolSucceeds()
        {
            Assert.IsTrue(FormattingService.TMSymbol() == "&#8482;");
        }

        [TestMethod]
        public void GetBillionsSucceeds()
        {
            var result = FormattingService.ToMillions(200000000);
            Assert.IsTrue(result == 200);
        }

        [TestMethod]
        public void GetMillionsSucceeds()
        {
            var result = FormattingService.ToBillions(200000000000);
            Assert.IsTrue(result == 200);
        }

        [TestMethod]
        public void GetTrillionsSucceeds()
        {
            var result = FormattingService.ToTrillions(200000000000000);
            Assert.IsTrue(result == 200);
        }

        [TestMethod]
        public void ConvertInchesToFeetAndInchesSucceeds()
        {
            var result = FormattingService.ConvertInchesToFeetAndInches(68);
            Assert.IsTrue(result.Contains("'"));
            Assert.IsTrue(result.Contains("\""));
        }

        [TestMethod]
        public async Task ShortenUrlSucceeds()
        {
            string url = "www.google.com/someplace";
            string result = await FormattingService.ShortenUrl(url);
            Assert.IsTrue(result.Length < url.Length);
        }

        [TestMethod]
        public async Task UnshortenUrlSucceeds()
        {
            string url = "https://tinyurl.com/2p8z2mm9";
            string result = await FormattingService.UnshortenUrl(url);
            Assert.IsTrue(result.Length > url.Length);
        }

        [TestMethod]
        public void CsvToJsonSucceeds()
        {
            var csv = @"comma,separated,values
                        are,super,duper
                        fun,fun,forever
                        data, is, beautiful";
            var json = FormattingService.CsvToJson(csv, ",");
            Assert.IsTrue(JToken.Parse(json!).HasValues);
        }

        [TestMethod]
        public void HexidecialSucceeds()
        {
            var match = Regex.Match("#000000", FormattingService.HexidecimnalRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void HexidecialFails()
        {
            var match = Regex.Match("#33ff355$", FormattingService.HexidecimnalRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void SocialSecuritySucceeds()
        {
            var match = Regex.Match("111-22-1111", FormattingService.SocialSecurityRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void SocialSecurityFails()
        {
            var match = Regex.Match("11-2-34453-1", FormattingService.SocialSecurityRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void AlphanumericSucceeds()
        {
            var match = Regex.Match("AaaBbbCcc1133", FormattingService.AlphanumericRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void AlphanumericWithSpacesSucceeds()
        {
            var match = Regex.Match("Aaa BbbCcc111222333", FormattingService.AlphanumericWithSpacesRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void AlphanumericWithSpacesFails()
        {
            var match = Regex.Match("Aaa^BbbCcc+111222333", FormattingService.AlphanumericWithSpacesRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void CardAmexSucceeds()
        {
            var match = Regex.Match("378282246310005", FormattingService.CardAmexRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void CardAmexFails()
        {
            var match = Regex.Match("3782224631005", FormattingService.CardAmexRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void CardDiscoverSucceeds()
        {
            var match = Regex.Match("6011000990139424", FormattingService.CardDiscoverRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void CardDiscoverFails()
        {
            var match = Regex.Match("601111111111117", FormattingService.CardDiscoverRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void CardMastercardSucceeds()
        {
            var match = Regex.Match("5555555555554444", FormattingService.CardMastercardRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void CardMastercardFails()
        {
            var match = Regex.Match("55555555555544", FormattingService.CardMastercardRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void ComplexPasswordSucceeds()
        {
            var match = Regex.Match("lL1!eightchars", FormattingService.ComplexPasswordRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void ComplexPasswordFails()
        {
            var match = Regex.Match("lL1eightchars", FormattingService.ComplexPasswordRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void DecimalNumbersSucceeds()
        {
            var match = Regex.Match("3.14", FormattingService.DecimalNumbersRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void DecimalNumbersFails()
        {
            var match = Regex.Match("5", FormattingService.DecimalNumbersRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void DateWithSlashesSucceeds()
        {
            var match = Regex.Match("03/15/1983", FormattingService.DateWithSlashesRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void DateWithSlashesFails()
        {
            var match = Regex.Match("15/3-1983", FormattingService.DateWithSlashesRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void AmericanEnglishDateSucceeds()
        {
            var match = Regex.Match("03/15/1983", FormattingService.AmericanEnglishDateRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void AmericanEnglishDateFails()
        {
            var match = Regex.Match("15/03/1983", FormattingService.AmericanEnglishDateRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void BritishEnglishDateSucceeds()
        {
            var match = Regex.Match("15/03/1983", FormattingService.BritishEnglishDateRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void BritishEnglishDateFails()
        {
            var match = Regex.Match("15/14/1983", FormattingService.BritishEnglishDateRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void FilePathSucceeds()
        {
            var match = Regex.Match("var/some/place.pdf", FormattingService.FilePathRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void FilePathFails()
        {
            var match = Regex.Match("var|some|place.", FormattingService.FilePathRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void HasDupelicatesSucceeds()
        {
            var match = Regex.Match("this contains contains dupes", FormattingService.HasDupelicatesRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void HasDupelicatesFails()
        {
            var match = Regex.Match("eachcharisdiferent eachWordIsDifferent", FormattingService.HasDupelicatesRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void HtmlTagSucceeds()
        {
            var match = Regex.Match("<input type='button' />", FormattingService.HtmlTagRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void HtmlTagFails()
        {
            var match = Regex.Match("{'some':'value' }", FormattingService.HtmlTagRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void HttpsUrlSucceeds()
        {
            var match = Regex.Match("https://tel.net", FormattingService.HttpsUrlRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void HttpsUrlFails()
        {
            var match = Regex.Match("http//tel.net", FormattingService.HttpsUrlRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void IpV4Succeeds()
        {
            var match = Regex.Match("8.8.8.8", FormattingService.IpV4Regex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void IpV4Fails()
        {
            var match = Regex.Match("8.7.5.esVV.oc.oc", FormattingService.IpV4Regex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void IpV6Succeeds()
        {
            var match = Regex.Match("2001:0db8:85a3:0000:0000:8a2e:0370:7334", FormattingService.IpV6Regex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void IpV6Fails()
        {
            var match = Regex.Match("1:22:85a3:0000-27-8a2e-0370-7334", FormattingService.IpV6Regex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void IpV4AndIpV6Succeeds()
        {
            var match = Regex.Match("8.8.8.4", FormattingService.IpV4AndIpV6Regex);
            Assert.IsTrue(match.Success);
            match = Regex.Match("2001:0db8:85a3:0000:0000:8a2e:0370:7334", FormattingService.IpV4AndIpV6Regex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void IIpV4AndIpV6Fails()
        {
            var match = Regex.Match("1:33:85a3:0000-27--0370-_", FormattingService.IpV4AndIpV6Regex);
            Assert.IsTrue(!match.Success);
            match = Regex.Match("89.345.23.aa.c-1", FormattingService.IpV4AndIpV6Regex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void IsoDashDateSucceeds()
        {
            var match = Regex.Match("2024-01-01", FormattingService.IsoDashDateRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void IsoDashDateFails()
        {
            var match = Regex.Match("00/11/234", FormattingService.IsoDashDateRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void ModeratePasswordSucceeds()
        {
            var match = Regex.Match("l34534Rdhfbdth", FormattingService.ModeratePasswordRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void ModeratePasswordFails()
        {
            var match = Regex.Match("lRdhdth", FormattingService.ModeratePasswordRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void USPassportSucceeds()
        {
            var match = Regex.Match("123456789", FormattingService.USPassportRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void USPassportFails()
        {
            var match = Regex.Match("12345", FormattingService.USPassportRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void PhoneNumberInternationalSucceeds()
        {
            var match = Regex.Match("+44 7911 123456", FormattingService.PhoneNumberInternationalRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void PhoneNumberInternationalFails()
        {
            var match = Regex.Match("12345-dhguhb-01", FormattingService.PhoneNumberInternationalRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void SqlServerDateSucceeds()
        {
            var match = Regex.Match("2023-05-10 20:33:14.160", FormattingService.SqlServerDateRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void SqlServerDateFails()
        {
            var match = Regex.Match("2023/05/1014.160", FormattingService.SqlServerDateRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void Time12HourSucceeds()
        {
            var match = Regex.Match("1:00am", FormattingService.Time12HourRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void Time12HourFails()
        {
            var match = Regex.Match("24:00", FormattingService.Time12HourRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void Time24HourSucceeds()
        {
            var match = Regex.Match("21:00", FormattingService.Time24HourRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void Time24HourFails()
        {
            var match = Regex.Match("22:01am", FormattingService.Time24HourRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void UserNameSucceeds()
        {
            var match = Regex.Match("SomeUser007", FormattingService.UserNameRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void UserNameFails()
        {
            var match = Regex.Match("B@-#!!!{uzr%^&(+    ", FormattingService.UserNameRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void UsPostalCodeSucceeds()
        {
            var match = Regex.Match("90210", FormattingService.UsPostalCodeRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void UsPostalCodeFails()
        {
            var match = Regex.Match("23-90810-12345632", FormattingService.UsPostalCodeRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void WholeAndDecimalNumbersSucceeds()
        {
            var match = Regex.Match("10", FormattingService.WholeAndDecimalNumbersRegex);
            Assert.IsTrue(match.Success);
            match = Regex.Match("10.123", FormattingService.WholeAndDecimalNumbersRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void WholeAndDecimalNumbersCodeFails()
        {
            var match = Regex.Match("ASFDOIUHF(H_", FormattingService.WholeAndDecimalNumbersRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void WholeNumbersSucceeds()
        {
            var match = Regex.Match("10", FormattingService.WholeNumbersRegex);
            Assert.IsTrue(match.Success);
        }

        [TestMethod]
        public void WholeNumbersFails()
        {
            var match = Regex.Match("1.12", FormattingService.WholeNumbersRegex);
            Assert.IsTrue(!match.Success);
        }

        [TestMethod]
        public void DelimitedToJsonSucceeds()
        {
            string csv = @"comma,separated,values
                        are,super,duper
                        fun,fun,forever
                        data, is, beautiful";
            string json = FormattingService.CsvToJson(csv, ",")!;
            Assert.IsTrue(JToken.Parse(json).HasValues);
        }

        [TestMethod]
        public void ConvertJsonToXmlSucceeds()
        {
            string result = FormattingService.ConvertJsonToXml("{ 'some':'thing' }");
            Assert.IsTrue(result.Contains("/Root>"));
        }

        [TestMethod]
        public void ConvertXmlToJsonSucceeds()
        {
            string result = FormattingService.ConvertXmlToJson("<xml><childXml></childXml></xml>");
            Assert.IsTrue(result.Contains("{"));
        }

        [TestMethod]
        public void SerializeObjectSucceeds()
        {
            List<string> list = new List<string> { "Aa", "Bb", "Cc" };
            var result = FormattingService.SerializeObject(list);
            Assert.IsTrue(result.Contains("["));
        }

        [TestMethod]
        public void ConvertToAsciiSucceeds()
        {
            Bitmap image = new Bitmap(@"..\..\..\Resources\my_friend_benn.jpg", true);
            image = FormattingService.GetResizedImage(image, 200);
            string content = FormattingService.ConvertToAscii(image);
            Assert.IsTrue(content.Length > 0);
        }
    }
}

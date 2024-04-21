using GoogleMaps.LocationServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Threading.Tasks;
using Sonrai.ExtRS;
using static Sonrai.ExtRS.ReferenceDataService;
using Sonrai.ExtRS.Models.Tiingo;

namespace Sonrai.ExtRS.UnitTests
{
    [TestClass]
    public class ReferenceDataTests
    {
        public readonly string tiingoAuthToken = "9fa5a9bfbcc5cdb6ea5cd9da8acc6dd2de079d67";
        ReferenceDataService.Address origin = new ReferenceDataService.Address() { AddressLine = "2042 Oakland Ave.", City = "Milwaukee", State = "WI", PostalCode = "53204", Country = "US" };
        ReferenceDataService.Address destination = new ReferenceDataService.Address() { AddressLine = "742 Harrison Ave.", City = "Beloit", State = "WI", PostalCode = "53511", Country = "US" };
        public static string upsAuthKey = "";
        public static string fedExAuthKey = "";

        [TestInitialize]
        public void TestInitialize()
        {
            if(upsAuthKey == "")
            {
                upsAuthKey = ReferenceDataService.GetAuthToken(Shipper.UPS, "h4EOs5OQVkzzATVpEzILm4GGIOwgNPooYzGnsRuxg9yFQ3Lb", "TUAPkGeNKJCqMmf3ErUZmEzTjv13CnlBWe9Gl60IdMhVpfPwmVTa1DgxAuYCnwVA", "5DD50B188C74A581", "https://fissonrai.io");
                Assert.IsTrue(upsAuthKey.Length > 0);
                fedExAuthKey = ReferenceDataService.GetAuthToken(Shipper.FedEx, "l7254d78ae8b824b178c6096cd02867c58", "79e65ed3083541ecbfa829a9dfea1b58");
                Assert.IsTrue(fedExAuthKey.Length > 0);
            }
        }

        [TestMethod]
        public async Task GetSynonymsSucceeds()
        {
            var result = await ReferenceDataService.GetSynonyms("nonplussed bored");
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetSynonymsFails()
        {
            var result = await ReferenceDataService.GetSynonyms(null);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetAppPublicIPSucceeds()
        {
            var result = await ReferenceDataService.GetAppPublicIP();
            var ip = result.Substring(result.Length - 7);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetCountryByIPSucceeds()
        {
            var result = await ReferenceDataService.GetCountryByIP("8.8.8.4");
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetTickerInfoSucceeds()
        {
            var result = await ReferenceDataService.GetTickerInfo("JCI", tiingoAuthToken);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetTickerInfoObjectSucceeds()
        {
            var result = await ReferenceDataService.GetTickerInfoObject("JCI", tiingoAuthToken);
            Assert.IsTrue(result.StartDate != null);
        }

        [TestMethod]
        public async Task GetTickerPriceSucceeds()
        {
            var result = await ReferenceDataService.GetTickerPrice("JCI", tiingoAuthToken);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetTickerPriceHistorySucceeds()
        {
            var result = await ReferenceDataService.GetTickerPriceHistory("JCI", DateTime.Now.AddDays(-7).ToString(), DateTime.Now.ToString(), tiingoAuthToken);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetTickerPriceObjectSucceeds()
        {
            Ticker result = await ReferenceDataService.GetTickerPriceObject("JCI", tiingoAuthToken);
            Assert.IsTrue(result.Open != null);
            Assert.IsTrue(result.High != null);
            Assert.IsTrue(result.Close != null);
        }

        [TestMethod]
        public async Task GetTickerPriceHistoryObjectSucceeds()
        {
            List<Ticker> result = await ReferenceDataService.GetTickerPriceHistoryObject("JCI", DateTime.Now.AddDays(-7).ToString(), DateTime.Now.ToString(), tiingoAuthToken);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public async Task GetForexPriceSucceeds()
        {
            var result = await ReferenceDataService.GetForexPrice("EURUSD", tiingoAuthToken);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetGoogleNewsSucceeds()
        {
            var result = await ReferenceDataService.GetGoogleNews("IBM");
            Assert.IsTrue(result.Count > 0);
        }

        [Ignore]
        [TestMethod]
        public void GetGoogleTranslationSucceeds()
        {
            var result = ReferenceDataService.GetGoogleTranslation("extras", "en", "fr", "[YourGoogleAPIKey]");
            Assert.IsTrue(result == "Suppléments");
        }

        [TestMethod]
        public void GetShippingRatesSucceeds()
        {
            RestResponse result = ReferenceDataService.GetShippingRates(Shipper.USPS, "PRIORITY", 5, 3, origin, destination, "6856SONRAH845", "", "", false);
            Assert.IsTrue(result.IsSuccessful);
            result = ReferenceDataService.GetShippingRates(Shipper.UPS, "OVERNIGHT", 2, 2, origin, destination, "", upsAuthKey, "C2016A", false);
            Assert.IsTrue(result.IsSuccessful);
            result = ReferenceDataService.GetShippingRates(Shipper.FedEx, "GROUND", 2, 2, origin, destination, "", fedExAuthKey, "", false);
            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void GetVerifyAddressSucceeds()
        {
            RestResponse result = ReferenceDataService.GetVerifyAddressUSPS("6856SONRAH845", "2079 Yorkshire Dr.", "", "Beloit", "WI", "53511", false);
			Assert.IsFalse(result.Content!.Contains("Invalid"));
			Assert.IsTrue(result.IsSuccessful);
        }

		[TestMethod]
        public void GetTrackingInfoSucceeds()
        {
            var result = ReferenceDataService.GetTrackingInfo(Shipper.USPS, "3SONRA323Q721", "3SONRA323Q721");
            Assert.IsTrue(result.IsSuccessful);
            string authToken = ReferenceDataService.GetAuthTokenUPS("h4EOs5OQVkzzATVpEzILm4GGIOwgNPooYzGnsRuxg9yFQ3Lb", "TUAPkGeNKJCqMmf3ErUZmEzTjv13CnlBWe9Gl60IdMhVpfPwmVTa1DgxAuYCnwVA", "", "https://fissonrai.io");
            result = ReferenceDataService.GetTrackingInfo(Shipper.UPS, "", "", authToken);
            Assert.IsTrue(result.IsSuccessful);
            authToken = ReferenceDataService.GetAuthTokenFedEx("l7254d78ae8b824b178c6096cd02867c58", "79e65ed3083541ecbfa829a9dfea1b58");
            result = ReferenceDataService.GetTrackingInfo(Shipper.FedEx, "9261299991099834284833", "", authToken);
            Assert.IsTrue(result.IsSuccessful);
        }
    }
}

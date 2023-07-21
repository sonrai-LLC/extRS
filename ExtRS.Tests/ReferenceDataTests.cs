using GoogleMaps.LocationServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Threading.Tasks;
using Sonrai.ExtRS;

namespace Sonrai.ExtRS.UnitTests
{
    [TestClass]
    public class ReferenceDataTests
    {
        public readonly string tiingoAuthToken = "9fa5a9bfbcc5cdb6ea5cd9da8acc6dd2de079d67";
        ReferenceDataService.Address origin = new ReferenceDataService.Address() { AddressLine = "2042 Oakland Ave.", City = "Milwaukee", State = "WI", PostalCode = "53204", Country = "US" };
        ReferenceDataService.Address destination = new ReferenceDataService.Address() { AddressLine = "742 Harrison Ave.", City = "Beloit", State = "WI", PostalCode = "53511", Country = "US" };
        public static string upsAuthKey;
        public static string fedExAuthKey;

        [TestInitialize]
        public void TestInitialize()
        {
            upsAuthKey = ReferenceDataService.GetAuthToken("UPS", "h4EOs5OQVkzzATVpEzILm4GGIOwgNPooYzGnsRuxg9yFQ3Lb", "TUAPkGeNKJCqMmf3ErUZmEzTjv13CnlBWe9Gl60IdMhVpfPwmVTa1DgxAuYCnwVA", "5DD50B188C74A581", "https://fissonrai.io");
            Assert.IsTrue(upsAuthKey.Length > 0);
            fedExAuthKey = ReferenceDataService.GetAuthToken("FedEx", "l7254d78ae8b824b178c6096cd02867c58", "79e65ed3083541ecbfa829a9dfea1b58");
            Assert.IsTrue(fedExAuthKey.Length > 0);
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
        public async Task GetTickerPricesSucceeds()
        {
            var result = await ReferenceDataService.GetTickerPrices("JCI", tiingoAuthToken);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetForexPriceSucceeds()
        {
            var result = await ReferenceDataService.GetForexPrice("EURUSD", tiingoAuthToken);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void GetAuthTokensSucceeds()
        {

        }

        [TestMethod]
        public void GetShippingRatesSucceeds()
        {
            RestResponse result; // = ReferenceDataService.GetShippingRates("PRIORITY", "USPS", 5, 3, "53511", "53235", "6856SONRAH845");
            //Assert.IsTrue(result.IsSuccessful);
            //result = ReferenceDataService.GetShippingRates("OVERNIGHT", "UPS", 2, 2, origin, destination, "", upsAuthKey, "C2016A", false);
            //Assert.IsTrue(result.IsSuccessful);
            result = ReferenceDataService.GetShippingRates("GROUND", "FedEx", 2, 2, origin, destination, "", fedExAuthKey, "", false);
            Assert.IsTrue(result.IsSuccessful);
        }

        [TestMethod]
        public void GetTrackingInfoSucceeds()
        {
            var result = ReferenceDataService.GetTrackingInfo("USPS", "3SONRA323Q721", "3SONRA323Q721");
            Assert.IsTrue(result.IsSuccessful);
            string authToken = ReferenceDataService.GetAuthTokenUPS("h4EOs5OQVkzzATVpEzILm4GGIOwgNPooYzGnsRuxg9yFQ3Lb", "TUAPkGeNKJCqMmf3ErUZmEzTjv13CnlBWe9Gl60IdMhVpfPwmVTa1DgxAuYCnwVA", "", "https://fissonrai.io");
            result = ReferenceDataService.GetTrackingInfo("UPS", "", "", authToken);
            Assert.IsTrue(result.IsSuccessful);
            authToken = ReferenceDataService.GetAuthTokenFedEx("l7254d78ae8b824b178c6096cd02867c58", "79e65ed3083541ecbfa829a9dfea1b58");
            result = ReferenceDataService.GetTrackingInfo("FedEx", "9261299991099834284833", "", authToken);
            Assert.IsTrue(result.IsSuccessful);
        }
    }
}

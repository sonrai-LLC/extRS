using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Threading.Tasks;

namespace Sonrai.ExtRS.UnitTests
{
    [TestClass]
    public class ReferenceDataTests
    {
        public readonly string token = "9fa5a9bfbcc5cdb6ea5cd9da8acc6dd2de079d67";

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
            var result = await ReferenceDataService.GetTickerInfo("JCI", token);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetTickerPricesSucceeds()
        {
            var result = await ReferenceDataService.GetTickerPrices("JCI", token);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetForexPriceSucceeds()
        {
            var result = await ReferenceDataService.GetForexPrice("EURUSD", token);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void GetAuthTokensSucceeds()
        {
            var result = ReferenceDataService.GetAuthToken("UPS", "h4EOs5OQVkzzATVpEzILm4GGIOwgNPooYzGnsRuxg9yFQ3Lb", "TUAPkGeNKJCqMmf3ErUZmEzTjv13CnlBWe9Gl60IdMhVpfPwmVTa1DgxAuYCnwVA", "5DD50B188C74A581", "https://fissonrai.io");
            Assert.IsTrue(result.Length > 0);
            result = ReferenceDataService.GetAuthToken("FedEx", "l7254d78ae8b824b178c6096cd02867c58", "79e65ed3083541ecbfa829a9dfea1b58");
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void GetShippingRatesSucceeds()
        {
            RestResponse result = ReferenceDataService.GetShippingRates("PRIORITY", "USPS", 5, 3, "53511", "53235", "3SONRA323Q721");
            Assert.IsTrue(result.IsSuccessful);
            result = ReferenceDataService.GetShippingRates("UPS", 2, 2, "53511", "53235", "CFITZ001");
            Assert.IsTrue(1 == 2);
            //result = a//it ReferenceDataService.GetShippingRates("FedEx", 2, 2, "53511", "53235", "CFITZ001");
            //Assert.IsT//e(result.Length > 0);
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

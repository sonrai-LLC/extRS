using GoogleMaps.LocationServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Threading.Tasks;
using Sonrai.ExtRS;
using static Sonrai.ExtRS.ReferenceDataService;
using Sonrai.ExtRS.Models.Tiingo;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Reflection;
using System;
using System.Configuration;

namespace Sonrai.ExtRS.UnitTests
{
    [TestClass]
    public class ReferenceDataTests
    {
        public static string tiingoAPIToken = "";
        ReferenceDataService.Address origin = new ReferenceDataService.Address() { AddressLine = "2042 Oakland Ave.", City = "Milwaukee", State = "WI", PostalCode = "53204", Country = "US" };
        ReferenceDataService.Address destination = new ReferenceDataService.Address() { AddressLine = "742 Harrison Ave.", City = "Beloit", State = "WI", PostalCode = "53511", Country = "US" };
        public static string upsAuthKey = "";
        public static string fedExAuthKey = "";
        public static string upsId = "";
        public static string upsSecret = "";
        public static string fedexId = "";
        public static string fedexSecret = "";
        public static string googleAPIKey = "";

        private IConfiguration _configuration { get; }

        public ReferenceDataTests()
        {
            // set your API ids and secrets in UserSecrets (right-click project: "Manage User Secrets")
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<ReferenceDataTests>();
            _configuration = builder.Build();
            var secretVals = _configuration.GetChildren().ToList();
            upsId = secretVals.Where(x => x.Key == "upsId").First().Value!;
            upsSecret = secretVals.Where(x => x.Key == "upsSecret").First().Value!;
            fedexId = secretVals.Where(x => x.Key == "fedexId").First().Value!;
            fedexSecret = secretVals.Where(x => x.Key == "fedexSecret").First().Value!;
            tiingoAPIToken = secretVals.Where(x => x.Key == "tiingoAPIToken").First().Value!;
            googleAPIKey = secretVals.Where(x => x.Key == "googleAPIKey").First().Value!;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            if (upsAuthKey == "")
            {
                upsAuthKey = ReferenceDataService.GetAuthToken(Shipper.UPS, upsId, upsSecret, "5DD50B188C74A581", "https://fissonrai.io");
                Assert.IsTrue(upsAuthKey.Length > 0);
                fedExAuthKey = ReferenceDataService.GetAuthToken(Shipper.FedEx, fedexId, fedexSecret);
                Assert.IsTrue(fedExAuthKey.Length > 0);
            }
        }

        [TestMethod]
        public async Task GetSynonymsSucceeds()
        {
            var result = await ReferenceDataService.GetSynonyms("nonplussed bored");
            Assert.IsTrue(result.Length > 2);
        }

        [TestMethod]
        public async Task GetSynonymsFails()
        {
            var result = await ReferenceDataService.GetSynonyms(null!);
            Assert.IsTrue(result == "[]");
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
            var result = await ReferenceDataService.GetTickerInfo("JCI", tiingoAPIToken);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetTickerInfoObjectSucceeds()
        {
            var result = await ReferenceDataService.GetTickerInfoObject("JCI", tiingoAPIToken);
            Assert.IsTrue(result.StartDate > DateTime.MinValue);
        }

        [TestMethod]
        public async Task GetTickerPriceSucceeds()
        {
            var result = await ReferenceDataService.GetTickerPrice("JCI", tiingoAPIToken);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetTickerPriceHistorySucceeds()
        {
            var result = await ReferenceDataService.GetTickerPriceHistory("JCI", DateTime.Now.AddDays(-7).ToString(), DateTime.Now.ToString(), tiingoAPIToken);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetTickerPriceObjectSucceeds()
        {
            Ticker result = await ReferenceDataService.GetTickerPriceObject("JCI", tiingoAPIToken);
            Assert.IsTrue(result.Open > 0);
            Assert.IsTrue(result.High > 0);
            Assert.IsTrue(result.Close > 0);
        }

        [TestMethod]
        public async Task GetTickerPriceHistoryObjectSucceeds()
        {
            List<Ticker> result = await ReferenceDataService.GetTickerPriceHistoryObject("JCI", DateTime.Now.AddDays(-7).ToString(), DateTime.Now.ToString(), tiingoAPIToken);
            Assert.IsTrue(result.Count > 0);
        }

        [Ignore]
        [TestMethod]
        public async Task GetForexPriceSucceeds()
        {
            var result = await ReferenceDataService.GetForexPrice("EURUSD", tiingoAPIToken);
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public async Task GetGoogleNewsSucceeds()
        {
            var result = await ReferenceDataService.GetGoogleNews("IBM");
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public async Task GetGoogleNewsWithLinksSucceeds()
        {
            var result = await ReferenceDataService.GetGoogleNewsWithLinks("IBM");
            Assert.IsTrue(result.Count > 0);
        }

        [Ignore]
        [TestMethod]
        public void GetGoogleTranslationSucceeds()
        {
            var result = ReferenceDataService.GetGoogleTranslation("extras", "en", "fr", googleAPIKey);
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
            string authToken = ReferenceDataService.GetAuthTokenUPS(upsId, upsSecret, "", "https://fissonrai.io");
            result = ReferenceDataService.GetTrackingInfo(Shipper.UPS, "", "", authToken);
            Assert.IsTrue(result.IsSuccessful);
            authToken = ReferenceDataService.GetAuthTokenFedEx(fedexId, fedexSecret);
            result = ReferenceDataService.GetTrackingInfo(Shipper.FedEx, "377101283611590", "", authToken);
            Assert.IsTrue(result.IsSuccessful);
        }
    }
}

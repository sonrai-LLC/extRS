using GoogleMaps.LocationServices;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Sonrai.ExtRS.UnitTests
{
    [TestClass]
    public class GISTests
    {
        private GISService? _gis;

        private IConfiguration _configuration { get; }

        public GISTests()
        {
            var builder = new ConfigurationBuilder()
              .AddUserSecrets<GISTests>();
            _configuration = builder.Build();
        }

        [TestInitialize]
        public void Init()
        {
            _gis = new GISService(new HttpClient(), new GoogleLocationService(_configuration["googleApiKey"])); // Google Maps API key, found here https://developers.google.com/maps/documentation/javascript/get-api-key
        }

        [TestMethod]
        public void ValidateAddressSucceeds()
        {
            var result = _gis!.ValidateAddress("Beloit, WI");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateAddressFails()
        {
            var result = _gis!.ValidateAddress("?-$#, ,,_}%");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetLocationFails()
        {
            var result = _gis!.GetLocation("Beloit, WI");
            Assert.IsTrue(result.Long!.Length > 0);
        }

        [TestMethod]
        public void GetLocationReturnsNothing()
        {
            var result = _gis!.GetLocation("IYTDFOUYFUILYG");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetLocationsSucceeds()
        {
            List<string> locations = new List<string> { "Chicago, IL", "Milwaukee, WI", "Detroit, MI" };
            var result = _gis!.GetLocations(locations);
            Assert.IsTrue(result.Count == 3);
        }

        [TestMethod]
        public void GetLocationsReturnsNothing()
        {
            List<string> locations = new List<string> { "adv??asdvgsd, _B", "adgsdgvs, RE", "YTFYT??, H+" };
            var result = _gis!.GetLocations(locations);
            Assert.IsEmpty(result);
        }

        [TestMethod]
        public void GetUnitedStatesFlagUrlSucceeds()
        {
            var result = GISService.GetUnitedStatesFlagUrl("WI");
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void GetUnitedStatesFlagUrlFails()
        {
            Assert.ThrowsExactly<InvalidOperationException>(() => GISService.GetUnitedStatesFlagUrl(null!));
        }

        [TestMethod]
        public async Task GetUnitedStatesFlagImageSucceeds()
        {
            var result = await _gis!.GetUnitedStatesFlagImage("wisconsin");
            Assert.IsNotEmpty(result);
        }

        [TestMethod]
        public async Task GetUnitedStatesFlagImageFails()
        {
            await Assert.ThrowsExactlyAsync<HttpRequestException>(() => _gis!.GetUnitedStatesFlagImage("ZZ"));
        }

        [TestMethod]
        public void GetStateNameFromStateAbbreviationSucceeds()
        {
            var result = GISService.GetStateNameFromStateAbbreviation("WI");
            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod]
        public void GetStateNameFromStateAbbreviationFails()
        {
            Assert.ThrowsExactly<InvalidOperationException>(() => GISService.GetStateNameFromStateAbbreviation("ZZ"));
        }

        [TestMethod]
        public void GetStateAbbreviationFromStateNameSucceeds()
        {
            var result = GISService.GetStateAbbreviationFromStateName("Wisconsin");
            Assert.IsGreaterThan(0, result.Length);
        }

        [TestMethod]
        public void GetStateAbbreviationFromStateNameFails()
        {
            Assert.ThrowsExactly<InvalidOperationException>(() => GISService.GetStateAbbreviationFromStateName("Milwaukee"));
        }

        [TestMethod]
        public void StateAbbreviationsSucceeds()
        {
            var result = GISService.StateAbbreviations().Count > 50;
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StateNamesSucceeds()
        {
            var result = GISService.StateNames().Count > 50;
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetStateOrProvinceNameSucceeds()
        {
            var result = GISService.GetStateOrProvinceName("WI");
            Assert.IsTrue(result.Length > 0);
        }
    }
}

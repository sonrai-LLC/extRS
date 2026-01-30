using GoogleMaps.LocationServices;
using Microsoft.AspNetCore.Http;
using Sonrai.ExtRS.Models;
using Sonrai.ExtRS.Models.GIS;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace Sonrai.ExtRS
{
    public class GISService
    {
        private readonly GoogleLocationService _locationService;
        private HttpClient _client;
        private static readonly string states101FlagUrl = "https://www.states101.com/img/flags/svg/";
        public GISService(HttpClient client, GoogleLocationService locationService)
        {
            _client = client;
            _locationService = locationService;
        }

        public Location GetLocation(string address)
        {
            MapPoint coords = _locationService.GetLatLongFromAddress(address.Replace(" ", "+") + "," + address.Replace(" ", "+") + "," + address.Replace(" ", "+"));
            Location location;
            if (coords != null)
            {
                location = new Location() { Lat = coords.Latitude.ToString(), Long = coords.Longitude.ToString() };
            }
            else
            {
                return null!;
            }

            return location;
        }

        public bool ValidateAddress(string address)
        {
            return GetLocation(address) != null;
        }

        public List<Location> GetLocations(List<string> addresses)
        {
            var locations = new List<Location>();
            foreach (string address in addresses)
            {
                Location unused = new Location();
                MapPoint coords = _locationService.GetLatLongFromAddress(address.Replace(" ", "+") + "," + address.Replace(" ", "+") + "," + address.Replace(" ", "+"));
                if (coords != null)
                {
                    Location location = new Location() { Lat = coords.Latitude.ToString(), Long = coords.Longitude.ToString() };
                    locations.Add(location);
                }
            }

            return locations;
        }

        public static string GetUnitedStatesFlagUrl(string stateAbbrev)
        {
            return string.Format("{0}{1}.svg", states101FlagUrl, GetStateNameFromStateAbbreviation(stateAbbrev));
        }

        public async Task<byte[]> GetUnitedStatesFlagImage(string state)
        {
            return await _client.GetByteArrayAsync(string.Format("{0}{1}.svg", states101FlagUrl, state));
        }

        public static string GetStateNameFromStateAbbreviation(string abbrev)
        {
            return LocationRefData.StatesAndProvinces.First(x => x.Abbreviation == abbrev).Name;
        }

        public static string GetStateAbbreviationFromStateName(string stateName)
        {
            return LocationRefData.StatesAndProvinces.First(x => x.Name == stateName).Abbreviation;
        }

        public static List<string> StateAbbreviations()
        {
            return LocationRefData.StatesAndProvinces.Select(s => s.Abbreviation).ToList();
        }

        public static List<string> StateNames()
        {
            return LocationRefData.StatesAndProvinces.Select(s => s.Name).ToList();
        }

        public static string GetStateOrProvinceName(string abbreviation)
        {
            return LocationRefData.StatesAndProvinces.Where(s => s.Abbreviation.Equals(abbreviation, StringComparison.CurrentCultureIgnoreCase)).Select(s => s.Name).FirstOrDefault()!;
        }

        public static string GetAbbreviation(string name)
        {
            return LocationRefData.StatesAndProvinces.Where(s => s.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).Select(s => s.Abbreviation).FirstOrDefault()!;
        }

        public static List<State> ToList()
        {
            return LocationRefData.StatesAndProvinces;
        }

        public async Task<(double? Latitude, double? Longitude)> GetLatLongFromRequestAsync(HttpRequest request)
        {
            try
            {
                var ip = request.Headers.ContainsKey("X-Forwarded-For")
                    ? request.Headers["X-Forwarded-For"].ToString().Split(',')[0]
                    : request.HttpContext.Connection.RemoteIpAddress?.ToString();

                if (string.IsNullOrWhiteSpace(ip) || ip == "::1")
                    return (null, null);

                using var httpClient = new HttpClient();
                var url = $"http://ip-api.com/json/{ip}?fields=lat,lon,status,message";
                var response = await httpClient.GetStringAsync(url);

                var json = JsonDocument.Parse(response);
                if (json.RootElement.GetProperty("status").GetString() == "success")
                {
                    double lat = json.RootElement.GetProperty("lat").GetDouble();
                    double lon = json.RootElement.GetProperty("lon").GetDouble();
                    return (lat, lon);
                }

                return (null, null);
            }
            catch
            {
                return (null, null);
            }
        }

        public static string? GetClientIp(HttpRequest request)
        {
            if (request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
            {
                var ip = forwardedFor.FirstOrDefault()?.Split(',').FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(ip))
                    return ip;
            }

            return request.HttpContext.Connection.RemoteIpAddress?.ToString();
        }

        public static async Task<(double? lat, double? lon)> GetLatLongFromIpAsync(string ip)
        {
            try
            {
                using var httpClient = new HttpClient();
                var url = $"http://ip-api.com/json/{WebUtility.UrlEncode(ip)}";
                var geo = await httpClient.GetFromJsonAsync<GeoLocation>(url);

                if (geo != null && geo.Status == "success")
                    return (geo.Lat, geo.Lon);
            }
            catch (Exception ex)
            {

            }
            return (null, null);
        }
    }
}

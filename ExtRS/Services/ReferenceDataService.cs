using RestSharp;
using System.Text;

namespace Sonrai.ExtRS
{
    public class ReferenceDataService
    {
        #region Gen-Purpose

        public static async Task<string> GetSynonyms(string wordsPlusDelimited)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync("https://api.datamuse.com/words?ml=" + wordsPlusDelimited);
        }

        public static async Task<string> GetAppPublicIP()
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync("https://api.ipify.org?format=json");
        }

        public static async Task<string> GetCountryByIP(string ip)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(string.Format("https://api.country.is/{0}", ip));
        }

        #endregion

        #region Financial
        public static async Task<string> GetTickerInfo(string ticker, string token)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(string.Format("https://api.tiingo.com/tiingo/daily/{0}?token={1}", ticker, token));
        }

        public static async Task<string> GetTickerPrices(string ticker, string token)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(string.Format("https://api.tiingo.com/tiingo/daily/{0}/prices?token={1}", ticker, token));
        }

        public static async Task<string> GetTickerPriceHistory(string ticker, string start, string end, string token)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(string.Format("https://api.tiingo.com/tiingo/daily/{0}/prices?startDate={1}&endDate={2}?token={3}", ticker, start, end, token));
        }

        public static async Task<string> GetForexPrice(string currencies, string token)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(string.Format("https://api.tiingo.com/tiingo/fx/{0}/top?token={1}", currencies, token));
        }

        public string GetTwelveData()
        {
            var client = new RestClient("https://twelve-data1.p.rapidapi.com/price?symbol=AMZN&format=json&outputsize=30");
            var request = new RestRequest("price", Method.Get);
            request.AddHeader("X-RapidAPI-Key", "76ec5ebe2cmsh2f7ff7de7c01fe8p1add7ejsn7d187081087a");
            request.AddHeader("X-RapidAPI-Host", "twelve-data1.p.rapidapi.com");
            RestResponse response = client.Execute(request);
            //return ""; //https://api.twelvedata.com/logo/ge.com

            return response.StatusCode.ToString();
        }

        #endregion

        #region Shipping

        public static RestResponse GetAuthToken(string shipper, string clientId, string clientSecret, string accountCode = "", string redirectUrl = "")
        {
            switch (shipper)
            {
                case "UPS": return GetAuthTokenUPS(clientId, clientSecret, accountCode, redirectUrl);
                case "FedEx": return GetAuthTokenFedEx(clientId, clientSecret);
                default: return GetAuthTokenUPS(clientId, clientSecret, accountCode, redirectUrl);
            }
        }

        public static RestResponse GetAuthTokenUPS(string clientId, string clientSecret, string accountCode, string redirectUrl)
        {
            var client = new RestClient("https://wwwcie.ups.com/security/v1/oauth/");
            var request = new RestRequest("token", Method.Post);
            var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + clientSecret));
            request.AddHeader("Authorization", "Basic " + authString);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddBody("grant_type=client_credentials"); //&code={0}&redirect_uri={1} accountCode, redirectUrl
            RestResponse response = client.Execute(request);

            return response;
        }
        
        public static RestResponse GetAuthTokenFedEx(string clientId, string clientSecret)
        {
            var client = new RestClient("https://apis-sandbox.fedex.com/oauth/");
            var request = new RestRequest("token", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddBody(string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}", clientId, clientSecret));
            RestResponse response = client.Execute(request);

            return response;
        }

        public static RestResponse GetShippingRates(string service, string shipper, int lbs, decimal ounces, string originPostalCode, string destinationPostalCode, string userId = "", string clientId = "", string clientSecret = "")
        {
            switch (shipper)
            {
                case "USPS": return GetShippingRatesUSPS(lbs, ounces, originPostalCode, destinationPostalCode, userId, service);
                //case "UPS": return await GetShippingRatesUPS(lbs, ounces, originPostalCode, destinationPostalCode, userId);
                //case "FedEx": return await GetShippingRatesFedEx(lbs, ounces, originPostalCode, destinationPostalCode, userId);
                default: return GetShippingRatesUSPS(lbs, ounces, originPostalCode, destinationPostalCode, userId, service);
            }
        }

        public static RestResponse GetShippingRatesUSPS(int lbs, decimal ounces, string originPostalCode, string destinationPostalCode, string userId, string service)
        {
            var client = new RestClient("https://secure.shippingapis.com/shippingapi.dll?API=RateV4&XML=" +
                string.Format(RateRequestUSPS, userId, "ABC123", service, originPostalCode, destinationPostalCode, lbs, ounces));
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/xml");
            var response = client.Execute(request);

            return response;
        }

        public static RestResponse GetShippingRatesUPS(int lbs, decimal ounces, string originPostalCode, string destinationPostalCode, string userId, string clientId, string clientSecret, string apiKey)
        {
            return new RestResponse();
        }

        public static RestResponse GetShippingRatesFedEx(int lbs, decimal ounces, string originPostalCode, string destinationPostalCode, string userId, string clientId, string clientSecret, string apiKey)
        {
            var client = new RestClient("https://apis-sandbox.fedex.com/track/v1");
            var request = new RestRequest("trackingnumbers", Method.Post);
            request.AddHeader("Authorization", "Bearer ");
            request.AddHeader("X-locale", "en_US");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("payload", string.Format(RateRequestUSPS, "", "", "q245346456", "FedEx", "q245346456-1"));
            var response = client.Execute(request);

            return response;
        }

        public static string RateRequestUSPS = @"XML=<RateV4Request USERID='{0}'>
            <Revision>2</Revision>
            <Package ID='{1}'>
                <Service>{2}</Service>
                <ZipOrigination>{3}</ZipOrigination>
                <ZipDestination>{4}</ZipDestination>
                <Pounds>{5}</Pounds>
                <Ounces>{6}</Ounces>
                <Container></Container>
                <Width></Width>
                <Length></Length>
                <Height></Height>
                <Girth></Girth>
                <Machinable>false</Machinable>
            </Package>
            </RateV4Request>";

        public static RestResponse GetTrackingInfo(string shipper, string trackingNumber, string userId = "", string authToken = "")
        {
            switch (shipper)
            {
                case "USPS": return GetTrackingInfoUSPS(trackingNumber, userId);
                case "UPS": return GetTrackingInfoUPS(trackingNumber, authToken);
                case "FedEx": return GetTrackingInfoFedEx(trackingNumber, authToken);
                default: return GetTrackingInfoUSPS(trackingNumber, userId);
            }
        }

        public static RestResponse GetTrackingInfoUSPS(string trackingNumber, string userId)
        {
            var client = new RestClient("https://secure.shippingapis.com/shippingapi.dll?API=TrackV2&XML=" +
                string.Format(TrackingRequestUSPS, userId, trackingNumber));
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/xml");
            var response = client.Execute(request);

            return response;
        }

        public static RestResponse GetTrackingInfoUPS(string trackingNumber, string token = "")
        {
            var client = new RestClient("https://apis-sandbox.fedex.com/track/v1");
            var request = new RestRequest("trackingnumbers", Method.Post);
            request.AddHeader("Authorization", "Bearer ");
            request.AddHeader("X-locale", "en_US");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("payload", string.Format(TrackingRequestUSPS, "", "", "q245346456", "FedEx", "q245346456-1"));
            var response = client.Execute(request);

            return response;
        }

        public static RestResponse GetTrackingInfoFedEx(string trackingNumber, string authToken)
        {
            var client = new RestClient("https://apis-sandbox.fedex.com/track/v1");
            var request = new RestRequest("trackingnumbers", Method.Post);
            request.AddHeader("Authorization", "Bearer ");
            request.AddHeader("X-locale", "en_US");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("payload", string.Format(TrackingRequestFedEx, "", "", "q245346456", "FedEx", "q245346456-1"));
            var response = client.Execute(request);

            return response;
        }

        public static string TrackingRequestUSPS = @"
         <TrackRequest USERID='{0}'>
            <TrackID ID='{1}'></TrackID>
         </TrackRequest>";

        //EMAIL_ALERT
        public static string TrackingRequestUPS = @"
         {
            'uniqueTrackingId': {0},
            'approximateIntakeDate': {1},
            'notifyEventTypes': ['{2}'],
            'firstName': '{3}',
            'lastName': '{4}',
            'notifications': ['{5}']
         }";

        public static string TrackingRequestFedEx = @"{
              'includeDetailedScans': true,
              'trackingInfo': [
                {
                  'shipDateBegin': '{0}',
                  'shipDateEnd': '{1}',
                  'trackingNumberInfo': {
                    'trackingNumber': '{2}',
                    'carrierCode': '{3}',
                    'trackingNumberUniqueId': '{4}'
                  }
                }
              ]
            }";


        #endregion
    }
}

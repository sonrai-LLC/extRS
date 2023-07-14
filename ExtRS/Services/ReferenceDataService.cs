using RestSharp;

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

        #region Market Data
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

        #region Shipping Rates

        public static async Task<string> GetShippingRates(string shipper, int lbs, decimal ounces, string originPostalCode, string destinationPostalCode, string userId = "", string clientId = "", string clientSecret = "")
        {
            switch (shipper)
            {
                case "USPS": return await GetShippingRatesUSPS(lbs, ounces, originPostalCode, destinationPostalCode, userId);
                //case "UPS": return await GetShippingRatesUPS(lbs, ounces, originPostalCode, destinationPostalCode, userId);
                //case "FedEx": return await GetShippingRatesFedEx(lbs, ounces, originPostalCode, destinationPostalCode, userId);
                default: return await GetShippingRatesUSPS(lbs, ounces, originPostalCode, destinationPostalCode, userId);
            }
        }

        public static async Task<string> GetShippingRatesUSPS(int lbs, decimal ounces, string originPostalCode, string destinationPostalCode, string userId)
        {
            var client = new RestClient("https://apis-sandbox.fedex.com/track/v1");
            var request = new RestRequest("trackingnumbers", Method.Post);
            request.AddHeader("Authorization", "Bearer ");
            request.AddHeader("X-locale", "en_US");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("payload", string.Format(RateRequestUSPS, "", "", "q245346456", "FedEx", "q245346456-1"));
            var response = client.Execute(request);

            return response.ToString();
        }

        public static async Task<string> GetShippingRatesUPS(int lbs, decimal ounces, string originPostalCode, string destinationPostalCode, string userId, string clientId, string clientSecret, string apiKey)
        {
            return "";
        }

        public static async Task<string> GetShippingRatesFedEx(int lbs, decimal ounces, string originPostalCode, string destinationPostalCode, string userId, string clientId, string clientSecret, string apiKey)
        {
            return "";
        }

        public static string RateRequestUSPS = @"XML=<RateV4Request USERID='{0}'>
            <Revision>2</Revision>
            <Package ID='{1}'>
                <Service>{2}</Service>
                <ZipOrigination>{3}</ZipOrigination>
                <ZipDestination>{4}</ZipDestination>
                <Pounds>{5}</Pounds>
                <Ounces>{6}</Ounces>
                <Container>{7}</Container>
                <Width>{8}</Width>
                <Length>{9}</Length>
                <Height>{10}</Height>
                <Girth>{11}</Girth>
                <Machinable>false</Machinable>
            </Package>
            </RateV4Request>";

        #endregion

        #region Tracking

        public static async Task<string> GetTrackingInfo(string shipper, string trackingNumber, string userId = "", string clientId = "", string clientSecret = "")
        {
            switch (shipper)
            {
                case "USPS": return await GetTrackingInfoUSPS(trackingNumber, userId);
                case "UPS": return await GetTrackingInfoUPS(trackingNumber, clientId, clientSecret);
                case "FedEx": return await GetTrackingInfoFedEx(trackingNumber, clientId, clientSecret);
                default: return await GetTrackingInfoUSPS(trackingNumber, userId);
            }
        }

        public static async Task<string> GetTrackingInfoUSPS(string trackingNumber, string userId)
        {
            var client = new RestClient("https://secure.shippingapis.com/shippingapi.dll?API=RateV4");
            var request = new RestRequest("trackingnumbers", Method.Post);
            request.AddHeader("Authorization", "Bearer ");
            request.AddHeader("X-locale", "en_US");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("payload", string.Format(TrackingRequestUSPS, "", "", "q245346456", "FedEx", "q245346456-1"));
            var response = client.Execute(request);

            return response.ToString();
        }

        public static async Task<string> GetTrackingInfoUPS(string trackingNumber, string clientId, string clientSecret, string token = "")
        {
            var client = new RestClient("https://apis-sandbox.fedex.com/track/v1");
            var request = new RestRequest("trackingnumbers", Method.Post);
            request.AddHeader("Authorization", "Bearer ");
            request.AddHeader("X-locale", "en_US");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("payload", string.Format(TrackingRequestUSPS, "", "", "q245346456", "FedEx", "q245346456-1"));
            var response = client.Execute(request);

            return response.ToString();
        }

        public static async Task<string> GetTrackingInfoFedEx(string trackingNumber, string clientId, string clientSecret, string token = "")
        {
            var client = new RestClient("https://apis-sandbox.fedex.com/track/v1");
            var request = new RestRequest("trackingnumbers", Method.Post);
            request.AddHeader("Authorization", "Bearer ");
            request.AddHeader("X-locale", "en_US");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("payload", string.Format(TrackingRequestFedEx, "", "", "q245346456", "FedEx", "q245346456-1"));
            var response = client.Execute(request);

            return response.ToString();
        }

        //EMAIL_ALERT
        public static string TrackingRequestUSPS = @"
         {
            'uniqueTrackingId': {0},
            'approximateIntakeDate': {1},
            'notifyEventTypes': ['{2}'],
            'firstName': '{3}',
            'lastName': '{4}',
            'notifications': ['{5}']
         }";

        public static string TrackingRequestUPS = @"
         <TrackRequest USERID='{0}'>
            <TrackID ID='{1}'></TrackID>
         </TrackRequest>";

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

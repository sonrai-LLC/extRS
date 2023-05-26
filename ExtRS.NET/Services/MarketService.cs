using RestSharp;

namespace Sonrai.ExtRS.Services
{

    public class MarketService
    {
        HttpClient _client;
        public MarketService(HttpClient client)
        {
            _client = client;
        }

        public string GetServerHealth()
        {
            var client = new RestClient("https://twelve-data1.p.rapidapi.com/price?symbol=AMZN&format=json&outputsize=30");
            var request = new RestRequest("price", Method.Get);
            request.AddHeader("X-RapidAPI-Key", "76ec5ebe2cmsh2f7ff7de7c01fe8p1add7ejsn7d187081087a");
            request.AddHeader("X-RapidAPI-Host", "twelve-data1.p.rapidapi.com");
            RestResponse response = client.Execute(request);
            //return ""; //https://api.twelvedata.com/logo/ge.com

            return response.StatusCode.ToString();
        }
    }   
}

﻿using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using RestSharp;
using System.Text;
using System.Text.Json;

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
        public static async Task<string> GetTickerInfo(string ticker, string tiingoToken)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(string.Format("https://api.tiingo.com/tiingo/daily/{0}?token={1}", ticker, tiingoToken));
        }

        public static async Task<string> GetTickerPrices(string ticker, string tiingoToken)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(string.Format("https://api.tiingo.com/tiingo/daily/{0}/prices?token={1}", ticker, tiingoToken));
        }

        public static async Task<string> GetTickerPriceHistory(string ticker, string start, string end, string tiingoToken)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(string.Format("https://api.tiingo.com/tiingo/daily/{0}/prices?startDate={1}&endDate={2}?token={3}", ticker, start, end, tiingoToken));
        }

        public static async Task<string> GetForexPrice(string currencies, string tiingoToken)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(string.Format("https://api.tiingo.com/tiingo/fx/{0}/top?token={1}", currencies, tiingoToken));
        }

        public static async Task<List<string>> GetGoogleNews(string search)
        {
            HttpClient client = new HttpClient();
            var content = await client.GetStringAsync(string.Format("https://news.google.com/rss/search?q={0}", search));
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            var newsItems = document.All.Where(m => m.LocalName == "title").ToList();

            return newsItems.Select(x => x.InnerHtml).ToList();
        }

        public static async Task<string> GetNewsApiNews(string search, string fromDate, string apiKey)
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(string.Format("https://newsapi.org/v2/everything?q={0}&from={1}&apiKey={2}", search, fromDate, apiKey));
        }

        public string GetTwelveData()
        {
            var client = new RestClient("https://twelve-data1.p.rapidapi.com/price?symbol=AMZN&format=json&outputsize=30");
            var request = new RestRequest("price", Method.Get);
            request.AddHeader("X-RapidAPI-Key", "76ec5ebe2cmsh2f7ff7de7c01fe8p1add7ejsn7d187081087a");
            request.AddHeader("X-RapidAPI-Host", "twelve-data1.p.rapidapi.com");
            RestResponse response = client.Execute(request);

            return response.StatusCode.ToString();
        }

        #endregion

        #region Shipping

        public static string uspsTest = "https://secure.shippingapis.com/";
        public static string uspsProd = "https://production.shippingapis.com/";

        public static string upsTest = "https://wwwcie.ups.com/api/";
        public static string upsProd = "https://onlinetools.ups.com/api/";

        public static string fedexTest = "https://apis-sandbox.fedex.com/";
        public static string fedexProd = "https://apis-sandbox.fedex.com/";


        public static string GetAuthToken(Shipper shipper, string clientId, string clientSecret, string accountCode = "", string redirectUrl = "")
        {
            switch (shipper)
            {
                case Shipper.UPS: return GetAuthTokenUPS(clientId, clientSecret, accountCode, redirectUrl);
                case Shipper.FedEx: return GetAuthTokenFedEx(clientId, clientSecret);
                default: return GetAuthTokenUPS(clientId, clientSecret, accountCode, redirectUrl);
            }
        }

        public static string GetAuthTokenUPS(string clientId, string clientSecret, string accountCode, string redirectUrl)
        {
            var client = new RestClient("https://wwwcie.ups.com/security/v1/oauth/");
            var request = new RestRequest("token", Method.Post);
            var authString = Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + clientSecret));
            request.AddHeader("Authorization", "Basic " + authString);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddBody("grant_type=client_credentials");
            RestResponse response = client.Execute(request);
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(response.Content!);

            return obj["access_token"];
        }
        
        public static string GetAuthTokenFedEx(string clientId, string clientSecret)
        {
            var client = new RestClient("https://apis-sandbox.fedex.com/oauth/");
            var request = new RestRequest("token", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddBody(string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}", clientId, clientSecret));
            RestResponse response = client.Execute(request);
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(response.Content!);

            return obj["access_token"];
        }

        public static RestResponse GetShippingRates(Shipper shipper, string service, int lbs, decimal ounces, Address origin, Address destination, string userId = "", string authToken = "", string shipperNumber = "", bool isProd = false)
        {
            switch (shipper)
            {
                case Shipper.USPS: return GetShippingRatesUSPS(lbs, ounces, origin, destination, userId, service, "1ST", isProd);
                case Shipper.UPS: return GetShippingRatesUPS(lbs, ounces, origin, destination, authToken, service, shipperNumber, isProd);
                case Shipper.FedEx: return GetShippingRatesFedEx(lbs, ounces, origin, destination, authToken, "", isProd);
                default: return GetShippingRatesUSPS(lbs, ounces, origin, destination, userId, service, "1ST", isProd);
            }
        }

        public static RestResponse GetShippingRatesUSPS(int lbs, decimal ounces, Address origin, Address destination, string userId, string service, string packageId = "1ST", bool isProd = false)
        {
            var client = new RestClient(string.Format("{0}/shippingapi.dll?API=RateV4&XML=", isProd ? uspsProd : uspsTest)
            + string.Format(RateRequestUSPS, userId, packageId, service, origin.PostalCode, destination.PostalCode, lbs, ounces));
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/xml");
            var response = client.Execute(request);

            return response;
        }

        public static RestResponse GetShippingRatesUPS(int lbs, decimal ounces, Address origin, Address destination, string authToken, string service, string shipperNumber, bool isProd = false)
        {
            var client = new RestClient(string.Format("{0}rating/v1/", isProd ?  upsProd : upsTest));  //C2016A
            var request = new RestRequest("rate", Method.Post);
            request.AddHeader("Authorization", "Bearer " + authToken);
            request.AddHeader("X-locale", "en_US");
            request.AddHeader("Content-Type", "application/json");
            string content = RateRequestUPS.Replace("{0}", "extrs").Replace("{1}", "extrs").Replace("{2}", shipperNumber).Replace("{3}", origin.AddressLine)
                .Replace("{4}", origin.City).Replace("{5}", origin.State).Replace("{6}", origin.PostalCode).Replace("{7}", origin.Country).Replace("{8}", destination.AddressLine)
                .Replace("{9}", destination.City).Replace("{10}", destination.State).Replace("{11}", destination.PostalCode).Replace("{12}", destination.Country).Replace("{13}", service).Replace("{14}", lbs.ToString());
            request.AddBody(content);
            var response = client.Execute(request);

            return response;
        }

        public static RestResponse GetShippingRatesFedEx(int lbs, decimal ounces, Address origin, Address destination, string authToken, string userId = "", bool isProd = false)
        {
            var client = new RestClient(string.Format("{0}//rate/v1/rates/", isProd ? fedexProd : fedexTest));
            var request = new RestRequest("quotes", Method.Post);
            request.AddHeader("Authorization", "Bearer " + authToken);
            request.AddHeader("X-locale", "en_US");
            request.AddHeader("Content-Type", "application/json");
            string content = RateRequestFedEx.Replace("{0}", origin.PostalCode).Replace("{1}", destination.PostalCode).Replace("{2}", lbs.ToString());
            request.AddBody(content);
            var response = client.Execute(request);

            return response;
        }

        public static string RateRequestUSPS = @"<RateV4Request USERID='{0}'>
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

        public static string RateRequestUPS = @"{
          RateRequest: {
            Request: {
              TransactionReference: {
                CustomerContext: '{0}',
                TransactionIdentifier: '000-11-2222-3'
              }
            },
            Shipment: {
              Shipper: {
                Name: '{1}',
                ShipperNumber: '{2}',
                Address: {
                  AddressLine: [
                    '{3}',
                  ],
                  City: '{4}',
                  StateProvinceCode: '{5}',
                  PostalCode: '{6}',
                  CountryCode: '{7}'
                }
              },
              ShipTo: {
                Address: {
                  AddressLine: [
                    '{8}'
                  ],
                  City: '{9}',
                  StateProvinceCode: '{10}',
                  PostalCode: '{11}',
                  CountryCode: '{12}'
                }
              },
              PaymentDetails: {
                ShipmentCharge: {
                  Type: '01',
                  BillShipper: {
                    AccountNumber: '{2}'
                  }
                }
              },
              Service: {
                Code: '02',
                Description: '{13}'
              },
              Package: {
                SimpleRate: {
                  Description: 'SimpleRateDescription',
                  Code: 'XS'
                },
                PackagingType: {
                  Code: '02',
                  Description: 'Packaging'
                },
                PackageWeight: {
                  UnitOfMeasurement: {
                    Code: 'LBS',
                    Description: 'Pounds'
                  },
                  Weight: '{14}'
                }
              }
            }
          }
        }";

       

        public static string RateRequestFedEx = @"{
          ""accountNumber"": {
            ""value"": ""XXXXX7364""
          },
          ""requestedShipment"": {
            ""shipper"": {
              ""address"": {
                ""postalCode"": {0},
                ""countryCode"": ""US""
              }
            },
            ""recipient"": {
              ""address"": {
                ""postalCode"": {1},
                ""countryCode"": ""US""
              }
            },
            ""pickupType"": ""DROPOFF_AT_FEDEX_LOCATION"",
            ""rateRequestType"": [
              ""ACCOUNT"",
              ""LIST""
            ],
            ""requestedPackageLineItems"": [
              {
                ""weight"": {
                  ""units"": ""LB"",
                  ""value"": {2}
                }
              }
            ]
          }
        }";

        public static RestResponse GetTrackingInfo(Shipper shipper, string trackingNumber, string userId = "", string authToken = "")
        {
            switch (shipper)
            {
                case Shipper.USPS: return GetTrackingInfoUSPS(trackingNumber, userId);
                case Shipper.UPS: return GetTrackingInfoUPS(trackingNumber, authToken);
                case Shipper.FedEx: return GetTrackingInfoFedEx(trackingNumber, authToken);
                default: return GetTrackingInfoUSPS(trackingNumber, userId);
            }
        }

        public static RestResponse GetTrackingInfoUSPS(string trackingNumber, string userId, bool isProd = false)
        {
            var client = new RestClient(isProd ? uspsProd : uspsTest + "shippingapi.dll?API=TrackV2&XML=" +
                string.Format(TrackingRequestUSPS, userId, trackingNumber));
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/xml");
            var response = client.Execute(request);

            return response;
        }

        public static RestResponse GetTrackingInfoUPS(string trackingNumber, string authToken, bool isProd = false)
        {
            var client = new RestClient(string.Format("{0}/track/v1/details/{1}", isProd ? upsProd : upsTest,  trackingNumber));
            var request = new RestRequest("details", Method.Get);
            request.AddHeader("Authorization", "Bearer " + authToken);
            request.AddHeader("X-locale", "en_US");
            request.AddHeader("transId", "1234567");
            request.AddHeader("transactionSrc", "fissonrai.io");
            var response = client.Execute(request);

            return response;
        }

        public static RestResponse GetTrackingInfoFedEx(string trackingNumber, string authToken, bool isProd = false)
        {
            var client = new RestClient(string.Format("{0}/track/v1/", isProd ? fedexProd : fedexTest));
            var request = new RestRequest("trackingnumbers", Method.Post);
            request.AddHeader("Authorization", "Bearer " + authToken);
            request.AddHeader("X-locale", "en_US");
            request.AddBody(TrackingRequestFedEx.Replace("{0}", trackingNumber).Replace("{1}", "FDXE").Replace("{2}", trackingNumber + "-1"));
            var response = client.Execute(request);

            return response;
        }

		public static RestResponse GetVerifyAddressUSPS(string userId, string address1, string address2, string city, string state, string zip, bool isProd = false)
		{
			var client = new RestClient(isProd ? uspsProd : uspsTest + "shippingapi.dll?API=Verify&XML=" +
				string.Format(VerifyAddressUSPS, userId, address1, address2, city, state, zip));
			var request = new RestRequest();
			request.AddHeader("Content-Type", "application/xml");
			var response = client.Execute(request);

			return response;
		}

		public static string TrackingRequestUSPS = @"
         <TrackRequest USERID='{0}'>
            <TrackID ID='{1}'></TrackID>
         </TrackRequest>";


        public static string TrackingRequestFedEx = @"{
              'includeDetailedScans': true,
              'trackingInfo': [
                {
                  'trackingNumberInfo': {
                    'trackingNumber': '{0}',
                    'carrierCode': '{1}',
                    'trackingNumberUniqueId': '{2}'
                  }
                }
             ]}
           }";

		//https://secure.shippingapis.com/shippingapi.dll?API=Verify&XML=
		public static string VerifyAddressUSPS = @"
        <AddressValidateRequest USERID=""{0}"">
        <Revision>1</Revision>
        <Address ID=""0"">
        <Address1>{1}</Address1>
        <Address2>{2}</Address2>
        <City>{3}</City>
        <State>{4}</State>
        <Zip5>{5}</Zip5>
        <Zip4/>
        </Address>
        </AddressValidateRequest>";

        public struct Address
        {
            public string AddressLine;
            public string City;
            public string State;
            public string PostalCode;
            public string Country;
        }

        public enum Shipper
        {
            USPS,
            UPS,
            FedEx
        }

        #endregion
    }
}

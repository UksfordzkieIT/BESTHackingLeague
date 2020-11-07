using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EkoApp.Services
{
    public class GeolocService
    {
        public struct Petrol
        {
            public string Name { get; set; }
            public string Street { get; set; }
            public string Price { get; set; }
        };
        public string GetBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            return "";
        }
        public List<Petrol> GetNearestPetrols(string city, string fuelType)
        {
            var client = new WebClient();

            string text = client.DownloadString("https://cenapaliw.pl/stationer/" + fuelType + "/alla/"+ city);
            List<Petrol> stations = new List<Petrol>();
            string[] delimeterWords = {
                "<tbody>", "</tbody>"
             };
            string[] test = text.Split(delimeterWords, StringSplitOptions.None);
            text = test[1];
            string[] delimeterSign = { "</tr>" };
            test = text.Split(delimeterSign, StringSplitOptions.None);
            string[] delimeterName = {
                 "<td><b>"," <small>"
             };
            string[] delimeterStreet = {
                 "</small></b><br />","</td>"
             };
            string[] delimeterPrice = {
                 "color: #000000;\">","</b><br /><small>"
             };
           int k = 0;

            foreach (var i in test)
            {
                string[] tempName = i.Split(delimeterName, StringSplitOptions.None);
                string[] tempStreet = i.Split(delimeterStreet, StringSplitOptions.None);
                string[] tempPrice = i.Split(delimeterPrice, StringSplitOptions.None);
                if (tempName.Length >= 2 && tempPrice.Length >= 2 && tempStreet.Length >= 2 && tempName[1] != "Inne")
                {
                    var street = tempStreet[1].Split('/')[0];
                    stations.Add(new Petrol
                    {
                        Name = tempName[1],
                        Street = street.Split('(')[0],
                        Price = tempPrice[1]
                    });
                }
                k++;
                if (k == 10)
                    break;
            }
            return stations;
        }
        public async Task<string> ReversedGeocode(string lant, string longi)
        {
            var client = new HttpClient();
            var ulr = "https://geoapify-platform.p.rapidapi.com/v1/geocode/reverse?lat=" + lant + "&apiKey=a66b15f2e00b41bb9bc1f89e3e3e95a9&lon=" + longi + "&lang=en&limit=1&type=city";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(ulr),
                Headers =
    {
        { "x-rapidapi-key", "bdd083fea6mshd1e6fda33f40feap1e0940jsn473be46e109c" },
        { "x-rapidapi-host", "geoapify-platform.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {

                    string body = await response.Content.ReadAsStringAsync();

                    var data = (JObject)JsonConvert.DeserializeObject(body);

                    var features = data["features"];
                    var deepdown = features[0];
                    var props = deepdown["properties"];
                    var city = props["city"];

                    return city.ToString();
                }
            }
            return null;
        }
        public async Task<float> GetDistance(string clientLat, string clientLong, string petrolLat, string petrolLong)
        {
            string url = "https://api.openrouteservice.org/v2/directions/driving-car?api_key=5b3ce3597851110001cf6248338e15fa82e94419b2dd6dab1b9d8e0c&start=" + clientLong + ',' + clientLat + "&end=" + petrolLong + "," + petrolLat;
            var baseAddress = new Uri(url);

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = baseAddress
            };

            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    string body = await response.Content.ReadAsStringAsync();
                    var data = (JObject)JsonConvert.DeserializeObject(body);
                    var features = data["features"];
                    var deepdown = features[0];
                    var props = deepdown["properties"];
                    var summary = props["summary"];
                    var distance = summary["distance"].ToString();
                    return float.Parse(distance);
                }
                return -1;
            }
        }
        public async Task<Tuple<string, string>> GeoCode(string street)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://geoapify-platform.p.rapidapi.com/v1/geocode/search?apiKey=a66b15f2e00b41bb9bc1f89e3e3e95a9&text=" + street + "&lang=en&limit=1"),
                Headers =
    {
        { "x-rapidapi-key", "bdd083fea6mshd1e6fda33f40feap1e0940jsn473be46e109c" },
        { "x-rapidapi-host", "geoapify-platform.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    string body = await response.Content.ReadAsStringAsync();
                    var data = (JObject)JsonConvert.DeserializeObject(body);
                    var features = data["features"];
                    var deepdown = features[0];
                    var props = deepdown["properties"];
                    var longtitude = props["lon"].ToString();
                    var longToReturn = longtitude.Replace(',', '.');
                    var lattitude = props["lat"].ToString();
                    var lattToReturn = lattitude.Replace(',', '.');

                    return new Tuple<string, string>(longToReturn, lattToReturn);
                }
                return null;
            }
        }
    }
}

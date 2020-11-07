using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace EkoApp.Services
{
    public static class GeolocService
    {
        public struct Petrol
        {
            public string Name { get; set; }
            public string Street { get; set; }
            public string Price { get; set; }
        };
        public static string GetBetween(string strSource, string strStart, string strEnd)
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
        public static List<Petrol> GetNearestPetrols(string city, string fuelType)
        {
            var client = new WebClient();
            //Dictionary<string, string> countys = new Dictionary<string, string>();
            //countys.Add("Lower Silesian","dolno-slaskie");
            //countys.Add("Kuyavian-Pomeranian","kujawsko-pomorskie");
            //countys.Add("Lublin","lubelskie");
            //countys.Add("Lubusz","Lublin");
            //countys.Add("Łódź","lodzkie");
            //countys.Add("Lesser Poland","malopolskie");
            //countys.Add("Masovian","mazowieckie");
            //countys.Add("Opole","opolskie");
            //countys.Add("Subcarpathian","podkarpackie");
            //countys.Add("podlaskie","podlaskie");
            //countys.Add("Pomeranian","pomorskie");
            //countys.Add("Silesian","slaskie");
            //countys.Add("Świętokrzyskie","swietokrzyskie");
            //countys.Add("Warmian-Masurian","warminsko-mazurskie");
            //countys.Add("Greater Poland","wielkopolskie");
            //countys.Add("West Pomeranian", "zachodniopomorskie");

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
                if(tempName.Length>=2 && tempPrice.Length >= 2 && tempStreet.Length >= 2 && tempName[1]!= "Inne")
                stations.Add(new Petrol
                {
                    Name = tempName[1],
                    Street = tempStreet[1].Split('/')[0],
                    Price = tempPrice[1]
                });
                k++;
                if (k == 10)
                    break;
            }
            return stations;
        }
        public static async Task<string> ReversedGeocode(string lant, string longi)
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
                response.EnsureSuccessStatusCode();

                string body = await response.Content.ReadAsStringAsync();

                var data = (JObject)JsonConvert.DeserializeObject(body);

                var features = data["features"];
                var deepdown = features[0];
                var props = deepdown["properties"];
                var city = props["city"];

                return city.ToString();
            }
        }
        public static async Task<float> GetDistance(string clientLat, string clientLong, string petrolLat, string petrolLong)
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
                string body = await response.Content.ReadAsStringAsync();
                var data = (JObject)JsonConvert.DeserializeObject(body);
                var features = data["features"];
                var deepdown = features[0];
                var props = deepdown["properties"];
                var summary = props["summary"];
                var distance = summary["distance"].ToString();
                return float.Parse(distance);
            }
        }
        public static async Task<Tuple<string, string>> GeoCode(string street)
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
        }
    }
}

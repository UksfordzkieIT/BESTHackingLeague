using System;
using System.Collections.Generic;
using System.Linq;
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
        public static List<Petrol> GetNearestPetrols(string county, string city, string fuelType)
        {
            var client = new WebClient();
            Dictionary<string, string> countys = new Dictionary<string, string>();
            countys.Add("Lower Silesian","dolno-slaskie");
            countys.Add("Kuyavian-Pomeranian","kujawsko-pomorskie");
            countys.Add("Lublin","lubelskie");
            countys.Add("Lubusz","Lublin");
            countys.Add("Łódź","lodzkie");
            countys.Add("Lesser Poland","malopolskie");
            countys.Add("Masovian","mazowieckie");
            countys.Add("Opole","opolskie");
            countys.Add("Subcarpathian","podkarpackie");
            countys.Add("podlaskie","podlaskie");
            countys.Add("Pomeranian","pomorskie");
            countys.Add("Silesian","slaskie");
            countys.Add("Świętokrzyskie","swietokrzyskie");
            countys.Add("Warmian-Masurian","warminsko-mazurskie");
            countys.Add("Greater Poland","wielkopolskie");
            countys.Add("West Pomeranian", "zachodniopomorskie");

            string text = client.DownloadString("https://cenapaliw.pl/stationer/" + "e95/" + countys[county] + '/' + "warszawa");
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
                if(tempName.Length>=2 && tempPrice.Length >= 2 && tempStreet.Length >= 2)
                stations.Add(new Petrol
                {
                    Name = tempName[1],
                    Street = tempStreet[1],
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
                string[] delimeterWords = { "\"city\":\"", "\",\"suburb\"" };
                string[] test = body.Split(delimeterWords, StringSplitOptions.None);
                return test[1];
            }
        }
    }
}

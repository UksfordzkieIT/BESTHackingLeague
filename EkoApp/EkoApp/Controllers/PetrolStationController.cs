using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Threading.Tasks;
using EkoApp.Services;
using EkoApp.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using static EkoApp.Services.GeolocService;

namespace EkoApp.Controllers
{
	
	public class PetrolStationController : Controller
	{
		
		public IActionResult Index()
		{
			return View(new LocalisationModel());
		}
		public async Task<IActionResult> ShowNearest(string latitude, string longitude, double fuelVolume = 50)
		{
			double fuelConsumption = 8.0;
			string nazwaMiastaEng = await GeolocService.ReversedGeocode(latitude, longitude);

			List<PetrolWithData> petrols = new List<PetrolWithData>();
			var neartestPetrols = GeolocService.GetNearestPetrols("Warszawa", "e95");
			foreach(var petr in neartestPetrols)
			{
				var nazwasplit = petr.Street.Split(' ');
				var price = petr.Price.Split("zł");
				string nazwa = "";
				foreach (var j in nazwasplit)
				{
					nazwa += j + "%20";
				}
				nazwa += nazwaMiastaEng;
				var petrolCoord = await GeolocService.GeoCode(nazwa);
				var dist = await GeolocService.GetDistance(latitude, longitude, petrolCoord.Item2, petrolCoord.Item1);
				petrols.Add(new PetrolWithData
				{
					Petrol = petr,
					Distance = dist,
					TravelCost = ((2* dist)/100000 * fuelConsumption) * float.Parse(price[0]),
					RealPricePerLiter = (float.Parse(price[0]) * fuelVolume) / (fuelVolume - ((2 * dist) / 100000 * fuelConsumption))
				});
			}

			var a = petrols.OrderBy(x => x.RealPricePerLiter).ToList();
			//var a = petrols;

			
			return View(a);
		}
	}
}

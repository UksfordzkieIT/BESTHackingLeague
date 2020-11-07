using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EkoApp.Services;
using EkoApp.ViewsModel;
using Microsoft.AspNetCore.Mvc;

namespace EkoApp.Controllers
{
	
	public class PetrolStationController : Controller
	{
		
		public IActionResult Index()
		{
			return View(new LocalisationModel());
		}
		public async Task<IActionResult> ShowNearest(string latitude, string longitude, string fuelVolume, string fuelConsumption)
		{
			string nazwaMiastaEng = await GeolocService.ReversedGeocode(latitude, longitude);
			ViewData["latitude"] = latitude;
			ViewData["longitude"] = longitude;
			List<PetrolWithData> petrols = new List<PetrolWithData>();
			var neartestPetrols = GeolocService.GetNearestPetrols("Warszawa", "e95");

			var fuelVol = double.Parse(fuelVolume);
			double fuelConsum = double.Parse(fuelConsumption);

			foreach (var petr in neartestPetrols)
			{
				var nazwasplit = petr.Street.Split(' ');
				var price = petr.Price.Split("zł");
				string nazwa = nazwasplit[0];
				for(int i =1;i<nazwasplit.Length;++i)
				{
					if (nazwasplit[i] == "" || nazwasplit[i] == "\n")
					{
						break;
					}
					nazwa += "%20" + nazwasplit[i];
				}
				nazwa += ',' + nazwaMiastaEng;
				var petrolCoord = await GeolocService.GeoCode(nazwa);
				if (petrolCoord != null)
				{
					var dist = await GeolocService.GetDistance(latitude, longitude, petrolCoord.Item2, petrolCoord.Item1);
					petrols.Add(new PetrolWithData
					{
						Petrol = petr,
						Distance = dist / 1000,
						TravelCost = ((2 * dist) / 100 * fuelConsum) * float.Parse(price[0]),
						RealPricePerLiter = (float.Parse(price[0]) * fuelVol) / (fuelVol - ((2 * dist) / 100000 * fuelConsum)),
						Coords = petrolCoord
					});
				}
			}

			var a = petrols.OrderBy(x => x.RealPricePerLiter).ToList();
			
			return View(a);
		}
	}
}

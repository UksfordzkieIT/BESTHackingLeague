using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EkoApp.Services;
using EkoApp.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EkoApp.Controllers
{
	[Authorize]
	public class PetrolStationController : Controller
	{
		private readonly GeolocService _geolocService;
		public PetrolStationController(GeolocService geolocService)
		{
			_geolocService = geolocService;
		}
		private string TlumaczNazwe(string nazwa)
		{
			if(nazwa.ToLower() == "warsaw")
			{
				return "warszawa";
			}
			else if (nazwa.ToLower() == "cracow")
			{
				return "krakow";
			}
			else
			{
				return nazwa.ToLower();
			}
		}
		
		public IActionResult Index()
		{
			return View(new LocalisationModel());
		}
		public async Task<IActionResult> ShowNearest(string latitude, string longitude, string fuelVolume, string fuelConsumption)
		{
			string nazwaMiastaEng = await _geolocService.ReversedGeocode(latitude, longitude);
			if (nazwaMiastaEng != null)
			{
				ViewData["latitude"] = latitude;
				ViewData["longitude"] = longitude;
				List<PetrolWithData> petrols = new List<PetrolWithData>();
				
				var neartestPetrols = _geolocService.GetNearestPetrols(TlumaczNazwe(nazwaMiastaEng), "e95");

				var fuelVol = double.Parse(fuelVolume);
				double fuelConsum = double.Parse(fuelConsumption);

				foreach (var petr in neartestPetrols)
				{
					var nazwasplit = petr.Street.Split(' ');
					var price = petr.Price.Split("zł");
					string nazwa = nazwasplit[0];
					for (int i = 1; i < nazwasplit.Length; ++i)
					{
						if (nazwasplit[i] == "" || nazwasplit[i] == "\n")
						{
							break;
						}
						nazwa += "%20" + nazwasplit[i];
					}
					nazwa += ',' + TlumaczNazwe(nazwaMiastaEng);
	
					var petrolCoord = await _geolocService.GeoCode(nazwa);
					if (petrolCoord != null)
					{
						var dist = await _geolocService.GetDistance(latitude, longitude, petrolCoord.Item2, petrolCoord.Item1);
						if (dist != -1)
						{
							var realPrice = (float.Parse(price[0]) * fuelVol) / (fuelVol - ((2 * dist) / 100000 * fuelConsum));
							if (realPrice > 0)
							{
								petrols.Add(new PetrolWithData
								{
									Petrol = petr,
									Distance = dist / 1000,
									TravelCost = ((2 * dist) / 100 * fuelConsum) * float.Parse(price[0]),
									RealPricePerLiter = realPrice,
									Coords = petrolCoord
								});
							}
						}
					}
				}

				var a = petrols.OrderBy(x => x.RealPricePerLiter).ToList();

				return View(a);
			}
			return NotFound();
		}
	}
}

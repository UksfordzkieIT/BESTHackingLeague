using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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
		public async Task<IActionResult> ShowNearest(string latitude, string longitude)
		{
			 var odp = await GeolocService.ReversedGeocode(latitude, longitude);
			var petrol = GeolocService.GetNearestPetrols(null, null, null);
			return View();
		}
	}
}

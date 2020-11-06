using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EkoApp.Controllers
{
	public class PetrolStationController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
